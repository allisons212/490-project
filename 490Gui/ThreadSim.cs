using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace _490Gui
{
	/**
	Summary: ThreadSim class is the class that contains the execution methods of a process and related 
		computations for the process in addition to a console report for debugging
	*/
	public class ThreadSim
	{
		public static int counter = 0; // integer used in current throughput calculation
		public static TimeSpan TATSummation = default; // summation of tat for all processes
		public static int TATCounter = 0; // counts the number of proceses for tat calculation
		public static TimeSpan avgTATComputation = default; // the time span average of a tat system


		public ThreadSim()
        {

        }

		// Summary: Original execution for each processes before HRRN or RR
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

		// Summary: Prints to console for each process execution by using the RR algorithm
		// where each process is assigned a fixed time slot in a cyclic way
		// Params: Process, int
		// Return: bool 
		public static bool executeRoundRobin(Process process, int miliseconds, int quantumTime)
		{
			int numberOfExecutions = 0; // keeps track of how many times a process runs
			bool executedServiceTime = false; // initially set executed service time as false
			process.ProcessThread = Thread.CurrentThread.ManagedThreadId; // allows for thread id to be recalled
			Thread.CurrentThread.GetType().GetField("m_Name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Thread.CurrentThread, process.ProcessID); // set name for thread; this allows name to be regularly changed
			Console.WriteLine(process.ProcessID + " is on thread " + process.ProcessThread); // shows what thread a process is on

			if (process.ServiceTime <= quantumTime) // checks to see if service time is less than quantum time
			{
				for (int i = 0; i < process.ServiceTime; i++) // runs the processes til end of service time 
				{
					//Console.WriteLine(process.ProcessID + " is executing with RR. Count: " + numberOfExecutions);
					Thread.Sleep(miliseconds);
					executedServiceTime = true;
					numberOfExecutions++;
				}
				process.FinishTime = DateTime.Now; // documents time a process leaves a system
				process.TAT = Process.computeTAT(process.EntryTime, process.FinishTime); // computes tat for each process
				TATSummation += process.TAT; // used for the average system tat computation
				TATCounter++; // used for the average system tat computation
				avgTATComputation = new TimeSpan(TATSummation.Ticks / TATCounter); // computation for average tat
				//Console.WriteLine(process.ProcessID + "'s tat is " + process.TAT);
				//Console.WriteLine("current avg tat " + avgTATComputation);
				long trial = computeCurrentThroughput(process.EntryTime); 			

				//*******************
				// THIS REPORT IS PRINTED TO CHECK DATA
				//*******************
				Console.WriteLine("*************        CPU           RR******************");
				printReport(process);

			}
			else
            {
				for (int i =0; i< quantumTime; i++) // process is using quantum time as max time to execute
                {
					//Console.WriteLine(process.ProcessID + " is executing with RR. Count: " + numberOfExecutions);
					Thread.Sleep(miliseconds);
					executedServiceTime = false;
					numberOfExecutions++;
				}
            }
			//Console.WriteLine(process.ProcessID + " is executing with RR. Count: " + numberOfExecutions);
			counter++;
			return executedServiceTime; // returns true or false 
		}

		// Summary: Prints to console for each process execution with hrrn algorithm
		// by calculating response ratio to compare long waiting time with short execution times of processes
		// to determine which process to go next 
		// Params: Process, int
		// Return: None
		public static void executeHRRN(Process process, int miliseconds)
		{
			process.EntryTime = DateTime.Now; // process entry time 
			process.ProcessThread = Thread.CurrentThread.ManagedThreadId; // sets the thread a process is on 
			Thread.CurrentThread.GetType().GetField("m_Name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Thread.CurrentThread, process.ProcessID); // set name for thread; this allows name to be regularly changed
			Console.WriteLine(process.ProcessID + " is on thread " + process.ProcessThread); // recalls which thread a process is on


			for (int i = 0; i < process.ServiceTime; i++) // runs for the length of service time 
			{
				// Console.WriteLine(process.ProcessID + " is executing with HRRN. Count: " + i);
				Thread.Sleep(miliseconds);
			}
			process.FinishTime = DateTime.Now; // records finish time 
			process.TAT = Process.computeTAT(process.EntryTime, process.FinishTime); // computes tat for each process
			TATSummation += process.TAT; // used for the average system tat computation
			TATCounter++; // used for the average system tat computation
			avgTATComputation = new TimeSpan(TATSummation.Ticks / TATCounter); // computation for average tat
			// Console.WriteLine(process.ProcessID + "'s tat is " + process.TAT);
			// Console.WriteLine("current avg tat " + avgTATComputation);


			counter++;

			//*******************
			// THIS REPORT IS PRINTED TO CHECK DATA
			//*******************
			Console.WriteLine("*************           CPU       HRRN******************");
			printReport(process);

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
			Console.WriteLine("Current Average System nTAT:" + avgTATComputation);
			Console.WriteLine("*************End This Process Report******************");
		}
	}
}