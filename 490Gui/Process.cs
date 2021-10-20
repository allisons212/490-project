using System;

public class Process
{
    // ADD MEMBER VARIABLES FOR PHASE II 
    public int arrivalTime;
    public int serviceTime;
    public string processID;
    public int priority;
    public DateTime entryTime;
    public DateTime finishTime;
    public Process()
    {
        arrivalTime = 0;
        serviceTime = 0;
        processID = "";
        priority = 0;
    }
}