using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _490Gui
{
    public class ProgArgs: EventArgs
    {
        public hrrnThreadSim ThreadObj { get; set; }

        public Queue<Process> ProcessList { get; set; }
    }
}
