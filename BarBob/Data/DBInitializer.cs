using BarBob.Data;
using BarBob.Models;
using BarBob.Utility;
using Microsoft.AspNetCore.Identity;

namespace BarBob.Data
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DBInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            // create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Manager)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new User
                {
                    Id = "AdminID1",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Ad",
                    LastName = "Nguyen Van",
                    PhoneNumber = "1112223333"
                }, "Admin@123").GetAwaiter().GetResult();

                User userDB = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(userDB, SD.Role_Admin).GetAwaiter().GetResult();


                _userManager.CreateAsync(new User
                {
                    Id = "ManagerID1",
                    UserName = "manager@gmail.com",
                    Email = "manager@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Ma",
                    LastName = "Nguyen Van",
                    PhoneNumber = "1112223333"
                }, "Admin@123").GetAwaiter().GetResult();


                userDB = _db.Users.FirstOrDefault(u => u.Email == "manager@gmail.com");
                _userManager.AddToRoleAsync(userDB, SD.Role_Manager).GetAwaiter().GetResult();

                _userManager.CreateAsync(new User
                {
                    Id = "EmployeeID1",
                    UserName = "employee@gmail.com",
                    Email = "employee@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Em",
                    LastName = "Nguyen Van",
                    PhoneNumber = "1112223333"
                }, "Admin@123").GetAwaiter().GetResult();

                userDB = _db.Users.FirstOrDefault(u => u.Email == "employee@gmail.com");
                _userManager.AddToRoleAsync(userDB, SD.Role_Employee).GetAwaiter().GetResult();

                _userManager.CreateAsync(new User
                {
                    Id = "CustomerID1",
                    UserName = "customer@gmail.com",
                    Email = "customer@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Cus",
                    LastName = "Nguyen Van",
                    PhoneNumber = "1112223333"
                }, "Admin@123").GetAwaiter().GetResult();

                userDB = _db.Users.FirstOrDefault(u => u.Email == "customer@gmail.com");
                _userManager.AddToRoleAsync(userDB, SD.Role_Customer).GetAwaiter().GetResult();
            }

            if (!_db.Feedbacks.Any())
            {
                User user = _db.Users.FirstOrDefault(b => b.UserName == "customer@gmail.com");

                if (user == null)
                {
                    return;
                }

                var feedbacks = new List<Feedback>
                    {
                        new Feedback { UserId = user.Id, FeedbackDate = new DateTime(2024, 2, 15), Title = "Good", Status = "I was very funny at there" },
                        new Feedback { UserId = user.Id, FeedbackDate = new DateTime(2024, 2, 16), Title = "Good", Status = "I love that" }
                    };

                _db.Feedbacks.AddRange(feedbacks);
                _db.SaveChanges();
            }

            if (!_db.TableTypes.Any())
            {
                var tabletypes = new List<TableType>
                {
                    new TableType { Table_name = "Indoor", Price = "150.000" },
                    new TableType { Table_name = "Patio", Price = "200.000" },

                };

                _db.TableTypes.AddRange(tabletypes);
                _db.SaveChanges();
            }

            if (!_db.Tables.Any())
            {
                var tabletypes = _db.TableTypes.ToList();

                var indoorTable = tabletypes.FirstOrDefault(tt => tt.Table_name == "Indoor");
                var patioTable = tabletypes.FirstOrDefault(tt => tt.Table_name == "Patio");

                var tables = new List<Table>
                {
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" },
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" },
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" },
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" },
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" },
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" },
                    new Table { TableType = indoorTable, Description = "Private and chill with song", TableImg = "null" },
                    new Table { TableType = patioTable, Description = "Public and have Song Han Bridge", TableImg = "null" }

                };

                _db.Tables.AddRange(tables);
                _db.SaveChanges();
            }

            if (!_db.Bookings.Any())
            {
                User user = _db.Users.FirstOrDefault(b => b.UserName == "customer@gmail.com");
                Table table = _db.Tables.FirstOrDefault(t => t.Description == "Private and chill with song");


                if (user == null)
                {
                    return;
                }

                var bookings = new List<Booking>
                {
                    new Booking {  UserId = user.Id, TableId = table.Id, Guests = "2", BookingDate = new DateTime(2024,5,29), CheckinDate = new DateTime(2024, 6, 2), CheckinTime = new TimeSpan(18, 30, 00)}
                };

                _db.Bookings.AddRange(bookings);
                _db.SaveChanges();
            }
        }

    }
}
