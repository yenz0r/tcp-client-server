using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTcp
{
    class MainClass
    {
        public static void Listening(Socket listener)
        {
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();

            do
            {
                Console.WriteLine("server listen..");
                size = listener.Receive(buffer);
                data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                Console.WriteLine(data);
                var feedBack = "DONE!";
                listener.Send(Encoding.UTF8.GetBytes(feedBack));
                buffer = null;

            } while (size != 0);

            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }

        public static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);

            while (true)
            {
                var listener = tcpSocket.Accept();
                Task.Run(() => Listening(listener));
            }
        }
    }
}
