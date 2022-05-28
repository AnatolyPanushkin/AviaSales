using System.Runtime.InteropServices;
using AviaSales.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Data
{
    public class AviaSalesContext:DbContext
    {
        public AviaSalesContext()
        {
            
        }

        public AviaSalesContext(DbContextOptions options) : base(options)
        {
            
        }
        

        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Town> Towns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasOne(e => e.DepartTown).WithMany(e => e.DepartTickets)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.ArriveTown).WithMany(e => e.ArriveTickets)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            /*modelBuilder.Entity<Ticket>()
                .HasOne(e => e.DepartTown).WithMany(e => e.DepartTickets)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.ArriveTown).WithMany(e => e.ArriveTickets)
                .OnDelete(DeleteBehavior.SetNull);*/

            modelBuilder.Entity<Passenger>().HasOne(e => e.Ticket).WithOne(e => e.Passenger)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey("Ticket");
            
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=TicketSales;Trusted_Connection=True;Integrated Security=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}