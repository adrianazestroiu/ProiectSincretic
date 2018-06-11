using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
	public class MyEventArgs : EventArgs
	{
        public byte[] Data { get; set; }
        public DateTime Time { get; set; }
    }
}
