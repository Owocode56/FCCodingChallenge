using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCodingChallenge.Tests
{
    public static class Logger
    {
        public static void Info(string message)
        {
            Console.WriteLine("INFO: {0}", message);
        }

        public static void Debug(string message)
        {
            Console.WriteLine("DEBUG: {0}", message);
        }

        public static void Warning(string message)
        {
            Console.WriteLine("WARNING: {0}", message);
        }

        public static void Error(string message)
        {
            Console.WriteLine("ERROR: {0}", message);
        }
    }
}
