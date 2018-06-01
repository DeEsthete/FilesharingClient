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
        static void Main(string[] args)
        {
            ServerConnect serverConnect = new ServerConnect();
            serverConnect.Connect();
            serverConnect.UserMenu();
        }
    }
}
