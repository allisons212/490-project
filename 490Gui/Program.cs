using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _490Gui
{
    static class Program
    {
        /**
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    **/
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //AllocConsole();
            // cout hey what file
            // cin filepath
            //string filePath;
            //Console.WriteLine("Please type filepath: \n");
            //filePath = Console.ReadLine();
            //Console.WriteLine("Path is " + filePath);
            //FreeConsole();
            string filePath = "C:/Users/cowca/Documents/490csv.csv";
            ProgArgs progArgs = new ProgArgs();
            progArgs.ProcessList =  Parser.ReadProcessFile(filePath);
            progArgs.ThreadObj = new ThreadSim();
            progArgs.ThreadObj.ExecuteProcess(progArgs.ProcessList);
            Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            // f += HandleButtonClick();
            Application.Run(f);
        }

        /**
        private void HandleButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag == "Start System")
                this.sysStatLabel.Text = "System Running";
            else if (btn.Tag == "Pause System")
                this.sysStatLabel.Text = "System Paused";
            // etc.
        } **/
    }

    
}

