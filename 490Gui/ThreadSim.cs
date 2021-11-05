using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace _490Gui
{
	public class ThreadSim
	{
		public static int counter = 0;

		public ThreadSim()
        {

        }

		// Summary: Prints to console for each process execution
		// Params: Process, int
		// Return: None
		public static void executeProcess(Process process, int miliseconds)
		{
			process.EntryTime = DateTime.Now;
			process.ProcessThread = Thread.CurrentThread.ManagedThreadId;
            Thread.CurrentThread.GetType().GetField("m_Name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Thread.CurrentThread, process.ProcessID); // set name for thread; this allows name to be regularly changed
            Console.WriteLine(process.ProcessID + " is on thread " + process.ProcessThread);

            // set names on GUI events
            if (process.ProcessThread == 3)
            {
                // activate CPU1PanelUpdate method 
            }

            if (process.ProcessThread == 4)
            {
                // activate CPU2PanelUpdate method
            }
			

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
			// Console.WriteLine(currentTP);
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
			// Console.WriteLine(counter);
			return counter;
		}
	}
}