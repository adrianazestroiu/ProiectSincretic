using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPCommunication
{
    using PsProtocol;
    using PsServer;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    class MyTcpListener
    {
        public static void Main()
        {
            RunTcpServer();
        }
        
        private static void RunTcpServer()
        {
            var configuration = new PsServerConfiguration(13000);
            var server = new PsServerProcessor();
            //NetworkStream stream = client.GetStream();
            int j = 0;
            configuration.AddMiddleware((client, data) => {
                
                if(data.CompareTo("ON")==0)
                    Console.WriteLine("Pompa a fost pornita");
                else
                    if (data.CompareTo("OFF") == 0)
                        Console.WriteLine("Pompa a fost oprita");
                return server.Process(client,data);
            });
            var app = new PsServerApp(configuration);
            var serverTask = app.Start();
            Console.WriteLine("\nHit enter to stop server...");
            Console.Read();
            app.Stop();
            Task.WaitAll(serverTask);
        }
    }
}
