using PsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Program
    {
        //NetworkStream stream = client.GetStream();
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER to connect");
            Console.ReadLine();
            RunClientApp();
        }

        public static void RunClientApp()
        {
            Console.WriteLine("Received: ");
            var configuration = new PsClientConfiguration("127.0.0.1", 13000, (output,data) => {
                
                byte[] str = new byte[1];
                str[0] = data;
                string stringToDisplay = ASCIIEncoding.ASCII.GetString(str);

                //Console.WriteLine($"Received:aoleu {data}");
                
                Console.WriteLine(stringToDisplay);
                return Task.CompletedTask;
            });

            var clientApp = new PsClientApp(configuration);
            var clientTask = clientApp.Start();
            int i;
            Console.WriteLine("Menu:\n 1) Porneste pompa \n 0) Opreste pompa \n");
            Console.WriteLine("Introdu optiunea:\n");
            i=Convert.ToInt16(Console.ReadLine());
            if(i==1)
            {
                clientApp.PsOutput.Send((byte[])Encoding.ASCII.GetBytes("ON")).Wait();
                //clientApp.PsOutput.Send((byte)'N').Wait();
            }
           else
            {
                clientApp.PsOutput.Send((byte[])Encoding.ASCII.GetBytes("Off")).Wait();
                /*clientApp.PsOutput.Send((byte)'O').Wait();
                clientApp.PsOutput.Send((byte)'F').Wait();
                clientApp.PsOutput.Send((byte)'F').Wait();
                */
            }

            Console.WriteLine("Press ENTER to stop client.");
            Console.ReadLine();
            clientApp.Stop();
            Task.WaitAll(clientTask);
        }
    }
}
