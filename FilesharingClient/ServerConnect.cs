using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FilesharingClient
{
    public class ServerConnect
    {
        public void StartWork(TcpClient client)
        {
            while (true)
            {
                Work(client);
            }
        }
        public async void Work(TcpClient client)
        {
            using (var networkStream = client.GetStream())
            {
                while (true)
                {
                    string path = Console.ReadLine();
                    FileInfo info = new FileInfo(path);

                    FileTransport file = new FileTransport();
                    file.FileName = info.Name;
                    file.Expansion = info.Extension;
                    file.Data = File.ReadAllBytes(path);

                    string serialized = JsonConvert.SerializeObject(file);
                    Console.WriteLine(serialized.Length);
                    var buffer = Encoding.Default.GetBytes(serialized);
                    await networkStream.WriteAsync(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
