using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HangmanServer
{
    public class Server
    {
        #region Fields
        /// <summary>
        /// A listener that accepts conections from remote clients.
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// Server specs.
        /// </summary>
        private int port = 15555;
        private int minPlayers;

        /// <summary>
        /// Game specific variables.
        /// </summary>
        private List<Player> players;
        private List<Player> guessers;
        private Player hangmaster;
        private string secretWord;
        private char[] usedLetters;
        private string stringData;

        /// <summary>
        /// Singleton pattern instance.
        /// </summary>
        private static Server instance = null;
        #endregion

        #region Properties
        /// <summary>
        /// The number of current active connections to the server.
        /// </summary>
        public int Connections { get; set; }

        /// <summary>
        /// Object used for locking critical areas of code when client threads wish to access them.
        /// </summary>
        public object ThreadLock { get; set; } = new object();

        /// <summary>
        /// Singleton pattern property.
        /// </summary>
        public static Server Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Server();
                }

                return instance;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Private construcor.
        /// </summary>
        private Server()
        {
            minPlayers = 2;
            Connections = 0;

            players = new List<Player>();
            guessers = new List<Player>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the server and waits for it to be populate with enough players to play a game.
        /// </summary>
        public void InitializeServer()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            ConnectToPlayers();
        }

        /// <summary>
        /// Starts a new game of Hangman.
        /// </summary>
        public void PlayHangman()
        {
            // Set initial conditions for the game.
            AssignPlayerRoles();
            GetSecretWord();

            // Enter main loop of the game.
            MainGameLoop();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Listens for connections and accepts clients until enough players are found.
        /// </summary>
        private void ConnectToPlayers()
        {
            while (Connections < minPlayers)
            {
                Console.WriteLine($"Waiting for connections... (current connections: {Connections}/{minPlayers})");

                Player newPlayer = new Player(listener.AcceptTcpClient());
                players.Add(newPlayer);
                Connections++;
            }
        }

        /// <summary>
        /// Assigns player roles (guesser or hangmaster) randomly.
        /// </summary>
        private void AssignPlayerRoles()
        {
            Random rnd = new Random();

            hangmaster = players[rnd.Next(0, players.Count)];
            hangmaster.IsHangmaster = true;
            guessers.AddRange(players.FindAll(p => p != hangmaster));

            // Relay to hangmaster client that they are hangmaster.
            hangmaster.WriteString("1");

            // Relay to player client(s) that they are guesser(s).
            foreach (Player player in guessers)
            {
                player.WriteString("0");
            }
        }

        /// <summary>
        /// Requests the secret word from the hanglaster player and assigns it to the secretWord variable.
        /// </summary>
        private void GetSecretWord()
        {
            try
            {
                hangmaster.WriteString("You are the Hangmaster. Enter the secret word that the other player will try to guess: ");
                secretWord = hangmaster.ReadString();
                Console.WriteLine($"Received secret word from hangmaster: {secretWord}.");

                if (!secretWord.All(char.IsLetter))
                {
                    secretWord = "APPLE";
                    Console.WriteLine($"Invalid secret word, using default instead: {secretWord}.");
                    hangmaster.WriteString($"Your secret word contained invalid characters. Using default secret word instead: {secretWord}");
                }
                else
                {
                    Console.WriteLine($"Secret word registered.");
                    hangmaster.WriteString($"Your secret word has been registered. The secret word is: {secretWord}");
                }

                secretWord = secretWord.ToUpper();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when getting secret word from hangmaster client.");
                Console.WriteLine("Error message received:\n\n");
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// This is the main game loop that repeats until a player has won.
        /// </summary>
        private void MainGameLoop()
        {
            while (Connections >= minPlayers)
            {
                // Server gets guesses and sends results after each round to all players.
            }

            // Report  differently depending on why the game ended.
            if (Connections >= minPlayers)
            {
                Console.WriteLine("Game ended normally.");
            }
            else
            {
                Console.WriteLine("One or more players disconnected. Ending the game.");
            }
        }
        #endregion
    }
}
