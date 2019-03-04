using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string ip = "127.0.0.1"; 
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Connect(tcpEndPoint);

                Console.Write("message to send : ");

                var message = Console.ReadLine();

                if (message.ToUpper() == "EXIT")
                {
                    Console.WriteLine("client stopped..");
                    return;
                }

                var data = Encoding.UTF8.GetBytes(message);

                tcpSocket.Send(data);

                var buffer = new byte[256];
                var size = 0;
                var answer = new StringBuilder();

                do
                {
                    size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (tcpSocket.Available > 0);

                Console.WriteLine(answer.ToString());

                if (!tcpSocket.Connected)
                {
                    tcpSocket.Shutdown(SocketShutdown.Both);
                    tcpSocket.Close();
                    tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
           
        }
    }
}