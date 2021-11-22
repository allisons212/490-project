using System;
using System.Collections.Generic;


/**
 Summary: Parser class reading from an input file and sets the data from the file to a queue.
 */
public class Parser
{
    public Parser()
    {
        int arrivalTime; // integer that contains process arrival time
        string processID; // string that holds process id 
        int serviceTime; // integer that holds the process service time
        int priority; // integer that holds the priority of each process
        Queue<Process> processList; // the queue that holds all process data 
    }

    // Summary: Reads data from a csv file, sets it to a process object, and adds to a queue
    // of processes
    // Params: String
    // Return: Process Queue
    public static Queue<Process> readProcessFile(string path)
    {
        var processTempList = new Queue<Process>(); // queue of processes to be returned by function
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