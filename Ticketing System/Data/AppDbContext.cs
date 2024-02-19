using Microsoft.EntityFrameworkCore;

namespace Ticketing_System.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure cascade delete for Concert-Performance relationship
            modelBuilder.Entity<Concert>()
                .HasMany(c => c.Performances)
                .WithOne(p => p.Concert)
                .HasForeignKey(p => p.ConcertId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure cascade delete for Performance-Reservation relationship
            modelBuilder.Entity<Performance>()
                .HasMany(p => p.Reservations)
                .WithOne(r => r.Performance)
                .HasForeignKey(r => r.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConcertHall>().HasData(
                new ConcertHall() { Id = 1, Name = "Mozart", NumberOfSeats= 200},
                new ConcertHall() { Id = 2, Name = "Beethoven", NumberOfSeats = 500 },
                new ConcertHall() { Id = 3, Name = "Vivaldi", NumberOfSeats = 750 },
                new ConcertHall() { Id = 4, Name = "Bach", NumberOfSeats = 400 },
                new ConcertHall() { Id = 5, Name = "Chopin", NumberOfSeats = 50 }
                );
        }

        public DbSet<Concert> Concert { get; set; }
        public DbSet<ConcertHall> ConcertHall { get; set; }
        public DbSet<Performance> Performance { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

    }
}
