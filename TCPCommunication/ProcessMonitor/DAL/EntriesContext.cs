using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessMonitor.DAL
{
    public class EntriesContext : DbContext
    {
        public EntriesContext()
        {
        }

        public EntriesContext(DbContextOptions<EntriesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entries> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entries>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Timespan)
                    .HasColumnName("Timespan ")
                    .HasColumnType("datetime");
            });
        }
    }
}
