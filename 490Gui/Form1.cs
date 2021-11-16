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
        Thread thread1; // first thread
        Thread thread2; // second thread
        int secondsElapsed; // time passed in program
        bool started = false; // bool tracking if process has started already
        /**
* default constructor for Form1
**/

        public Form1()
        {

            // initialize GUI
            InitializeComponent();

            // ask user to choose what file they want to run program off of
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;

            // load processList with data from 
            processList = Parser.readProcessFile(file);

            // datagrid generation assumes a non-blank csv file is used

            // populate datagrid with initial data

            // read in data 


            DataTable table = new DataTable(); // table used to populate data grid
            String[] headers = { "Process Name", "Arrival Time", "Service Time", "Finish Time", "TAT", "nTAT" };
            String[] l = System.IO.File.ReadAllLines(file);

            // set up headers

            foreach (String header in headers)
            {
                table.Columns.Add(header, typeof(String));
            }

            // for each line in csv
            for (int i = 0; i < l.Length; i++)
            {
                DataRow row = table.NewRow();
                table.Rows.Add(processList.ElementAt<Process>(i).ProcessID, processList.ElementAt<Process>(i).ArriveTime, processList.ElementAt<Process>(i).ServiceTime);
            }


            // initialize log table with these values
            dataGridView2.DataSource = table;


            // this section needs to be updated so that it only initializes with processes at time 0
            // then can update table based on time variable - front end or back end depending on where that is stored, needs to be closely tied to the execute thread

            DataTable table2 = new DataTable(); // table used to populate data grid
            String[] headers2 = { "Process Name", "Service Time"};
            String[] l2 = System.IO.File.ReadAllLines(file);

            // set up headers

            foreach (String header in headers2)
            {
                table2.Columns.Add(header, typeof(String));
            }

            // for each line in csv
            for (int i = 0; i < l2.Length; i++)
            {
                DataRow row = table2.NewRow();
                table2.Rows.Add(processList.ElementAt<Process>(i).ProcessID, processList.ElementAt<Process>(i).ServiceTime);
            }


            // initialize log table with these values
            dataGridView1.DataSource = table2;


            // initialize starting threads


            
        }

        // events for GUI

        /**
         * event handler code for when a process on cpu 1 needs to be changed
        */
        public delegate void updateCPUPanel1EventHandler(object source, EventArgs args);
        public event updateCPUPanel1EventHandler updateCPUPanel1;
        private void CPUPanel1_Update(object sender, EventArgs e)
        {
            this.cpu1ProcessExec.Text = thread1.Name;
        }
        protected virtual void OnCPUPanel1Update()
        {
            CPUPanel1_Update(this, EventArgs.Empty);
        }

        /**
         * event handler code for when a process on cpu 2 needs to be changed
        */
        public delegate void updateCPUPanel2EventHandler(object source, EventArgs args);
        public event updateCPUPanel2EventHandler updateCPUPanel2;
        private void CPUPanel2_Update(object sender, String s)
        {
            this.cpu2ProcessExec.Text = s;
        }
        protected virtual void OnCPUPanel2Update(String s)
        {
            CPUPanel2_Update(this, s);
        }

        /**
         * event handler code for when a process on waitlist queue needs to be changed
        */
        public delegate void waitProcessQueueUpdateEventHandler(object source, EventArgs args);
        public event waitProcessQueueUpdateEventHandler wPQUpdate;
        private void wPQ_Update(object sender, EventArgs e)
        {
            
        }

        protected virtual void OnWPQUpdate()
        {
            
        }

        /**
         * event handler code for when the start system button is clicked
        */
        public delegate void StartSys_ClickEventHandler(object source, EventArgs args);
        public event StartSys_ClickEventHandler StartSysClicked;
        private void StartSys_Click(object sender, EventArgs e)
        {
            this.sysStatLabel.Text = "System Running"; // changes text on system status
            if (started == false)
            {
                // this assumes thread1 is meant to start at time 0 and there is at least one thread
                thread1 = new Thread(new ThreadStart(SelectProcess));

                // 
                if (processList.Count > 1)
                {
                    thread2 = new Thread(new ThreadStart(SelectProcess));

                }


                // begin running threads

                thread1.Start();
                this.cpu1ProcessExec.Text = thread1.Name;

                if (processList.Count > 1)
                {
                    thread2.Start();
                    this.cpu2ProcessExec.Text = thread2.Name;
                }
                started = true;
            }
            else
            {
                if (thread1.IsAlive == false)
                {
                    thread1.Resume();
                }
                if (!thread2.IsAlive == false)
                {
                    thread2.Resume();
                }
            }
            
        }
        protected virtual void OnStartSysClick()
        {
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
            this.sysStatLabel.Text = "System Paused"; // changes text on system status

            if (thread1.IsAlive == true)
            {
                thread1.Suspend();
            }
            if (thread2.IsAlive == true)
            {
                thread2.Suspend();
            }
        }
        protected virtual void OnPauseSysClick()
        {
            if (PauseSysClicked != null)
            {
                PauseSysButton_Click(this, EventArgs.Empty);
            }
        }

        /**
         * event handler code for when a process on cpu 1 needs to be changed
        */
        public delegate void changeCPU1ExecutingStatusEventHandler(object source, EventArgs args);
        public event changeCPU1ExecutingStatusEventHandler cpu1ProcessUpdated;
        private void changeCPU1_ExecutingStatus(object sender, EventArgs e)
        {
            
        }
        protected virtual void OnCPU1ExecutingChange()
        {
            if (cpu1ProcessUpdated != null)
            {
                changeCPU1_ExecutingStatus(this, EventArgs.Empty);
            }
        }

        /**
         * event for when the value in the numeric up down box is changed - needs to affect the time unit calculations
        */
        public delegate void NumericUpDown1_ValueChangedEventHandler(object source, EventArgs args);
        public event NumericUpDown1_ValueChangedEventHandler numUpDown1ValChanged;
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // event for if time-tracking menu changes
            // call threamSim setTimeUnit
            
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
                        processList.Peek().EntryTime = DateTime.Now;
                    long executionTime = (processList.Peek().EntryTime.Ticks - Program.programStartTime.Ticks) / 10000000;
                    int arrivalTime = processList.Peek().ArriveTime;
                    //int counter;
                    // var process = processList.Dequeue();
                    if (arrivalTime <= executionTime)
                    {
                        process = processList.Dequeue();
                        counter--;
                        executedServiceTime = ThreadSim.executeRoundRobin(process, Decimal.ToInt32(this.numericUpDown1.Value), 5); //This would be changed to call HRRN and RR respectively TODO CHANGE THE 5 VALUE TO INPUT QUANTUM TIME
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
                        // Console.WriteLine("***********      " + processList.Peek().ProcessID + "      ************");
                    }
                    else
                    {
                        processList.Enqueue(process);
                        process.ServiceTime = process.ServiceTime - 5; //TODO CHANGE THIS VALUE TO INPUT QUANTUM TIME
                        counter++;
                    }
                }
            }

            if (Thread.CurrentThread.ManagedThreadId == 4)
            {
                List<Process> processListArray = processList.ToList();
                List <Process> availableProcessesList = new List<Process>();
                while (processListArray.Count != 0)
                    {
                    for (int i = 0; i < processListArray.Count; i++)
                    {
                        if (processListArray[i].ArriveTime <= (processListArray[i].EntryTime.Ticks - Program.programStartTime.Ticks) / 10000000)
                        {
                            availableProcessesList.Add(processListArray[i]);
                            processListArray[i].AvailableProcessesTime = DateTime.Now;
                            processListArray.Remove(processListArray[i]);
                        }
                    }
                    if (availableProcessesList.Count > 1)
                    {
                        calculateResponseRatio(availableProcessesList);
                    }
                    for (int i = 0; i < availableProcessesList.Count; i++)
                    {
                        ThreadSim.executeHRRN(availableProcessesList[i], Decimal.ToInt32(this.numericUpDown1.Value));
                        availableProcessesList.Remove(availableProcessesList[i]);
                    }
                }
            }
        }

        public void calculateResponseRatio(List<Process> processList)
        {
            for (int i=0; i<=processList.Count; i++)
            {
                var waitingTime = DateTime.Now.Ticks - processList[i].AvailableProcessesTime.Ticks;
                processList[i].ResponseRatio = (waitingTime + processList[i].ServiceTime) / processList[i].ServiceTime;
            }
            processList.Sort();
        }

        private void parserBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
