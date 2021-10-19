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
		public ThreadSim()
		{
			//Process[] reportArray = new Process[] { };
		}

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
				process.exitTime = DateTime.Now;
				// reportArray.Add(runningProcess);
		}
		

		public int computeThroughput(DateTime time1, DateTime time2, Process[] reportArray)
		{
			int counter = 0;
			foreach (Process process in reportArray)
			{
				if (process.exitTime > time1 && process.exitTime < time2)
				{
					counter++;
				}
			}
			return counter;
		}
	}
}

