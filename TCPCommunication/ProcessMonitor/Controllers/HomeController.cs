using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessMonitor.Models;
using ProcessMonitor.Services;

namespace ProcessMonitor.Controllers
{
    public class HomeController : Controller
    {
        private ITcpService _tcpService;

        public HomeController(ITcpService tcpService)
        {
            _tcpService = tcpService;
        }

        public IActionResult Index()
        {
            var model = CreateNewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ExchangedData data)
        {
            IActionResult result;
            if (data.Messsage != 0)
            {
                await _tcpService.SendMessage(data.Messsage);
                var model = CreateNewModel();
                result = View(model);
            }
            else
            {
                result = base.BadRequest();
            }
            return result;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private ExchangedData CreateNewModel()
        {
            var exData = new ExchangedData();
            var receivedData = _tcpService.GetReceivedMessages();
            var receivedDataByte1 = _tcpService.GetReceivedMessagesByte1();
            var receivedDataByte2 = _tcpService.GetReceivedMessagesByte2();

            exData.MesssageBack = receivedData
                                    .Aggregate(
                                                new StringBuilder(), 
                                                (builder, data) => builder.Append(data).Append(" "))
                                    .ToString()
                                    .TrimEnd();
            return exData;
        }
    }
}
