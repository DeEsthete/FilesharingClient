using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FilesharingClient
{
    public class ServerConnect
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 3535;

        private TcpClient client = new TcpClient();
        private NetworkStream stream;

        public async void Connect()
        {
            await client.ConnectAsync(IPAddress.Parse(SERVER_IP), SERVER_PORT);
        }

        public void UserMenu()
        {
            try
            {
                Console.WriteLine("Для выхода из приложения введите 'exit'");

                while (true)
                {
                    Console.WriteLine("-----------------Укажите полный путь файла-----------------");

                    string path = Console.ReadLine();

                    if (path == "exit")
                    {
                        break;
                    }
                    else
                    {
                        if (File.Exists(path))
                        {
                            Task.Run(() => Work(path));
                            Console.WriteLine("Файл отправлен!");
                        }
                        else
                        {
                            Console.WriteLine("Указан не правильный путь!");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                client.Close();
            }
        }

        private void Work(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            FileTransport file = new FileTransport();
            file.Data = File.ReadAllBytes(path);
            file.FileName = fileInfo.Name;

            var data = JsonConvert.SerializeObject(file);
            var bytesData = Encoding.Default.GetBytes(data);

            if (stream == null)
            {
                stream = client.GetStream();
            }

            stream.Write(bytesData, 0, bytesData.Length);
        }
    }
}
