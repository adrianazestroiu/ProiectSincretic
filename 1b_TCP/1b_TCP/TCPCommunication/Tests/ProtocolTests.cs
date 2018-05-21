using Moq;
using PsProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProtocolTests
    {
        private Mock<IPsOutput> _mockOutput;
        private PsServerProcessor _server;

        public ProtocolTests()
        {
            _mockOutput = new Mock<IPsOutput>();
            _server = new PsServerProcessor();
        }

        [Fact]
        public void TestServerProtocol()
        {
            _server.Process(_mockOutput.Object, (byte)'h');
            _mockOutput.Verify(m => m.Send(It.Is<byte>(b => b == (byte)'H')), Times.Once);
        }
    }
}
