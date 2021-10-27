using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace _490Gui
{
    static class Program
    {

        //static Queue<Process> processList = new Queue<Process>();
        [STAThreadAttribute] // needed to run openFileDialog in Form1.cs
        static void Main(string[] args)
        {
            DateTime programStartTime = DateTime.Now;

            // comment block is code for if running code to console without GUI

            /**string filePath = "C:/Users/cowca/Documents/490csv.csv";
            processList = Parser.readProcessFile(filePath);

            Thread thread1 = new Thread(new ThreadStart(selectProcess));
            Thread thread2 = new Thread(new ThreadStart(selectProcess));
            thread1.Start();
            thread2.Start();
            Console.ReadLine();**/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // generate new form

            Form1 f = new Form1();

            // make form sit at top of windows

            f.TopMost = true;

            // run application and form

            Application.Run(f);
        }

        
        

    }

}