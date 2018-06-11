using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessMonitor.Models;
using Server;
using ProcessMonitor.DAL;

namespace ProcessMonitor.Controllers
{
    public class HomeController : Controller
    {
        private static bool tcpInitialized = false;
        private static AsyncServer _tcpService=new AsyncServer();

        //public HomeController(AsyncServer tcpService)
        //{
        //    _tcpService = tcpService;
        //}

        public HomeController()
        {
            if (!tcpInitialized)
            {
                //_tcpService = new AsyncServer();
                AsyncServer.DataReceived += new EventHandler<MyEventArgs>(DataReceivedEventHandler);
                //_tcpService.StartListening();
                tcpInitialized = true;
            }
        }
        public ActionResult Index()
        {
            AsyncServer tcp = new AsyncServer();
            AsyncServer.DataReceived += new EventHandler<MyEventArgs>(DataReceivedEventHandler);
            return View();
        }

        public IActionResult History()
        {
            DataAccess.LoadData();
            return View();
        }

        private void DataReceivedEventHandler(object sender, MyEventArgs e)
        {
            var obiect = new ExchangedData(e.Data);

            //DataAccess.Insert(obiect);
        }

        public ActionResult Start()
        {
            //logica de send
            AsyncServer.startByte = 1;
            AsyncServer.buttonStartPressed = true;
            return RedirectToAction("Home/Index");
        }

        public ActionResult Stop()
        {
            //logica de send
            AsyncServer.startByte = 2;
            AsyncServer.buttonStopPressed = true;
            return RedirectToAction("Home/Index");
        }

        
    }
}
