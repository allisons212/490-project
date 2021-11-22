using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace _490Gui
{
    static class Program
    {

        // holds the start time of a program
        public static DateTime programStartTime;
        [STAThreadAttribute] // needed to run openFileDialog in Form1.cs
        static void Main(string[] args)
        {
            // saves the start time of a program
            programStartTime = DateTime.Now;

            // allows colors and fonts for the gui
            Application.EnableVisualStyles();

            // helps chose text rendering over graphics for gui
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