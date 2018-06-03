using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsProtocol
{
    public class PsServerProcessor
    {
        public Task Process(IPsOutput output, byte data)
        {
            return output.Send((byte)char.ToUpper((char)data));
        }
    }
}
