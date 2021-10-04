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
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AllocConsole();
            // cout hey what file
            // cin filepath
            string filePath;
            Console.WriteLine("Please type filepath: \n");
            filePath = Console.ReadLine();
            Console.WriteLine("Path is " + filePath);
            //string filePath = "C:/Users/Sierra Laney/Desktop/test.csv";
            var processList = new Queue<Process>();
            processList = Parser.readProcessFile(filePath);
            ThreadSim threadSimObj = new ThreadSim();
            ThreadSim.executeProcess(processList);
            Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    
}

