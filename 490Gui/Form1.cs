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
        static Queue<Process> hrrnProcessList = new Queue<Process>(); // list of processes for hrrn
        static List<Process> rrFinishedProcess = new List<Process>(); // completed processes on rr side
        static List<Process> hrrnFinishedProcess = new List<Process>(); // completed processes on hrrn side
        Thread thread1; // first thread
        Thread thread2; // second thread
        int secondsElapsed; // time passed in program
        bool started = false; // bool tracking if process has started already
        bool running = false; // bool to track if threads are running
        string file; // string to store csv
        int tempTime;
        
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
            hrrnProcessList = Parser.readProcessFile(file);

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
                
                // initialize RR wait process queue grid with these values
                rrWaitProcessQueue.DataSource = table2;

                DataTable table3 = new DataTable(); // table used to populate data grid
                String[] headers3 = { "Process Name", "Service Time" };
                String[] l3 = System.IO.File.ReadAllLines(file);

                // set up headers

                foreach (String header in headers3)
                {
                    table3.Columns.Add(header, typeof(String));
                }

                // for each line in csv
                for (int i = 0; i < l3.Length; i++)
                {
                    if (hrrnProcessList.ElementAt<Process>(i).ArriveTime == 0)
                    {
                        DataRow row = table3.NewRow();
                        table3.Rows.Add(hrrnProcessList.ElementAt<Process>(i).ProcessID, hrrnProcessList.ElementAt<Process>(i).ServiceTime);
                    }

                }

                HRRNWaitProcessQueue.DataSource = table3;

                // initialize starting threads
                this.sysStatLabel.Text = "System Running"; // changes text on system status
                // this assumes thread1 is meant to start at time 0 and there is at least one thread
                thread1 = new Thread(new ThreadStart(SelectProcess));
                cpu2TRShow.Text = tempTime.ToString();
                thread2 = new Thread(new ThreadStart(SelectProcess));
                
                
                // begin running threads

                thread1.Start(); // start thread 1 execution
                thread2.Start();
                
                timeElapsed.Start(); // start the GUI-side timer

                this.cpu1ProcessExec.Text = thread1.Name; // set the text denoting what process is running based on this process
                this.cpu2ProcessExec.Text = thread2.Name;

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
            bool executedServiceTime;
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
                List <Process> availableProcessesList = new List<Process>();
                List<Process> removeList = new List<Process>(); 
                while (hrrnProcessList.Count != 0)
                    {
                    
                    // Grab all items that are ready to be executed and add them to availableProcessList
                    for (int i = 0; i < hrrnProcessList.Count; i++)
                    {
                        var executionTime2 = (DateTime.Now.Ticks - Program.programStartTime.Ticks) / 10000000;
                        var arrivalTime2 = hrrnProcessList.ElementAt(i).ArriveTime;
                        if (hrrnProcessList.ElementAt(i).ArriveTime <= executionTime2)
                        {
                            hrrnProcessList.ElementAt(i).AvailableProcessesTime = DateTime.Now;
                            availableProcessesList.Add(hrrnProcessList.ElementAt(i));
                            removeList.Add(hrrnProcessList.ElementAt(i));
                        }
                    }

                    // Remove items from hrrnProcessList that will be executed
                    for (int i = 0; i<removeList.Count; i++)
                    {
                        if(hrrnProcessList.Contains(removeList[i]))
                        {
                            hrrnProcessList = new Queue<Process>(hrrnProcessList.Where(x => x != removeList[i]));
                        }
                    }
                    removeList.Clear(); //clear remove list so we can do it again

                    // If there are more than 1 items in availableProcessList, calculate the response ratio and sort them accordingly
                    if (availableProcessesList.Count > 1)
                    {
                        calculateResponseRatio(availableProcessesList);
                    }

                    // Execute all items in availableProcessList
                    for (int i = 0; i < availableProcessesList.Count; i++)
                    {
                        ThreadSim.executeHRRN(availableProcessesList[i], Decimal.ToInt32(this.timeUnitUpDown.Value));
                    }
                    foreach (Process p in availableProcessesList)
                    {
                        p.IntFinishTime = secondsElapsed;
                        hrrnFinishedProcess.Add(p);
                    }
                    availableProcessesList.Clear();
                }
            }
        }

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
                foreach (Process p in rrFinishedProcess)
                {
                    DataRow row = table.NewRow();
                    table.Rows.Add(p.ProcessID, p.ArriveTime, p.InitialServiceTime, p.IntFinishTime, p.TAT, p.NTAT);
                }
                
                // initialize log table with these values
                rrDatalog.DataSource = table;

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

            // for each line in csv
            for (int i = 0; i < hrrnProcessList.Count; i++)
            {
                if (hrrnProcessList.ElementAt<Process>(i).ArriveTime <= secondsElapsed)
                {
                    DataRow row = table4.NewRow();
                    table4.Rows.Add(hrrnProcessList.ElementAt<Process>(i).ProcessID, hrrnProcessList.ElementAt<Process>(i).ServiceTime);
                }
            }

            // initialize log table with these values

            HRRNWaitProcessQueue.DataSource = table;

            // reinitialize hrrn finished table
            // populate the beginnings of the log data table if processes have already finished
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
                    table.Rows.Add(p.ProcessID, p.ArriveTime, p.InitialServiceTime, p.IntFinishTime, p.TAT, p.NTAT);
                }

                // initialize log table with these values
                datalogHRRN.DataSource = table;

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
    }
}
