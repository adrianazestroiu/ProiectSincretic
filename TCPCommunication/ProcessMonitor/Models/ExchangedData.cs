using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessMonitor.Models
{
    public class ExchangedData
    {
        public byte Messsage { get; set; }
        public string MesssageBack { get; set; }

        public byte[] Bytes { get; set; }
        public bool Buton1 { get; set; }
        public bool Buton2 { get; set; }
        public bool Buton3 { get; set; }
        public bool Buton4 { get; set; }

        public bool Senzor1 { get; set; }
        public bool Senzor2 { get; set; }
        public bool Senzor3 { get; set; }
        public bool Senzor4 { get; set; }

        public bool Alarma { get; set; }

        public bool Pompa1 { get; set; }
        public bool Pompa2 { get; set; }
        public bool Pompa3 { get; set; }
        public bool Pompa4 { get; set; }

        public bool Inchidere { get; set; }
        public ExchangedData(byte[] bytes)
        {
            MesssageBack = string.Empty;

            //butoane
            Buton1 = new bool();
            Buton2 = new bool();
            Buton3 = new bool();
            Buton4 = new bool();


            //senzori
            Senzor1 = new bool();
            Senzor2 = new bool();
            Senzor3 = new bool();
            Senzor4 = new bool();

            //alarma
            Alarma = new bool();

            //pompe
            Pompa1 = new bool();
            Pompa2 = new bool();
            Pompa3 = new bool();
            Pompa4 = new bool();

            //inchidere sistem
            Inchidere = new bool();

            Bytes = bytes;

            Inchidere = (bytes[0] & 1) != 0 ? true : false;

            Pompa1 = (bytes[0] & 1 << 1) != 0 ? true : false;
            Pompa2 = (bytes[0] & 1 << 2) != 0 ? true : false;
            Pompa3 = (bytes[0] & 1 << 3) != 0 ? true : false;
            Pompa4 = (bytes[0] & 1 << 4) != 0 ? true : false;

            Alarma = (bytes[0] & 1 << 5) != 0 ? true : false;

            Buton1 = (bytes[0] & 1 << 6) != 0 ? true : false;
            Buton2 = (bytes[0] & 1 << 7) != 0 ? true : false;
            Buton3 = (bytes[7] & 1) != 0 ? true : false;
            Buton4 = (bytes[7] & 1 << 1) != 0 ? true : false;
        }
    }
}
