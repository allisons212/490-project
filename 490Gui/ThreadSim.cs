using System;
using System.Threading;
using System.Collections.Generic;

public class ThreadSim
{
	int tpNumber;
    int timeUnit;
	public ThreadSim()
	{

	}

    public void SetTimeUnit(int time)
    {
        this.timeUnit = time;
    }

    public int GetTimeUnit()
    {
        return timeUnit;
    }

    public void ExecuteProcess(Queue<Process> processQueue)
	{
		foreach (Process process in processQueue)
		{
			Console.WriteLine(process.processID + " is beginning execution.");
			process.entryTime = DateTime.Now;
            int totalTime = process.serviceTime * timeUnit;
			for (int i = 0; i < totalTime; i++)
			{
                // multiply service time by times unit
                
                Console.WriteLine(process.processID + " is executing.");
			}
			process.exitTime = DateTime.Now;
		}

	}

	public int ComputeThroughput(int time1, int time2)
	{

		return 0;
	}

    
}
