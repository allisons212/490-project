using System;

public class Process : ICloneable, IComparable
{
	// time process arrives in queue
	private int arrivalTime;
	public int ArriveTime
	{
		get { return arrivalTime; }
		set { arrivalTime = value; }
	}
	// time process takes
	private int serviceTime;
	public int ServiceTime
	{
		get { return serviceTime; }
		set { serviceTime = value; }
	}
	// ID of process
	private string processID;
	public string ProcessID
	{
		get { return processID; }
		set { processID = value; }
	}
	// priority of process in list of processes
	private int priority;
	public int Priority
	{
		get { return priority; }
		set { priority = value; }
	}
	// time process entered queue
	private DateTime entryTime;
	public DateTime EntryTime
	{
		get { return entryTime; }
		set { entryTime = value; }
	}
	// finish time of process
	private DateTime finishTime;
	public DateTime FinishTime
	{
		get { return finishTime; }
		set { finishTime = value; }
	}

	// time process takes
	private int initialServiceTime;
	public int InitialServiceTime
	{
		get { return initialServiceTime; }
		set { initialServiceTime = value; }
	}

	// finish time in int
	private int intFinishTime;
	public int IntFinishTime
	{
		get { return intFinishTime; }
		set { intFinishTime = value; }
	}

	// how long a process waits relative to the amount of time it takes to execute
	private TimeSpan ntat;
	public TimeSpan NTAT
	{
		get { return ntat; }
		set { ntat = computeNTAT(tat, serviceTime); }
	}
	// the elapsed time from when a process arrived to when the process finishes
	private TimeSpan tat;
	public TimeSpan TAT
	{
		get { return tat; }
		set { tat = computeTAT(entryTime, FinishTime); }
	}
	// thread of process
	private int processThread;
	public int ProcessThread
	{
		get { return processThread; }
		set { processThread = value; }
	}

	// waiting time 
	private DateTime availableProcessesTime;
	public DateTime AvailableProcessesTime
	{
		get { return availableProcessesTime; }
		set { availableProcessesTime = value; }
	}

	// waiting time 
	private double responseRatio;
	public double ResponseRatio
	{
		get { return responseRatio; }
		set { responseRatio = value; }
	}


	// process default constructor
	public Process()
	{
		arrivalTime = 0;
		serviceTime = 0;
		processID = "";
		priority = 0;
		availableProcessesTime = default;
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

	public int CompareTo(object process)
	{
		if (process == null)
			return 1;
		Process temp = process as Process;
		return this.ResponseRatio.CompareTo(temp.ResponseRatio);
	}

	public object Clone()
	{
		return new Process
		{
			ArriveTime = this.ArriveTime,
			ServiceTime = this.ServiceTime,
			ProcessID = this.ProcessID,
			Priority = this.Priority

		};
	}
}