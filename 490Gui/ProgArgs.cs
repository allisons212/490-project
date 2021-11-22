using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _490Gui
{
    public class ProgArgs: EventArgs
    {
        // gets/sets a threadobj to be used with the gui
        public ThreadSim ThreadObj { get; set; }

        // gets/sets a list of processes
        public Queue<Process> ProcessList { get; set; }
    }
}
