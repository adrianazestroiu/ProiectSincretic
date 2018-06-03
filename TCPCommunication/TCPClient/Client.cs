using PsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER to connect");
            Console.ReadLine();
            //Connect("127.0.0.1", "Hi");
            RunClientApp();
        }

        public static void RunClientApp()
        {
            var configuration = new PsClientConfiguration("127.0.0.1", 13000, (output, data) => {
                Console.WriteLine($"Received {data.ToString()}");
                return Task.CompletedTask;
            });

            var clientApp = new PsClientApp(configuration);
            var clientTask = clientApp.Start();

            clientApp.PsOutput.Send((byte)'H').Wait();
            clientApp.PsOutput.Send((byte)'e').Wait();
            clientApp.PsOutput.Send((byte)'l').Wait();
            clientApp.PsOutput.Send((byte)'l').Wait();
            clientApp.PsOutput.Send((byte)'o').Wait();

            Console.WriteLine("Press ENTER to stop client.");
            Console.ReadLine();
            clientApp.Stop();
            Task.WaitAll(clientTask);
        }
    }
}
