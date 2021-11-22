using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

#pragma warning disable CS0618 // Type or member is obsolete; use for some thread commands

namespace _490Gui
{
    public partial class Form1 : Form
    {
        //String[] processesChosen;
        static Queue<Process> processList = new Queue<Process>(); // list of processes pulled from csv for threads
        static List<Process> HRRNProcessList = new List<Process>(); // list of processes for hrrn
        static List<Process> HRRNwaitlist = new List<Process>(); // for HRRNWaitProcessQueue to pull processes from to show on queue
        static List<Process> rrFinishedProcess = new List<Process>(); // completed processes on rr side
        static List<Process> hrrnFinishedProcess = new List<Process>(); // completed processes on hrrn side

        Thread thread1; // first thread
        Thread thread2; // second thread
        int secondsElapsed; // time passed in program
        bool started = false; // bool tracking if process has started already
        bool running = false; // bool to track if threads are running
        string file; // string to store csv
        double hrrnAvgNTAT = 0; // stores avg NTAT for HRRN
        float rrAvgNTAT = 0; // stores avg NTAT for RR
        int tempTime;
        bool hrrnInitiated = false; // determines whether hrrn clonable function is run or not

        /**
        * default constructor for Form1
        **/

        public Form1()
        {

            // initialize GUI
            InitializeComponent();

            // ask user to choose what file they want to run program off of
            openFileDialog1.ShowDialog();
            file = openFileDialog1.FileName;

            // load processList with data from 
            processList = Parser.readProcessFile(file);
            //hrrnProcessList = Parser.readProcessFile(file);

            // datagrid generation assumes a non-blank csv file is used

            // populate datagrid with initial data

            // read in data 

        }

        // events for GUI

        /**
         * event handler code for when the start system button is clicked
        */
        public delegate void StartSys_ClickEventHandler(object source, EventArgs args);
        public event StartSys_ClickEventHandler StartSysClicked;
        private void StartSys_Click(object sender, EventArgs e)
        {

            if (started == false)
            {
                // set the time (in ms) that the threads will run at for the whole program
                timeElapsed.Interval = Decimal.ToInt32(this.timeUnitUpDown.Value);

                DataTable table2 = new DataTable(); // table used to populate data grid
                String[] headers2 = { "Process Name", "Service Time" };
                String[] l2 = System.IO.File.ReadAllLines(file);

                // set up headers

                foreach (String header in headers2)
                {
                    table2.Columns.Add(header, typeof(String));
                }

                // for each line in csv
                for (int i = 0; i < l2.Length; i++)
                {
                    if (processList.ElementAt<Process>(i).ArriveTime == 0)
                    {
                        DataRow row = table2.NewRow();
                        table2.Rows.Add(processList.ElementAt<Process>(i).ProcessID, processList.ElementAt<Process>(i).ServiceTime);
                    }

                }

                // initialize RR and HRRN wait process queue grid with these values
                rrWaitProcessQueue.DataSource = table2;
                HRRNWaitProcessQueue.DataSource = table2;

                // initialize starting threads
                this.sysStatLabel.Text = "System Running"; // changes text on system status
                // this assumes thread1 is meant to start at time 0 and there is at least one thread
                thread1 = new Thread(new ThreadStart(SelectProcess));
                //cpu2TRShow.Text = tempTime.ToString();
                thread2 = new Thread(new ThreadStart(SelectProcess));


                // begin running threads

                thread1.Start(); // start thread 1 (Round Robin) execution
                //setCPU2TRShow(Decimal.ToInt32(rrTSLength.Value));
                thread2.Start(); // start thread 2 (HRRN) execution

                timeElapsed.Start(); // start the GUI-side timer

                this.cpu1ProcessExec.Text = thread2.Name; // set the text denoting what process is running based on this process
                this.cpu2ProcessExec.Text = thread1.Name;

                started = true; // the program has begun running
                running = true; // the program is currently running
            }
            else
            {
                // check to see if program is running and if either of the threads is still running
                if (running == false && (thread1.IsAlive || thread2.IsAlive))
                {
                    if (thread1.IsAlive) { thread1.Resume(); } // only run this line if thread1 hasn't finished
                    if (thread2.IsAlive) { thread2.Resume(); } // only run this line if thread2 hasn't finished
                    running = true; // running bool now reflects system running status
                }
                if (timeElapsed.Enabled == false)
                {
                    timeElapsed.Enabled = true; // GUI-side timer now enabled
                    this.sysStatLabel.Text = "System Running"; // changes text on system status
                }
            }

        }
        // trigger for StartSysClicked event listener
        protected virtual void OnStartSysClick()
        {
            // continuously check to see if Start System button has been clicked
            if (StartSysClicked != null)
            {
                StartSysClicked(this, EventArgs.Empty);
            }
        }

