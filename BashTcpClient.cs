using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace WSBashBot
{
    // A helper class that wraps the TcpClient with some
    // helpful functions, such as inbuild StreamReader and Writer
    public class BashTcpClient : TcpClient
    {
        public StreamReader Reader;
        public StreamWriter Writer;
        
        // Set up the streams when we connect!
        new public void Connect(string ip, int port)
        {
            base.Connect(ip, port);
            Reader = new StreamReader(GetStream());
            Writer = new StreamWriter(GetStream())
                {
                    // Important so that everytime we write we flush
                    //  without having to explicitly call Flush()
                    AutoFlush = true
                };
        }

        // Send a ping!
        public void Ping()
        {
            WriteLine(BashCommand.Ping);
        }

        // Testing this close function
        // TODO: Make this work more betterer!
        new public void Close()
        {
            if (Client.Connected)
            {
                Client.Disconnect(true);
            }
        }

        // Simple 1:1 wrapper around the Write func
        public void Write(string format, params object[] values)
        {
            Writer.Write(format, values);
        }

        // Simple 1:1 wrapper around the WriteLine func
        public void WriteLine(string format, params object[] values)
        {
            Writer.WriteLine(format, values);
        }

        // Hashes the password
        public static string Hash(string password)
        {
            var md5 = MD5.Create();
            // Turn our string into a byte array so we can hash it
            var inputBytes = Encoding.ASCII.GetBytes(password);
            // Compute the hashed bytes
            var hash = md5.ComputeHash(inputBytes);
            // Create a string builder to turn the hashed bytes into a hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                // 'X2' is a two length hexadecimal representation of the number.
                sb.Append(hash[i].ToString("X2"));
            }
            // Return our fully built hashed string!
            // For some reason Toribash seems to send these in lowercase
            //   so convert here just for consistency.
            return sb.ToString().ToLower();
        }
    }
}
