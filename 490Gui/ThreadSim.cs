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
		public static int counter = 0;

		// Summary: Prints to console for each process execution
		// Params: Process, int
		// Return: None
		public static void executeProcess(Process process, int miliseconds)
		{
				Console.WriteLine(process.ProcessID + " is beginning execution.");
				process.EntryTime = DateTime.Now;
				for (int i = 0; i < process.ServiceTime; i++)
				{
					Console.WriteLine(process.ProcessID + " is executing.");
					Thread.Sleep(miliseconds);
				}
				process.FinishTime = DateTime.Now;
				counter++;

			long trial = computeCurrentThroughput(process.EntryTime);
		}
		
		// Summary: Calculates the number of processes completed since program start
		// Params: DateTime
		// Return: The number of processes completed since program start
		public static long computeCurrentThroughput(DateTime programStartTime)
		{
			TimeSpan timeDifference = DateTime.Now - programStartTime;
			float secondsSinceStart = timeDifference.Ticks / 10000000;
			float currentTP = counter / secondsSinceStart;
			Console.WriteLine(currentTP);
			return (counter / programStartTime.Ticks);
		}

		// Summary: Calculates the number of processes completed between two given times
		// Params: DateTime, DateTime, Array of Processes
		// Return: The number of processes completed
		public static int computeThroughput(DateTime time1, DateTime time2, Process[] reportArray)
		{
            int counter = 0;
            foreach (Process process in reportArray)
            {
                if (process.FinishTime > time1 && process.FinishTime < time2)
                {
                    counter++;
                }
            }
			Console.WriteLine(counter);
            return counter;
		}

	}
}

