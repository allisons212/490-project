using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace _490Gui
{
    static class Program
    {

        static Queue<Process> processList = new Queue<Process>();
        static void Main(string[] args)
        {
            DateTime programStartTime = DateTime.Now;
            string filePath = "C:/Users/Sierra Laney/Desktop/test.csv";
            processList = Parser.readProcessFile(filePath);
            
            Thread thread1 = new Thread(new ThreadStart(selectProcess));
            Thread thread2 = new Thread(new ThreadStart(selectProcess));
            thread1.Start();
            thread2.Start();
            Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        // Summary: Thread will select a new process from the queue
        // and then send the process to be executed.
        // Params: None
        // Return: None
        public static void selectProcess()
        {
            while (processList.Count != 0)
            {
                var process = processList.Dequeue();
                ThreadSim.executeProcess(process, 1000);
            }
        }

    }
}
