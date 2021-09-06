using System;
using System.Diagnostics;
using AI_Project1.GoalFinders;
using AI_Project1.Helpers;

namespace AI_Project1
{
    class Program
    {
        private static readonly GoalFinder goalFinder = new RecursiveGoalFinder();

        static void Main(string[] args)
        {
            RunFirstPage();

            var initialNode = Helper.LoadInitialState();

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            (Node goal, int expandedCount) = goalFinder.Find(initialNode);
            stopwatch.Stop();

            var timeElapsed = stopwatch.Elapsed;

            if (goal != null)
            {
                Helper.SaveSolution(goal, expandedCount, timeElapsed);
                Console.WriteLine("Search Done!");
            }
            else
                Console.WriteLine("Unable to find a path! :(");
        }

        static void RunFirstPage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\tAli Abbasi\t9531503\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\tArtificial Intelligence\n");
            Console.WriteLine("\tProject1:");
            Console.WriteLine("\tA*.\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tDr. S Noferesti");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\tIran, University of Sistan and Baluchestan");
            Console.WriteLine("\tDecember 2020");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\n\n\tPress any key to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
