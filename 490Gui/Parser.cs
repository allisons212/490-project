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

    public static Queue<Process> ReadProcessFile(string path)
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
                    process.arrivalTime = int.Parse(column);
                }
                else if (count == 1)
                {
                    process.processID = column;
                }
                else if (count == 2)
                {
                    process.serviceTime = int.Parse(column);
                }
                else
                {
                    process.priority = int.Parse(column);
                }

                count++;
            }
            count = 0;
        }
        return processTempList;
    }
}
