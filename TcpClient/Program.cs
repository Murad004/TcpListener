using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TcpClient();
            var ip = IPAddress.Parse("10.1.18.4");
            var port = 27001;
            var ep = new IPEndPoint(ip, port);

            try
            {
                client.Connect(ep);
                if (client.Connected)
                {
                    var writer = Task.Run(() =>
                    {

                        while (true)
                        {
                            var text = Console.ReadLine();
                            var stream = client.GetStream();
                            var bw = new BinaryWriter(stream);
                            bw.Write(text);
                        }
                    });
                    var reader = Task.Run(() =>
                    {

                        var stream = client.GetStream();
                        var br = new BinaryReader(stream);
                        Console.WriteLine($"From : {br.ReadString()}");

                    });

                    Task.WaitAll(writer, reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
