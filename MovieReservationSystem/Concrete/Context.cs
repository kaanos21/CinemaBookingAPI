using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MovieReservationSystem.Concrete
{
    public class Context : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
