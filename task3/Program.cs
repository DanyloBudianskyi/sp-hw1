using System;
using System.Diagnostics;
using System.Threading;

namespace task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Process process = new Process())
            {
                Console.Write("Input process name: ");
                process.StartInfo.FileName = Console.ReadLine();
                Console.Write("How many milliseconds application will work?\nInput your time: ");
                int time = Int32.Parse(Console.ReadLine());
                process.StartInfo.Arguments = "";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                Console.WriteLine($"Process {process.ProcessName} started");
                Thread.Sleep(time);
                if (!process.HasExited)
                {
                    process.Kill();
                    Console.WriteLine($"Process {process.ProcessName} killed");
                }

            }
            Console.ReadKey();
        }
    }
}
