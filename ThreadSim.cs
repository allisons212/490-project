using System;
using System.Threading;
// using System.Threading.Tasks;
using System.Collections.Generic;


public class ThreadSim
{
	int tpNumber;

	public ThreadSim()
	{
		Process[] reportArray = new Process[] { }
	}

	public static void executeProcess(Queue<Process> processQueue, int miliseconds)
	{
		int counter = 0; 
		foreach (Process process in processQueue)
		{
			Console.WriteLine(process.processID + " is beginning execution.");
			process.entryTime = DateTime.Now;
			for (int i = 0; i < process.serviceTime; i++)
			{
				Console.WriteLine(process.processID + " is executing.");
				Wait(miliseconds);
			}
			process.exitTime = DateTime.Now;
			reportArray[counter] = process;
			counter++; 
		}

	}

	public int computeThroughput(int time1, int time2, Process[] reportArray)
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
