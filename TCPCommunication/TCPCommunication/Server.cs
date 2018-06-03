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
            configuration.AddMiddleware((client, data) => {
                Console.WriteLine($"Received {data}");
                var server = new PsServerProcessor();
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
