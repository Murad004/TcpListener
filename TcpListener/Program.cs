using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{

    //var buffer = new byte[1024];
    //int length = stream.Read(buffer, 0, 1024);
    //var msg = Encoding.UTF8.GetString(buffer, 0, length);
    class Program
    {
        static TcpListener listener = null;
        static BinaryWriter bw = null;
        static BinaryReader br = null;
        static void Main(string[] args)
        {
            var ip = IPAddress.Parse("10.1.18.38");
            var ep = new IPEndPoint(ip, 27001);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine($"Listening on {listener.LocalEndpoint}");


            while (true)
            {
                var client = listener.AcceptTcpClient();

                Task.Run(() =>
                {
                    Console.WriteLine($"{client.Client.RemoteEndPoint} connected");

                    var stream = client.GetStream();
                    br = new BinaryReader(stream);
                    bw = new BinaryWriter(stream);

                    while (true)
                    {
                        var msg = br.ReadString();  
                        Console.WriteLine($"{client.Client.RemoteEndPoint} : {msg}");
                    }
                });

            }

        }
    }
}
