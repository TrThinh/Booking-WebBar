using BarBob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace BarBob.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuidlder)
        {
            base.OnModelCreating(modelBuidlder);

        }
    }
}
