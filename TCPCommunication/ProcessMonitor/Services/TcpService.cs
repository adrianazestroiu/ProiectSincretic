using PsServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ProcessMonitor.Services
{
    public class TcpService : ITcpService
    {
        private readonly PsServerApp _psServerApp;
        private ConcurrentBag<byte> _receivedData;
        private ConcurrentBag<byte> _receivedDataByte1;
        private ConcurrentBag<byte> _receivedDataByte2;

        public TcpService(PsServerApp psServerApp)
        {
            _psServerApp = psServerApp;
            _receivedData = new ConcurrentBag<byte>();
            _receivedDataByte1 = new ConcurrentBag<byte>();
            _receivedDataByte2 = new ConcurrentBag<byte>();
        }

        public Task SendMessage(byte message)
        {
            return _psServerApp.BroadCastMessage(message);
        }

        public byte[] GetReceivedMessages()
        {
            return _receivedData.ToArray();
        }

        public byte[] GetReceivedMessagesByte1()
        {
            return _receivedDataByte1.ToArray();
        }

        public byte[] GetReceivedMessagesByte2()
        {
            return _receivedDataByte2.ToArray();
        }
        public void ReceiveNewMessage(byte data)
        {
            //_receivedData.Add(data);

            Console.WriteLine(data);
            if (data >= 128)
            {
                if (_receivedDataByte2.Count > 0)
                {
                    if (_receivedDataByte2.First() != data)
                    {
                        _receivedDataByte2.Add(data);
                    }
                }
                else
                {
                    _receivedDataByte2.Add(data);
                }
            }
            else
            {
                if (_receivedDataByte1.Count > 0)
                {
                    if (_receivedDataByte1.First() != data)
                    {
                        _receivedDataByte1.Add(data);
                    }
                }
                else
                {
                    _receivedDataByte1.Add(data);
                }
            }
            _receivedData.Add(data);

        }
    }
}
