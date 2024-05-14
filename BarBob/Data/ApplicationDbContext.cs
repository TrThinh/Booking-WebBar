using BarBob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace BarBob.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuidlder)
        {
            base.OnModelCreating(modelBuidlder);

            //modelBuidlder.Entity<IdentityRole>().HasData(
            //    new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN"},
            //    new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER"},
            //    new IdentityRole { Id = "3", Name = "Employee", NormalizedName = "EMPLOYEE"},
            //    new IdentityRole { Id = "4", Name = "Customer", NormalizedName = "CUSTOMER"}
            //    );
        }
    }
}
