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

#pragma warning disable CS0618 // Type or member is obsolete

namespace _490Gui
{
    public partial class Form1 : Form
    {
        String[] processesChosen;
        static Queue<Process> processList = new Queue<Process>();
        
        Thread thread1;
        Thread thread2;

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

            foreach(String header in headers)
            {
                table.Columns.Add(header, typeof(String));
            }

            // for each line in csv
            for (int i = 0; i < l.Length; i++)
            {
                DataRow row = table.NewRow();
                table.Rows.Add(processList.ElementAt<Process>(i).processID, processList.ElementAt<Process>(i).arrivalTime, processList.ElementAt<Process>(i).serviceTime);
            }


            // initialize log table with these values
            dataGridView2.DataSource = table;
            

            // initialize starting threads
            
            thread1 = new Thread(new ThreadStart(selectProcess));

            thread2 = new Thread(new ThreadStart(selectProcess));

            // begin running threads

            thread1.Start();
            thread2.Start();
        }

        // events for GUI

        public delegate void waitProcessQueueUpdateEventHandler(object source, EventArgs args);
        public event waitProcessQueueUpdateEventHandler wPQUpdate;
        private void wPQ_Update(object sender, EventArgs e)
        {
            
        }

        protected virtual void OnWPQUpdate()
        {
            
        }

        public delegate void StartSys_ClickEventHandler(object source, EventArgs args);
        public event StartSys_ClickEventHandler StartSysClicked;
        private void StartSys_Click(object sender, EventArgs e)
        {
            this.sysStatLabel.Text = "System Running"; // changes text on system status
            if (thread1.IsAlive == false)
            {
                thread1.Resume();
            }
            if (!thread2.IsAlive == false)
            {
                thread2.Resume();
            }
        }
        protected virtual void OnStartSysClick()
        {
            if (StartSysClicked != null)
            {
                StartSysClicked(this, EventArgs.Empty);
            }
        }

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

        public delegate void NumericUpDown1_ValueChangedEventHandler(object source, EventArgs args);
        public event NumericUpDown1_ValueChangedEventHandler numUpDown1ValChanged;
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // event for if time-tracking menu changes
            // call threamSim setTimeUnit
            //ThreadSim.setTimeUnit(numericUpDown1.Value);
        }

        

        // Summary: Thread will select a new process from the queue
        // and then send the process to be executed.
        // Params: None
        // Return: None
        public static void selectProcess()
        {
            while (processList.Count != 0)
            {
                //tempName = processList.ElementAt<Process>(0).processID;
                var process = processList.Dequeue();

                ThreadSim.executeProcess(process, 1000);
            }
        }

        private void sysStatLabel_Click(object sender, EventArgs e)
        {

        }
    }

    


}
