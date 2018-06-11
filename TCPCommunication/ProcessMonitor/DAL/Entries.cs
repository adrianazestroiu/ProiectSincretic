using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ProcessMonitor.DAL
{
    [Table("Entries")]
    public class Entries
    {
        public int Id { get; set; }
        public DateTime Timespan { get; set; }
        public String Stare { get; set; }

        public Entries() { }
        public Entries(int set_id,DateTime set_timespan,String set_stare)
        {
            Id = set_id;
            Timespan = set_timespan;
            Stare = set_stare;
        }
        public Entries(DateTime set_timespan, String set_stare)
        {
            Timespan = set_timespan;
            Stare = set_stare;
        }
    }
}
