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
        bool running = false; // bool to track if threads are running
        string file; // string to store csv
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

            // datagrid generation assumes a non-blank csv file is used

            // populate datagrid with initial data

            // read in data 
            
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
            
            if (started == false)
            {
                // set the time (in ms) that the threads will run at for the whole program
                timeElapsed.Interval = Decimal.ToInt32(this.timeUnitUpDown.Value);

                // populate the beginnings of the log data table
                DataTable table = new DataTable(); // table used to populate data grid
                String[] headers = { "Process Name", "Arrival Time", "Service Time", "Finish Time", "TAT", "nTAT" };
                String[] l = System.IO.File.ReadAllLines(file);

                // set up headers for data table

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
                datalogHRRN.DataSource = table;
                
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
                
                // initialize HRRN wait process queue grid with these values
                HRRNWaitProcessQueue.DataSource = table2;
                
                // initialize starting threads
                this.sysStatLabel.Text = "System Running"; // changes text on system status
                // this assumes thread1 is meant to start at time 0 and there is at least one thread
                thread1 = new Thread(new ThreadStart(SelectProcess));

                // if there is more than one process load it onto a second thread - in phase 3 final the establishment for this will change slightly
                if (processList.Count > 1)
                {
                    thread2 = new Thread(new ThreadStart(SelectProcess));
                }
                
                // begin running threads

                thread1.Start(); // start thread 1 execution
                timeElapsed.Start(); // start the GUI-side timer
                this.cpu1ProcessExec.Text = thread1.Name; // set the text denoting what process is running based on this process

                if (processList.Count > 1)
                {
                    thread2.Start();
                    this.cpu2ProcessExec.Text = thread2.Name;
                }
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
            while (processList.Count != 0)
            {
                var process = processList.Dequeue();
                ThreadSim.executeProcess(process, Decimal.ToInt32(this.timeUnitUpDown.Value));
                
               
            }
        }

        // on every tick (1 second by default) this runs
        private void timeElapsed_Tick(object sender, EventArgs e)
        {
            secondsElapsed++; // variable to store how many "ticks" have passed based on GUI timer
            Console.WriteLine("Ticks elapsed: " + secondsElapsed);

            //recalculate data table for data grids
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
            HRRNWaitProcessQueue.DataSource = table;
            //int temp = Convert.ToInt32(cpu1TRShow.ToString()) - 1;
            //cpu1TRShow.Equals(temp);

            // check to see if name needs to be updated
            cpu1ProcessExec.Text = thread1.Name;
            cpu2ProcessExec.Text = thread2.Name;

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
