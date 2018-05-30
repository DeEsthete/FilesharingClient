using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FilesharingClient
{
    class Program
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 3535;

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            //client.Connect(SERVER_IP, SERVER_PORT);
            ServerConnect serverConnect = new ServerConnect();
            serverConnect.StartWork(client);
        }
    }
}
