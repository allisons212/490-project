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
        static Queue<Process> CPU1processList = new Queue<Process>(); // list of processes pulled from csv for thread 1 (HRRN)
        static Queue<Process> CPU2processList = new Queue<Process>(); // list of processes pulled from csv for thread 2 (RR)
        Thread thread1; // first thread (HRRN)
        Thread thread2; // second thread (RR)
        bool started = false; // bool tracking if process has started already
        
        /**
        * default constructor for Form1
        */
        public Form1()
        {
            // initialize GUI
            InitializeComponent();

            // ask user to choose what file they want to run program off of
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;

            // load processList with data from 
            CPU1processList = Parser.readProcessFile(file);
            CPU2processList = Parser.readProcessFile(file);

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
                
                table.Rows.Add(CPU1processList.ElementAt<Process>(i).ProcessID, CPU1processList.ElementAt<Process>(i).ArriveTime, CPU1processList.ElementAt<Process>(i).ServiceTime);
                
                
            }


            // initialize log table with these values
            datalogHRRN.DataSource = table;


            // this section needs to be updated so that it only initializes with processes at time 0
            // then can update table based on time variable - front end or back end depending on where that is stored, needs to be closely tied to the execute thread

            // wait process queue initialization

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
                if (CPU1processList.ElementAt<Process>(i).ArriveTime == 0)
                {
                    DataRow row = table2.NewRow();
                    table2.Rows.Add(CPU1processList.ElementAt<Process>(i).ProcessID, CPU1processList.ElementAt<Process>(i).ServiceTime);
                }
            }

            // initialize log tables with these values - since both are using the same dataset, can initialize with same table
            HRRNWaitProcessQueue.DataSource = table2;
            rrWaitProcessQueue.DataSource = table2;

            // this assumes thread1 is meant to start at time 0 and there is at least one thread
            thread1 = new Thread(new ThreadStart(SelectProcess));
            thread2 = new Thread(new ThreadStart(SelectProcess2));

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
                
                // begin running threads

                thread1.Start();
                this.cpu1ProcessExec.Text = thread1.Name;

                thread2.Start();
                this.cpu2ProcessExec.Text = thread2.Name;

                timeElapsed.Start();
                started = true;
            }
            else
            {
                if (thread1.IsAlive == false) // checks to see if thread is running
                {
                    thread1.Resume();
                }
                if (thread2.IsAlive == false) // checks to see if thread is running
                {
                    thread2.Resume();
                }
                timeElapsed.Enabled = true;
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
         * event for every second in the program (once the timer is initialized and started)
         */
        public static void timeEvent(object source, EventArgs e)
        {
            
        }

        /**
         * event handler code for when the pause system button is clicked
        */
        public delegate void PauseSysButton_ClickEventHandler(object source, EventArgs args);
        public event PauseSysButton_ClickEventHandler PauseSysClicked;
        private void PauseSysButton_Click(object sender, EventArgs e)
        {
            this.sysStatLabel.Text = "System Paused"; // changes text on system status
            timeElapsed.Enabled = false;
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

        

        // Summary: Thread will select a new process from the queue of the processList input
        // and then send the process to be executed. (HRRN)
        // Params: processList
        // Return: None
        public void SelectProcess()
        {
            while (CPU1processList.Count != 0)
            {
                var process = CPU1processList.Dequeue();
                //cpu1TRShow.Text = (process.ServiceTime).ToString();
                cpu1ProcessExec.Text = process.ProcessID;
                hrrnThreadSim.executeProcess(process, Decimal.ToInt32(this.timeUnitUpDown.Value));
            }
        }

        // Summary: same as the former function (SelectProcess) but for the round robin thread
        // Params: processList
        // Return: None
        public void SelectProcess2()
        {
            while (CPU2processList.Count != 0)
            {
                var process2 = CPU2processList.Dequeue();
                //cpu2TRShow.Text = (process2.ServiceTime).ToString();
                cpu2ProcessExec.Text = process2.ProcessID;
                rrThreadSim.executeProcess(process2, Decimal.ToInt32(this.timeUnitUpDown.Value));
            }
        }

        // on every tick (1 second) this runs
        private void timeElapsed_Tick(object sender, EventArgs e)
        {
            //int temp = Convert.ToInt32(cpu1TRShow.ToString()) - 1;
            //cpu1TRShow.Equals(temp);
        }
    }

    


}
