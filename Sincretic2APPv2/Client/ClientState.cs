﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using AutomationControl;

namespace Client
{
    class ClientState
    {// State object for receiving data from remote device.
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
}
