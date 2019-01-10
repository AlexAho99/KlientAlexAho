using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace KlientAlexAho
{
    class Program
    {
        public static void Main()
        {
            try
            {
                string address = "127.0.0.1";
                int port = 8001;

                //Anslut till server
                Console.WriteLine("Ansluter...");
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(address, port);
                Console.WriteLine("Ansluten!");

                //Skriv meddelande
                Console.Write("Skriv meddelande: ");
                string message = Console.ReadLine();

                //Någon försöker ansluta. Acceptera anslutningen
                Byte[] bMessage = System.Text.Encoding.ASCII.GetBytes(message);

                //Skicka iväg meddelandet
                Console.WriteLine("Skickar...");
                NetworkStream tcpStream = tcpClient.GetStream();
                tcpStream.Write(bMessage, 0, bMessage.Length);

                //Tag emot meddelande frånservern:
                byte[] bRead = new byte[256];
                int bReadSize = tcpStream.Read(bRead, 0, bRead.Length);

                //Konvertera meddelandet till ett string-objekt och skriv ut
                string read = "";
                for (int i = 0; i < bReadSize; i++)
                    read += Convert.ToChar(bRead[i]);
                Console.WriteLine("Servern säger: " + read);

                //sträng anslutningen:
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
