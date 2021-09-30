using System;

public class Process
{
	public int arrivalTime;
	public int serviceTime;
	public string processID;
	public int priority;
	public DateTime entryTime;
	public DateTime exitTime;
	public Process()
	{
		arrivalTime = 0;
		serviceTime = 0;
		processID = "";
		priority = 0;
	}
}
