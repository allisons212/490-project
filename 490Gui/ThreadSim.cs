using System;
using System.Threading;
using System.Threading.Tasks;
// using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace _490Gui
{
	public class ThreadSim
	{
		int tpNumber;
		static public int counter = 0;

		// Summary: Prints to console for each process execution
		// Params: Process, int
		// Return: None
		public static void executeProcess(Process process, int miliseconds)
		{
			// ArrayList reportArray = new ArrayList();
			// var runningProcess = processQueue.Dequeue();
				Console.WriteLine(process.processID + " is beginning execution.");
				process.entryTime = DateTime.Now;
				for (int i = 0; i < process.serviceTime; i++)
				{
					Console.WriteLine(process.processID + " is executing.");
					Thread.Sleep(miliseconds);
				}
				process.finishTime = DateTime.Now;
				counter++;
				// reportArray.Add(runningProcess);
		}
		
		// Summary: Calculates the number of processes completed since program start
		// Params: DateTime
		// Return: The number of processes completed since program start
		public int computeCurrentThroughput(DateTime programStartTime)
		{
			return counter / Convert.ToInt32(programStartTime);
		}

		// Summary: Calculates the number of processes completed between two given times
		// Params: DateTime, DateTime, Array of Processes
		// Return: The number of processes completed
		public int computeThroughput(DateTime time1, DateTime time2, Process[] reportArray)
		{
            int counter = 0;
            foreach (Process process in reportArray)
            {
                if (process.finishTime > time1 && process.finishTime < time2)
                {
                    counter++;
                }
            }
            return counter;
		}

		// Summary: Calculates the elapsed time from when a process arrived to when
		// the process finishes
		// Params: DateTime, DateTime
		// Return: TimeSpan
		public TimeSpan computeTAT(DateTime time1, DateTime time2)
        {
			TimeSpan tat = time2 - time1; 
			return tat; 
        }

		// Summary: Computes how long a process waits relative to the 
		// amount of time it takes to execute
		// Params: TimeSpan, int
		// Return: int

		public int computeNTAT(TimeSpan timespan, int serviceTime)
        {
			return Convert.ToInt32(timespan) / serviceTime; 
        }
	}
}

