using PsProtocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsServer
{
    public class PsServerWorker
    {
        public Guid Id { get; private set; }
        public IPsOutput Client { get => _client; }
        public PsServerWorker(PsServerWorkerConfiguration configuration)
        {
            _configuration = configuration;
            _stream = _configuration.Client.GetStream();
            _client = new PsOutput(_stream);
            Id = Guid.NewGuid();
        }
        
        public async Task Run()
        {
            try
            {
                while (!_configuration.CancellationToken.IsCancellationRequested)
                {
                    await ReadAndProcessByte();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wroker was closed. Reason: {ex.Message}");
            }
            _stream.Close();
            _configuration.Client.Close();
            _configuration.WrokerClosed(this);
        }

        private async Task ReadAndProcessByte()
        {
            var data = new byte[1];
            var readCount = await _stream.ReadAsync(data, 0, 1, _configuration.CancellationToken);
            if (readCount == 1)
            {
                await InvokeMiddleware(data[0]);
            }
        }

        private async Task InvokeMiddleware(byte data)
        {
            foreach (var middleware in _configuration.Middlewares)
            {
                await middleware(_client, data);
            }
        }

        private PsServerWorkerConfiguration _configuration;
        private IPsOutput _client;
        private Stream _stream;
    }
}
