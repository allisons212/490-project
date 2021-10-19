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
				Console.WriteLine(process.processID + " is beginning execution.");
				process.entryTime = DateTime.Now;
				for (int i = 0; i < process.serviceTime; i++)
				{
					Console.WriteLine(process.processID + " is executing.");
					Thread.Sleep(miliseconds);
				}
				process.finishTime = DateTime.Now;
				counter++;

			long trial = computeCurrentThroughput(process.entryTime);
			TimeSpan tat = computeTAT(process.entryTime, process.finishTime);
			TimeSpan ntat = computeNTAT(tat, process.serviceTime);
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
                if (process.finishTime > time1 && process.finishTime < time2)
                {
                    counter++;
                }
            }
			Console.WriteLine(counter);
            return counter;
		}

		// Summary: Calculates the elapsed time from when a process arrived to when
		// the process finishes
		// Params: DateTime, DateTime
		// Return: TimeSpan
		public static TimeSpan computeTAT(DateTime time1, DateTime time2)
        {
			TimeSpan tat = time2 - time1;
			Console.WriteLine(tat);
			return tat; 
        }

		// Summary: Computes how long a process waits relative to the 
		// amount of time it takes to execute
		// Params: TimeSpan, int
		// Return: int
		public static TimeSpan computeNTAT(TimeSpan timespan, int serviceTime)
        {
			TimeSpan nTAT = new TimeSpan(timespan.Ticks / serviceTime);
			Console.WriteLine(nTAT);
			return nTAT; 
        }
	}
}

