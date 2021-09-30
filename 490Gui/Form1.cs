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

        private void StartSys_Click(object sender, EventArgs e)
        {
            // will probably want to run open file dialog if this is first time opening event 
            sysStatLabel.Text = "System Running";
        }

        private void PauseSysButton_Click(object sender, EventArgs e)
        {
            sysStatLabel.Text = "System Paused";
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

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
