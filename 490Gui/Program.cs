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

        static Queue<Process> processList = new Queue<Process>();
        static void Main(string[] args)
        {
            // TODO ADD LOOP TO ITERATE THROUGH ALL PROCESSES 
            string filePath = "C:/Users/Sierra Laney/Desktop/test.csv";
            processList = Parser.readProcessFile(filePath);
            Thread thread1 = new Thread(new ThreadStart(dummyProcess));
            Thread thread2 = new Thread(new ThreadStart(dummyProcess));
            thread1.Start();
            thread2.Start();
            Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void dummyProcess()
        {
            var process = processList.Dequeue(); 
            ThreadSim.executeProcess(process, 1000);

        }

    }
}
