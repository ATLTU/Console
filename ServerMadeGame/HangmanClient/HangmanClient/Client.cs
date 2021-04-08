using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace HangmanClient
{
    class Client
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private bool isConnected;

        public IPEndPoint RemoteEndPoint { get; private set; }

        public IPEndPoint LocalEndPoint { get; private set; }

        public Client()
        {
            client = new TcpClient();
        }

        /// <summary>
        /// Connects to the specified remote socket.
        /// </summary>
        /// <param name="hostAddress">The remote address to connect to.</param>
        /// <param name="hostPort">The remote port to connect to.</param>
        public void Connect(string hostAddress, int hostPort)
        {
            while (!client.Connected)
            {
                try
                {
                    client.Connect(hostAddress, hostPort);

                    RemoteEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    LocalEndPoint = (IPEndPoint)client.Client.LocalEndPoint;

                    Console.WriteLine($"Connected to server with address {RemoteEndPoint.Address.ToString()}, port {RemoteEndPoint.Port.ToString()}.");

                    stream = client.GetStream();
                    streamReader = new StreamReader(stream, Encoding.UTF8);
                    streamWriter = new StreamWriter(stream, Encoding.UTF8);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to connect to server. Got the following error message:");
                    Console.WriteLine("\n" + e);
                    Console.WriteLine("Trying again in 10 seconds.\n");

                    Thread.Sleep(10000);
                }
            }
        }

        #region Public Methods
        /// <summary>
        /// Gets a string from the remote host via the network stream.
        /// </summary>
        /// <returns>A string from the remote host.</returns>
        public string ReadString()
        {
            try
            {
                return streamReader.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when reading from client.");
                Console.WriteLine("Error message received:\n\n");
                Console.WriteLine(e);

                return null;
            }
        }

        /// <summary>
        /// Gets a byte from the remote host via the network stream.
        /// </summary>
        /// <returns>A byte from the remote host.</returns>
        public byte ReadByte()
        {
            try
            {
                return (byte)stream.ReadByte();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when reading from client.");
                Console.WriteLine("Error message received:\n\n");
                Console.WriteLine(e);

                return 0;
            }
        }

        /// <summary>
        /// Sends a string to the remote host via the network stream.
        /// </summary>
        /// <param name="message">A string to send to the remote host.</param>
        public void WriteString(string message)
        {
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when writing to server.");
                Console.WriteLine("Error message received:\n\n");
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Sends a byte to the remote host via the network stream.
        /// </summary>
        /// <param name="data">A byte to send to the remote host.</param>
        public void WriteByte(byte data)
        {
            try
            {
                stream.WriteByte(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when writing a byte to client.");
                Console.WriteLine("Error message received:\n\n");
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}
