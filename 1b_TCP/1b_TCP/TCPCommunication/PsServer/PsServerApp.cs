using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsServer
{
    public class PsServerApp
    {
        public PsServerApp(PsServerConfiguration configuration)
        {
            _configuration = configuration;
            _workers = new ConcurrentDictionary<Guid, PsServerWorker>();
            _cancellationSource = new CancellationTokenSource();
        }

        public async Task Start()
        {
            _server = CreateTcpListener();
            // Start listening for client requests.
            _server.Start();
            // Enter the listening loop. 
            await Listen();
        }

        public void Stop()
        {
            _server.Stop();
            _cancellationSource.Cancel();
        }

        private async Task Listen()
        {
            while (!_cancellationSource.IsCancellationRequested)
            {
                try
                {
                    var tcpClient = await _server.AcceptTcpClientAsync();
                    CreateWorker(tcpClient, _cancellationSource.Token);
                }
                catch(ObjectDisposedException ex)
                {
                    Console.WriteLine($"Server was closed. {ex.Message}");
                }
            }
        }

        private void CreateWorker(TcpClient client, CancellationToken cancellationToken)
        {
            var workerConfiguration = new PsServerWorkerConfiguration()
            {
                CancellationToken = cancellationToken,
                Client = client,
                Middlewares = _configuration.Middlewares,
                WrokerClosed = WorkerRemoved
            };

            var worker = new PsServerWorker(workerConfiguration);
            var task = worker.Run();
            _workers.AddOrUpdate(worker.Id, worker, (guid, w)=> worker);
        }

        private void WorkerRemoved(PsServerWorker worker)
        {
            PsServerWorker removedWorker;
            _workers.TryRemove(worker.Id, out removedWorker);
        }

        private TcpListener CreateTcpListener()
        {
            TcpListener server = null;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, _configuration.Port);
            return server;
        }

        private readonly PsServerConfiguration _configuration;
        private readonly ConcurrentDictionary<Guid, PsServerWorker> _workers; 
        private readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        private TcpListener _server;
    }
}