        /**
         * event handler code for when the pause system button is clicked
        */
        public delegate void PauseSysButton_ClickEventHandler(object source, EventArgs args);
        public event PauseSysButton_ClickEventHandler PauseSysClicked;
        private void PauseSysButton_Click(object sender, EventArgs e)
        {

            if (running == true && (thread1.IsAlive || thread2.IsAlive))
            {
                if (thread1.IsAlive) { thread1.Suspend(); } // only run this line if thread1 hasn't finished
                if (thread2.IsAlive) { thread2.Suspend(); } // only run this line if thread2 hasn't finished
                running = false; // running bool now reflects system paused status
            }
            if (timeElapsed.Enabled == true)
            {
                timeElapsed.Enabled = false; // pause GUI-side timer
                this.sysStatLabel.Text = "System Paused"; // changes text on system status
            }
        }
        protected virtual void OnPauseSysClick()
        {
            // constantly checks if pause button has been clicked
            if (PauseSysClicked != null)
            {
                PauseSysButton_Click(this, EventArgs.Empty);
            }
        }

        // Summary: Thread will select a new process from the queue
        // and then send the process to be executed.
        // Params: time
        // Return: None
        public void SelectProcess()
        {
            Process process;
            var counter = processList.Count;
            List<Process> processListArray = processList.ToList();
            
            bool executedServiceTime;
            if (hrrnInitiated == false)
            {
                foreach (var elt in processListArray)
                {
                    HRRNProcessList.Add((Process)elt.Clone());
                    //hrrnProcessList.Add((Process)elt.Clone());
                    HRRNwaitlist.Add((Process)elt.Clone());
                    hrrnInitiated = true;
                }
                
            }
            
            //hrrnProcessList = HRRNProcessList;
            //var HRRNProcessList = DeepCopy(processListArray);
            //bool executedServiceTime;
            // set names on GUI events
            if (Thread.CurrentThread.ManagedThreadId == 3)
            {

                while (counter != 0)
                {
                    if (processList.Peek().EntryTime != null)
                    {
                        processList.Peek().EntryTime = DateTime.Now;
                    }
                    long executionTime = (processList.Peek().EntryTime.Ticks - Program.programStartTime.Ticks) / 10000000;
                    int arrivalTime = processList.Peek().ArriveTime;
                    //int counter;
                    // var process = processList.Dequeue();
                    if (arrivalTime <= executionTime)
                    {
                        process = processList.Dequeue();
                        tempTime = process.ServiceTime;
                        Console.WriteLine("process service time is: " + process.ServiceTime);
                        counter--;
                        try
                        {
                            setCPU2TRShow(process.ServiceTime);
                        }
                        catch
                        {
                            Invoke(new Action(() => { setCPU2TRShow(process.ServiceTime); cpu2ProcessExec.Text = process.ProcessID; }));
                        }
                        executedServiceTime = ThreadSim.executeRoundRobin(process, Decimal.ToInt32(this.timeUnitUpDown.Value), Decimal.ToInt32(this.rrTSLength.Value)); //This would be changed to call HRRN and RR respectively

                    }
                    else
                    {

                        Thread.Sleep(1000);
                        continue;
                    }
                    if (executedServiceTime || process.ServiceTime <= 0)
                    {
                        process.ServiceTime = 0;
                        Console.WriteLine("***********      " + process.ProcessID + " has exited. Program List Count: " + processList.Count + "      ************");
                        process.IntFinishTime = secondsElapsed;

                        rrFinishedProcess.Add(process);
                        // Console.WriteLine("***********      " + processList.Peek().ProcessID + "      ************");
                    }
                    else
                    {
                        processList.Enqueue(process);
                        process.ServiceTime = process.ServiceTime - (Decimal.ToInt32(this.rrTSLength.Value));
                        counter++;
                    }
                    //cpu2TRShow.Text = tempTime.ToString();
                }
            }

            if (Thread.CurrentThread.ManagedThreadId == 4)
            {
                // List<Process> processListArray = processList.ToList();
                List<Process> availableProcessesList = new List<Process>();
                List<Process> removeList = new List<Process>();
                while (HRRNProcessList.Count != 0)
                {

                    // Grab all items that are ready to be executed and add them to availableProcessList
                    for (int i = 0; i < HRRNProcessList.Count; i++)
                    {
                        var executionTime2 = (DateTime.Now.Ticks - Program.programStartTime.Ticks) / 10000000;
                        var arrivalTime2 = HRRNProcessList[i].ArriveTime;
                        if (HRRNProcessList[i].ArriveTime <= executionTime2)
                        {
                            HRRNProcessList[i].AvailableProcessesTime = DateTime.Now;
                            availableProcessesList.Add(HRRNProcessList[i]);
                            removeList.Add(HRRNProcessList[i]);
                        }
                    }
                    

                    // Remove items from HRRNProcessList that will be executed
                    for (int i = 0; i < removeList.Count; i++)
                    {
                        if (HRRNProcessList.Contains(removeList[i]))
                        {
                            HRRNProcessList.Remove(removeList[i]);
                        }
                        
                    }
                    
                    //hrrnProcessList = HRRNProcessList;
                    removeList.Clear(); //clear remove list so we can do it again

                    // If there are more than 1 items in availableProcessList, calculate the response ratio and sort them accordingly
                    if (availableProcessesList.Count > 1)
                    {
                        calculateResponseRatio(availableProcessesList);
                    }

                    // Execute all items in availableProcessList
                    for (int i = 0; i < availableProcessesList.Count; i++)
                    {
                        
                        try // this will throw a cross threading error - call invoke to get around this
                        {
                            setCPU1TRShow(availableProcessesList[i].ServiceTime);
                        }
                        catch
                        {
                            Invoke(new Action(() => { setCPU1TRShow(availableProcessesList[i].ServiceTime); cpu1ProcessExec.Text = availableProcessesList[i].ProcessID; }));
                        }
                        //hrrnProcessList.RemoveAt(0);
                        HRRNwaitlist.RemoveAt(0); // removes item at the top of the waitlist for waitlistProcessQueue purposes
                        ThreadSim.executeHRRN(availableProcessesList[i], Decimal.ToInt32(this.timeUnitUpDown.Value));
                        availableProcessesList[i].IntFinishTime = secondsElapsed;
                        hrrnFinishedProcess.Add(availableProcessesList[i]);
                    }
                    availableProcessesList.Clear();
                }
            }
        }
        // calculates response ratios for hrrn
        public void calculateResponseRatio(List<Process> processList)
        {
            for (int i = 0; i < processList.Count; i++)
            {
                var waitingTime = Convert.ToDouble(DateTime.Now.Ticks - processList[i].AvailableProcessesTime.Ticks) / 10000000;
                processList[i].ResponseRatio = (waitingTime + processList[i].ServiceTime) / processList[i].ServiceTime;
            }
            processList.Sort();
            
        }

