using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangmanServer
{
    class Program
    {
        /// <summary>
        /// Boolean for tracking whether the server is running or not.
        /// </summary>
        private static bool isRunning;

        static void Main(string[] args)
        {
            // Set console title.
            Console.Title = "Hangman Server";
            Server.Instance.InitializeServer();
            Server.Instance.PlayHangman();

            // Wait for key to exit.
            Console.WriteLine("Reached the end of Main. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
