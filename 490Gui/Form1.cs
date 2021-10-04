using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _490Gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // events for GUI
        // GUI works, code works, just need to link the two

        private void StartSys_Click(object sender, ProgArgs e) //fix progArgs
        {
            // start code to begin/continue process
            e.ThreadObj.ExecuteProcess(e.ProcessList);

            sysStatLabel.Text = "System Running"; // changes text on system status
        }

        private void PauseSysButton_Click(object sender, EventArgs e)
        {
            // pause code if process is running
            sysStatLabel.Text = "System Paused"; // changes text on system status
        }


        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // event for if time-tracking menu changes
            // call threamSim setTimeUnit
        }

    }

    public class NumericUpDown1EventArgs : EventArgs
    {
        // event arg for time setting menu thing
    }


}
