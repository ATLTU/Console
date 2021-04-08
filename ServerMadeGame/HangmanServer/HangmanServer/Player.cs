using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangmanServer
{
    public class Player
    {
        #region Fields
        private TcpClient client;
        private IPEndPoint remoteEndPoint;
        private NetworkStream stream;
        private StreamWriter streamWriter;
        private StreamReader streamReader;
        #endregion

        #region Properties
        public bool IsHangmaster { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">A TcpClient that provides a network stream between the player and the server.</param>
        public Player(TcpClient client)
        {
            this.client = client;
            remoteEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
            stream = this.client.GetStream();
            streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamReader = new StreamReader(stream, Encoding.UTF8);
        }
        #endregion

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

                Server.Instance.Connections--;

                return null;
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
                Console.WriteLine("Error when writing to client.");
                Console.WriteLine("Error message received:\n\n");
                Console.WriteLine(e);

                Server.Instance.Connections--;
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

                Server.Instance.Connections--;
            }
        }
        #endregion
    }
}
