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
    public class PsServerWorkerConfiguration
    {
        public TcpClient Client { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public Action<PsServerWorker> WrokerClosed { get; set; }
        public IEnumerable<Func<IPsOutput, byte[], Task>> Middlewares { get;  set; }
    }
}
