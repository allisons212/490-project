using System;
using System.Threading;
using System.Threading.Tasks;
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
            process.EntryTime = DateTime.Now;
            process.ProcessThread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(process.ProcessID + " is on thread " + process.ProcessThread);

            for (int i = 0; i < process.ServiceTime; i++)
            {
                // Console.WriteLine(process.ProcessID + " is executing.");
                Thread.Sleep(miliseconds);
            }
            process.FinishTime = DateTime.Now;
            counter++;

            //*******************
            // THIS REPORT IS PRINTED TO VIEW DATA IN LIEU OF GUI
            //*******************
            printReport(process);
        }

        // Summary: Prints all necessary data of processes to show data is being calculated
        // Params Process
        // Reuturn: None
        private static void printReport(Process process)
        {
            Console.WriteLine("*************Begin This Process Report******************");
            Console.WriteLine("Process Name: " + process.ProcessID);
            Console.WriteLine("Arrival Time: " + process.ArriveTime);
            Console.WriteLine("Service Time: " + process.ServiceTime);
            Console.WriteLine("Finish Time: " + process.FinishTime);
            TimeSpan TAT = Process.computeTAT(process.EntryTime, process.FinishTime);
            Console.WriteLine("Current TAT: " + TAT);
            TimeSpan nTAT = Process.computeNTAT(TAT, process.ServiceTime);
            Console.WriteLine("Current nTAT: " + nTAT);
            float throughput = computeCurrentThroughput(Program.getPrgmStrtTime());
            Console.WriteLine("Current Throughput: " + throughput);
            Console.WriteLine("*************End This Process Report******************");
        }

        // Summary: Calculates the number of processes completed since program start
        // Params: DateTime
        // Return: The number of processes completed since program start
        private static float computeCurrentThroughput(DateTime programStartTime)
		{
			TimeSpan timeDifference = DateTime.Now - programStartTime;
			float secondsSinceStart = timeDifference.Ticks / 10000000;
			float currentTP = counter / secondsSinceStart;
			// Console.WriteLine(currentTP);
			return (currentTP);
		}

		// Summary: Calculates the number of processes completed between two given times
		// Params: DateTime, DateTime, Array of Processes
		// Return: The number of processes completed
		private static int computeThroughput(DateTime time1, DateTime time2, Process[] reportArray)
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

