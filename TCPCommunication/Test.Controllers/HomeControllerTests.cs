using Microsoft.AspNetCore.Mvc;
using Moq;
using Monitorizare.Controllers;
using Monitorizare.Models;
using Monitorizare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class HomeControllerTests
    {
        private Mock<ITcpService> tcpServiceMock;
        private HomeController homeController;

        public HomeControllerTests()
        {
            tcpServiceMock = new Mock<ITcpService>();
            homeController = new HomeController(tcpServiceMock.Object);
        }

        [Theory]
        [InlineData(new byte[0], "")]
        [InlineData(new byte[] { 10 }, "10")]
        [InlineData(new byte[] { 10, 120, 133, 45 }, "10 120 133 45")]
        public void GetIndexShouldDisplayReceivedMessage(byte[] messages, string displayMessage)
        {
            tcpServiceMock.Setup(m => m.GetReceivedMessages()).Returns(messages);
            var result = homeController.Index() as ViewResult; //returns null if result is not of type ViewResult

            //check that we have a valid non null result
            Assert.NotNull(result);
            var model = result.Model as ExchangedData;
            Assert.NotNull(model);
            Assert.Equal(displayMessage, model.MesssageBack);
            Assert.Equal(0, model.Messsage);
        }

        [Theory]
        [InlineData(new byte[0], "")]
        [InlineData(new byte[] { 10 }, "10")]
        [InlineData(new byte[] { 10, 120, 133, 45 }, "10 120 133 45")]
        public async Task PostIndexShouldDisplayReceivedMessage(byte[] messages, string displayMessage)
        {
            tcpServiceMock.Setup(m => m.GetReceivedMessages()).Returns(messages);
            tcpServiceMock.Setup(m => m.SendMessage(It.IsAny<byte>())).Returns(Task.CompletedTask);
            var result = await homeController.Index(new ExchangedData() { Messsage=1}) as ViewResult; //returns null if result is not of type ViewResult

            //check that we have a valid non null result
            Assert.NotNull(result);
            var model = result.Model as ExchangedData;
            Assert.NotNull(model);
            Assert.Equal(displayMessage, model.MesssageBack);
            Assert.Equal(0, model.Messsage);
        }

        [Fact]
        public async Task PostIndexShouldSendTheMessage()
        {
            tcpServiceMock.Setup(m => m.GetReceivedMessages()).Returns(new byte[0]);
            tcpServiceMock.Setup(m => m.SendMessage(It.IsAny<byte>())).Returns(Task.CompletedTask);
            var result = await homeController.Index(new ExchangedData() { Messsage=1}) as ViewResult; //returns null if result is not of type ViewResult

            //check that SendMessage is invoked and 1 is passed as parameter
            tcpServiceMock.Verify(m => m.SendMessage(It.Is<byte>(data => data == 1)), Times.Once);
        }

        [Fact]
        public async Task PostIndexShouldReturnBadRequest()
        {
            tcpServiceMock.Setup(m => m.GetReceivedMessages()).Returns(new byte[0]);
            tcpServiceMock.Setup(m => m.SendMessage(It.IsAny<byte>())).Returns(Task.CompletedTask);
            var result = await homeController.Index(new ExchangedData()) as BadRequestResult; //returns null if result is not of type ViewResult

            Assert.NotNull(result);
        }
    }
}
