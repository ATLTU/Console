using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HangmanClient
{
    class Program
    {
        /// <summary>
        /// Address and port of the remote server.
        /// </summary>        
        private static string serverAddress = "127.0.0.1";
        private static int serverPort = 15555;

        /// <summary>
        /// The TcpClient that connects to the remote server.
        /// </summary>
        private static Client client = new Client();

        /// <summary>
        /// The hangman figure displayed on screen.
        /// </summary>
        private static HangmanFigure figure = new HangmanFigure();

        /// <summary>
        /// The player.
        /// </summary>
        private static Player player = new Player();

        static void Main(string[] args)
        {
            // Change console title.
            Console.Title = "Hangman Client";

            // Connect to the server.
            client.Connect(serverAddress,serverPort);

            PlayHangman();

            // Wait for key to exit.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void PlayHangman()
        {
            SetupSequence();

            if (player.IsHangmaster)
            {
                HangMasterGameLoop();
            }
            else
            {
                GuesserGameLoop();
            }
        }

        /// <summary>
        /// Sets initial starting conditions and establishes players.
        /// </summary>
        private static void SetupSequence()
        {
            // set IsHangMaster
            //player.IsHangmaster = byte.Parse(client.ReadString()) == 1 ? true : false;

            int data = int.Parse(client.ReadString());
            if (data == 1)
            {
                player.IsHangmaster = true;
                Console.WriteLine("Hangmaster = true received.");
            }
            else if (data == 0)
            {
                player.IsHangmaster = false;
                Console.WriteLine("Hangmaster = false received.");
            }
            else
            {
                Console.WriteLine($"Error: Message from server is invalid. Expected: 1 or 0, received: {data}");
            }
        }

        private static void HangMasterGameLoop()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                // Hangmaster listens to server and updates the game state.
            }
        }

        private static void GuesserGameLoop()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                // Guesser listens to server and updaes the game state, responds with guesses.
            }
        }
    }
}
