using System;
using System.Threading;
using System.Collections.Generic;

public class ThreadSim
{
	int tpNumber;
	public ThreadSim()
	{

	}

	public static void executeProcess(Queue<Process> processQueue)
	{
		foreach (Process process in processQueue)
		{
			Console.WriteLine(process.processID + " is beginning execution.");
			process.entryTime = DateTime.Now;
			for (int i = 0; i < process.serviceTime; i++)
			{
				Console.WriteLine(process.processID + " is executing.");
			}
			process.exitTime = DateTime.Now;
		}

	}

	public int computeThroughput(int time1, int time2)
	{

		return 0;
	}
}
