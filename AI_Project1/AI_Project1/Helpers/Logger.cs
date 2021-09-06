using System;

namespace AI_Project1.Helpers
{
    static class Logger
    {
        public static void Info (string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}.");
        }
    }
}