        // on every tick (1 second by default) this runs
        private void timeElapsed_Tick(object sender, EventArgs e)
        {
            secondsElapsed++; // variable to store how many "ticks" have passed based on GUI timer
            Console.WriteLine("Ticks elapsed: " + secondsElapsed);

            //recalculate data table for data grid (round robin)
            DataTable table = new DataTable(); // table used to populate data grid
            String[] headers = { "Process Name", "Service Time" };
            String[] l = System.IO.File.ReadAllLines(file);

            // set up headers

            foreach (String header in headers)
            {
                table.Columns.Add(header, typeof(String));
            }

            // for each line in csv
            for (int i = 0; i < processList.Count; i++)
            {
                if (processList.ElementAt<Process>(i).ArriveTime <= secondsElapsed)
                {
                    DataRow row = table.NewRow();
                    table.Rows.Add(processList.ElementAt<Process>(i).ProcessID, processList.ElementAt<Process>(i).ServiceTime);
                }
            }

            // initialize log table with these values

            rrWaitProcessQueue.DataSource = table;

            // reinitialize Round Robin dataLog table
            // populate the beginnings of the log data table if processes have already finished
            if (rrFinishedProcess.Count() > 0)
            {
                table = new DataTable(); // table used to populate data grid
                String[] headers2 = { "Process Name", "Arrival Time", "Service Time", "Finish Time (Ticks)", "TAT", "nTAT" };

                // set up headers for data table

                foreach (String header in headers2)
                {
                    table.Columns.Add(header, typeof(String));
                }

                // for each line in csv
                rrAvgNTAT = 0; // clear this every time this is checked
                foreach (Process p in rrFinishedProcess)
                {
                    DataRow row = table.NewRow();
                    double n = ((double)p.IntFinishTime*10 / (double)p.InitialServiceTime*10)/100; // calculate a double for this process's nTAT
                    table.Rows.Add(p.ProcessID, p.ArriveTime, p.InitialServiceTime, p.IntFinishTime, p.TAT, n.ToString());
                    rrAvgNTAT += p.IntFinishTime / p.InitialServiceTime;
                }
                rrAvgNTAT = rrAvgNTAT / rrFinishedProcess.Count;
                // initialize log table with these values
                rrDatalog.DataSource = table;
                nTATlabelRR.Text = rrAvgNTAT.ToString();

            }

            // HRRN updates

            //recalculate data table for data grid (hrrn)
            DataTable table4 = new DataTable(); // table used to populate data grid
            String[] headers4 = { "Process Name", "Service Time" };
            String[] l4 = System.IO.File.ReadAllLines(file);

            // set up headers

            foreach (String header in headers)
            {
                table4.Columns.Add(header, typeof(String));
            }

            // check to see if the currently executing thread is in hrrnProcessList
 //           if (thread2.Name != null)
 //           {
 //               foreach (Process p in hrrnProcessList)
 //               {
 //
  //                  if (thread2.Name.Equals(p.ProcessID))
 //                   {
 //                       hrrnProcessList.Remove(p);
 //                       break;
 //                   }
 //               }
 //           }
            


            // for each line in csv
            if (HRRNwaitlist.Count > 0)
            {
                for (int i = 0; i < HRRNwaitlist.Count; i++)
                {
                    if (HRRNwaitlist.ElementAt<Process>(i).ArriveTime <= secondsElapsed)
                    {
                        DataRow row = table4.NewRow();
                        
                        table4.Rows.Add(HRRNwaitlist.ElementAt<Process>(i).ProcessID, HRRNwaitlist.ElementAt<Process>(i).ServiceTime);
                        
                        
                    }
                }
            }


            // initialize log table with these values

            HRRNWaitProcessQueue.DataSource = table4;
            

            // reinitialize hrrn finished table
            // populate the beginnings of the log data table if processes have already finished
            hrrnAvgNTAT = 0; // reset hrrn ntat variable
            if (hrrnFinishedProcess.Count() > 0)
            {
                table = new DataTable(); // table used to populate data grid
                String[] headers2 = { "Process Name", "Arrival Time", "Service Time", "Finish Time (Ticks)", "TAT", "nTAT" };

                // set up headers for data table

                foreach (String header in headers2)
                {
                    table.Columns.Add(header, typeof(String));
                }

                // for each line in csv
                foreach (Process p in hrrnFinishedProcess)
                {
                    DataRow row = table.NewRow();
                    table.Rows.Add(p.ProcessID, p.ArriveTime, p.InitialServiceTime, p.IntFinishTime, p.TAT, (double)p.IntFinishTime / (double)p.InitialServiceTime);
                    hrrnAvgNTAT += (double)p.IntFinishTime / (double)p.InitialServiceTime; ;
                }
                hrrnAvgNTAT = (double)hrrnAvgNTAT / (double)hrrnFinishedProcess.Count();
                // initialize log table with these values
                datalogHRRN.DataSource = table;
                nTATLabelHRRN.Text = hrrnAvgNTAT.ToString();

            }

            // check to see if name needs to be updated
            cpu1ProcessExec.Text = thread2.Name;
            cpu2ProcessExec.Text = thread1.Name;

            // decrement time remaining shown 
            if (Int32.Parse(cpu1TRShow.Text) > 0)
            {
                cpu1TRShow.Text = (Int32.Parse(cpu1TRShow.Text) - 1).ToString();
            }
            if (Int32.Parse(cpu2TRShow.Text) > 0)
            {
                cpu2TRShow.Text = (Int32.Parse(cpu2TRShow.Text) - 1).ToString();
            }

        }
        // change time remaining text for HRRN
        private void setCPU1TRShow(int time)
        {
            cpu1TRShow.Text = time.ToString();
        }
        // change time remaining text for RR
        private void setCPU2TRShow(int time)
        {
            cpu2TRShow.Text = time.ToString();
        }
    }
}