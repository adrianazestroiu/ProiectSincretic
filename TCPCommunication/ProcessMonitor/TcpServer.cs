using ProcessMonitor.Services;
using PsProtocol;
using PsServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace ProcessMonitor
{
    public static class TcpServer
    {
        private static PsServerApp _app;
        private static Task _serverTask;
        private const int _port = 13000;
        public static TcpService TcpService { get; private set; }
        public static void StartTcpServer()
        {
            var configuration = new PsServerConfiguration(_port);
            configuration.AddMiddleware((client, data) =>
            {
                Console.WriteLine($"Received {data}");
                TcpService.ReceiveNewMessage(data);
                return Task.CompletedTask;
            });
            _app = new PsServerApp(configuration);
            TcpService = new TcpService(_app);
            _serverTask = _app.Start();
            WriteLine($"TCP server is listenning on port {_port}.");
        }

        public static void StopTcpServer()
        {
            _app.Stop();
            Task.WaitAll(_serverTask);
            WriteLine("TCP server stopped.");
        }
    }
}
