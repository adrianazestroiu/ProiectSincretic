using PsProtocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsServer
{
    public class PsServerConfiguration
    {
        public int Port { get; private set; }

       public IEnumerable<Func<IPsOutput, byte, Task>> Middlewares
        {
            get { return _middlewares.AsEnumerable(); }
        }

        public PsServerConfiguration(int port)
        {
            Port = port;
            _middlewares = new List<Func<IPsOutput, byte, Task>>();
        }

        public PsServerConfiguration AddMiddleware(Func<IPsOutput, byte, Task> middleware)
        {
            _middlewares.Add(middleware);
            byte[] a = new byte[1];
            
            ASCIIEncoding.ASCII.GetString(a);
            return this;
        }

        private readonly List<Func<IPsOutput, byte, Task>> _middlewares;
    }
}
