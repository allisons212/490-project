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

        private void StartSys_Click(object sender, EventArgs e)
        {
            // start code to begin/continue process
            sysStatLabel.Text = "System Running"; // changes text on system status
        }

        private void PauseSysButton_Click(object sender, EventArgs e)
        {
            // pause code if process is running
            sysStatLabel.Text = "System Paused"; // changes text on system status
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            // unneeded idk why this is here
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // unneeded idk why this is here
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            // unneeded idk why this is here
        }

        private void StartSys_MouseCaptureChanged(object sender, EventArgs e)
        {
            // unneeded
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // unneeded afaik
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // event for if time-tracking menu changes
        }

    }

    public class NumericUpDown1EventArgs : EventArgs
    {
        // event arg for time setting menu thing
    }

}
