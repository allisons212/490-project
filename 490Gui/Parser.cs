using System;
using System.Collections.Generic;

public class Parser
{

    public Parser()
    {
        int arrivalTime;
        string processID;
        int serviceTime;
        int priority;
        Queue<Process> processList;
    }

    // Summary: Reads data from a csv file, sets it to a process object, and adds to a queue
    // of processes
    // Params: String
    // Return: Process Queue
    public static Queue<Process> readProcessFile(string path)
    {
        var processTempList = new Queue<Process>();
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines)
        {
            Process process = new Process();
            processTempList.Enqueue(process);
            int count = 0;
            string[] columns = line.Split(',');
            foreach (string column in columns)
            {
                if (count == 0)
                {
                    process.ArriveTime = int.Parse(column);
                }
                else if (count == 1)
                {
                    process.ProcessID = column;
                }
                else if (count == 2)
                {
                    process.ServiceTime = int.Parse(column);
                }
                else
                {
                    process.Priority = int.Parse(column);
                }

                count++;
            }
            count = 0;
        }
        return processTempList;
    }
}