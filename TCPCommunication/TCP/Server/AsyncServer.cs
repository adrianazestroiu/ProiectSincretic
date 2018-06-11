using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class AsyncServer
    {
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static event EventHandler<MyEventArgs> DataReceived;
        static object state = 0;
        private static TcpClient client;
        private static byte[] oldData;

        private static NetworkStream stream;

        public static bool buttonStartPressed;
        public static bool buttonStopPressed;
        public static byte startByte;
        public static byte stopByte;

        public static byte[] myData;
        public AsyncServer()
        {
            //this.StateChanged += AsyncServer_StateChanged;
        }
        protected static void OnDataReceived(MyEventArgs e)
        {
            DataReceived?.Invoke(null, e);
        }

        public static void StopClient(object sender, EventArgs e)
        {
            client.Close();
        }

        public static void StopClient()
        {
            if (client != null)
            {
                client.Close();
            }

        }

        private static bool ByteArraysAreTheSame(byte[] firstArray, byte[] secondArray)
        {
            if (secondArray == null && firstArray != null)
                return false;

            for (int i = 0; i < 16; i++)
            {
                if (firstArray[i] != secondArray[i])
                    return false;
            }

            return true;
        }

        private static byte[] CopyFromByteArray(byte[] firstArray, byte[] secondArray)
        {
            if (secondArray == null)
            {
                secondArray = new byte[firstArray.Length];
            }

            for (int i = 0; i < 16; i++)
            {
                secondArray[i] = firstArray[i];
            }

            return secondArray;
        }

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
           

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(GetLocalIP());
            //ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 2000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    //Console.Clear();
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            ServerState state = new ServerState();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, ServerState.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            ServerState state = (ServerState)ar.AsyncState;
            Socket handler = state.workSocket;
            byte[] dataRead = new byte[16];
            handler.Receive(dataRead);
           
            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            //MyEventArgs myEventArgs = new MyEventArgs(dataRead);
            

            if (bytesRead > 0)
            {
                Console.WriteLine(dataRead[0].ToString());
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));
                //OnStateChanged(myEventArgs);

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, ServerState.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
        static public int Main(String[] args)
        {
            StartListening();
            return 0;
        }

    }
}
