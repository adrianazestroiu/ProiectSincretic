using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class MyEventArgs : EventArgs
    {
        private byte[] data = new byte[16];
        public MyEventArgs(byte[] x)
        {
            data = x;
        }

        public byte[] GetData()
        {
			return data;
        }
    }
}
