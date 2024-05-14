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
            if (!_db.Branches.Any())
            {
                var branches = new List<Branch>
                {
                    new Branch { NameBranch = "Novotel Hotel Premier Han River", Address = "Novotel Hotel Premier Han River - 36-38 Bach Dang Street, Thach Thang Ward, Hai Chau District, Da Nang City, Viet Nam"},
                    new Branch { NameBranch = "Vinces Hotel", Address = "Vinces Hotel - 74 Do Phap Thuan, Hoa Cuong Bac Ward, Hai Chau District, Da Nang City, Viet Nam"}
                };

                _db.Branches.AddRange(branches);
                _db.SaveChanges();
            }

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

                Branch branch = _db.Branches.FirstOrDefault(b => b.NameBranch == "Novotel Hotel Premier Han River");

                if (branch != null)
                {
                    _userManager.CreateAsync(new User
                    {
                        Id = "ManagerID1",
                        UserName = "manager@gmail.com",
                        Email = "manager@gmail.com",
                        EmailConfirmed = true,
                        FirstName = "Ma",
                        LastName = "Nguyen Van",
                        PhoneNumber = "1112223333",
                        BranchId = branch.Id
                    }, "Admin@123").GetAwaiter().GetResult();
                }

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
                    PhoneNumber = "1112223333",
                    BranchId = branch.Id
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

            if (!_db.Slots.Any())
            {
                // Lấy Branch mà bạn muốn thêm
                Branch branch1 = _db.Branches.FirstOrDefault(b => b.NameBranch == "Novotel Hotel Premier Han River");
                Branch branch2 = _db.Branches.FirstOrDefault(b => b.NameBranch == "Vinces Hotel");

                if (branch1 != null && branch2 != null)
                {
                    var slots = new List<Slot>
                    {
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch1.Id, Status = 1, TablePosition = "Over view"},
                        //Branch 2
                        new Slot { BranchId = branch2.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch2.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch2.Id, Status = 1, TablePosition = "Over view"},
                        new Slot { BranchId = branch2.Id, Status = 1, TablePosition = "Over view"}

                    };

                    _db.Slots.AddRange(slots);
                    _db.SaveChanges();
                }
            }

            if (!_db.Feedbacks.Any())
            {
                Branch branch = _db.Branches.FirstOrDefault(b => b.NameBranch == "Novotel Hotel Premier Han River");
                User user = _db.Users.FirstOrDefault(b => b.UserName == "customer@gmail.com");

                if (user == null)
                {
                    return;
                }

                var feedbacks = new List<Feedback>
                    {
                        new Feedback { BranchId = branch.Id, UserId = user.Id, FeedbackDate = new DateTime(2024, 2, 15), Title = "Good", Status = "I was very funny at there" },
                        new Feedback { BranchId = branch.Id, UserId = user.Id, FeedbackDate = new DateTime(2024, 2, 16), Title = "Good", Status = "I love that" }
                    };

                _db.Feedbacks.AddRange(feedbacks);
                _db.SaveChanges();
            }

        }

    }
}
