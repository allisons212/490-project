using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace _490Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(dummyProcess));
            t.Start();
            Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void dummyProcess()
        {
            string filePath = "C:/Users/Sierra Laney/Desktop/test.csv";
            var processList = new Queue<Process>();
            processList = Parser.readProcessFile(filePath);
            ThreadSim.executeProcess(processList, 1000);

        }

    }
}
