using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessMonitor.Models
{
    public class ExchangedData
    {
        public ExchangedData()
        {
            MesssageBack = string.Empty;

            //butoane
            Buton1 = new ConcurrentBag<bool>();
            Buton2 = new ConcurrentBag<bool>();
            Buton3 = new ConcurrentBag<bool>();
            Buton4 = new ConcurrentBag<bool>();

            //senzori
            Senzor1 = new ConcurrentBag<bool>();
            Senzor2 = new ConcurrentBag<bool>();
            Senzor3 = new ConcurrentBag<bool>();
            Senzor4 = new ConcurrentBag<bool>();

            //alarma
            Alarma = new ConcurrentBag<bool>();

            //pompe
            Pompa1 = new ConcurrentBag<bool>();
            Pompa2 = new ConcurrentBag<bool>();
            Pompa3 = new ConcurrentBag<bool>();
            Pompa4 = new ConcurrentBag<bool>();

            //inchidere sistem
            Inchidere = new ConcurrentBag<Boolean>();

        }

        public byte Messsage { get; set; }
        public string MesssageBack { get; set; }

        public ConcurrentBag<bool> Buton1 { get; set; }
        public ConcurrentBag<bool> Buton2 { get; set; }
        public ConcurrentBag<bool> Buton3 { get; set; }
        public ConcurrentBag<bool> Buton4 { get; set; }

        public ConcurrentBag<bool> Senzor1 { get; set; }
        public ConcurrentBag<bool> Senzor2 { get; set; }
        public ConcurrentBag<bool> Senzor3 { get; set; }
        public ConcurrentBag<bool> Senzor4 { get; set; }

        public ConcurrentBag<bool> Alarma { get; set; }

        public ConcurrentBag<bool> Pompa1 { get; set; }
        public ConcurrentBag<bool> Pompa2 { get; set; }
        public ConcurrentBag<bool> Pompa3 { get; set; }
        public ConcurrentBag<bool> Pompa4 { get; set; }

        public ConcurrentBag<bool> Inchidere { get; set; }

    }
}
