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
		public static TimeSpan TATSummation = default;
		public static int TATCounter = 0;
		public static TimeSpan avgTATComputation = default;

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

           
			

			for (int i = 0; i < process.ServiceTime; i++)
			{
				Console.WriteLine(process.ProcessID + " is executing.");
				Thread.Sleep(miliseconds);
			}
			process.FinishTime = DateTime.Now;

			counter++;

			long trial = computeCurrentThroughput(process.EntryTime);
		}

		// Summary: Prints to console for each process execution
		// Params: Process, int
		// Return: None
		public static bool executeRoundRobin(Process process, int miliseconds, int quantumTime)
		{
			//if (process.EntryTime != null)
			//		process.EntryTime = DateTime.Now;
			//process.EntryTime = DateTime.Now;
			int numberOfExecutions = 0;
			bool executedServiceTime = false;
			process.ProcessThread = Thread.CurrentThread.ManagedThreadId;
			Thread.CurrentThread.GetType().GetField("m_Name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Thread.CurrentThread, process.ProcessID); // set name for thread; this allows name to be regularly changed
			Console.WriteLine(process.ProcessID + " is on thread " + process.ProcessThread);

			if (process.ServiceTime <= quantumTime)
			{
				for (int i = 0; i < process.ServiceTime; i++)
				{
					Console.WriteLine(process.ProcessID + " is executing with RR. Count: " + numberOfExecutions);
					Thread.Sleep(miliseconds);
					executedServiceTime = true;
					numberOfExecutions++;
				}
				process.FinishTime = DateTime.Now;
				process.TAT = Process.computeTAT(process.EntryTime, process.FinishTime); // computes tat for each process
				TATSummation += process.TAT; // used for the average system tat computation
				TATCounter++; // used foro the average system tat computation
				avgTATComputation = new TimeSpan(TATSummation.Ticks / TATCounter); // computation for average tat
				Console.WriteLine(process.ProcessID + "'s tat is " + process.TAT);
				Console.WriteLine("current avg tat " + avgTATComputation);
				long trial = computeCurrentThroughput(process.EntryTime);

			}
			else
            {
				for (int i =0; i< quantumTime; i++)
                {
					Console.WriteLine(process.ProcessID + " is executing with RR. Count: " + numberOfExecutions);
					Thread.Sleep(miliseconds);
					executedServiceTime = false;
					numberOfExecutions++;
				}
				//process.ServiceTime -= quantumTime;
            }
			//Console.WriteLine(process.ProcessID + " is executing with RR. Count: " + numberOfExecutions);


			counter++;

			return executedServiceTime;
		}

		// Summary: Prints to console for each process execution
		// Params: Process, int
		// Return: None
		public static void executeHRRN(Process process, int miliseconds)
		{
			process.EntryTime = DateTime.Now;
			process.ProcessThread = Thread.CurrentThread.ManagedThreadId;
			Thread.CurrentThread.GetType().GetField("m_Name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Thread.CurrentThread, process.ProcessID); // set name for thread; this allows name to be regularly changed
			Console.WriteLine(process.ProcessID + " is on thread " + process.ProcessThread);


			for (int i = 0; i < process.ServiceTime; i++)
			{
				Console.WriteLine(process.ProcessID + " is executing.");
				Thread.Sleep(miliseconds);
			}
			process.FinishTime = DateTime.Now;
			process.TAT = Process.computeTAT(process.EntryTime, process.FinishTime); // computes tat for each process
			TATSummation += process.TAT; // used for the average system tat computation
			TATCounter++; // used foro the average system tat computation
			avgTATComputation = new TimeSpan(TATSummation.Ticks / TATCounter); // computation for average tat
			Console.WriteLine(process.ProcessID + "'s tat is " + process.TAT);
			Console.WriteLine("current avg tat " + avgTATComputation);


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

		// Summary: Sums the tat time of each process that have exited and divides it by the number of processes that have exited
		// Params: timespan array
		// Return: timespan
		public TimeSpan computeAvgTAT(TimeSpan[] TATs)
		{
			TimeSpan sumTAT = default;
			int counter = 0;

			for (int i=0; i < TATs.Length; i++)
            {
				sumTAT += TATs[i];
				counter++;
            }

			TimeSpan avgTAT = new TimeSpan(sumTAT.Ticks / counter);

			return avgTAT;
		}
	}
}