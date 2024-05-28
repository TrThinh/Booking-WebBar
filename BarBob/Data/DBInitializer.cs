﻿using BarBob.Data;
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

            //if (!_db.Feedbacks.Any())
            //{
            //    User user = _db.Users.FirstOrDefault(b => b.UserName == "customer@gmail.com");

            //    if (user == null)
            //    {
            //        return;
            //    }

            //    var feedbacks = new List<Feedback>
            //        {
            //            new Feedback { UserId = user.Id, FeedbackDate = new DateTime(2024, 2, 15), Title = "Good", Status = "I was very funny at there" },
            //            new Feedback { UserId = user.Id, FeedbackDate = new DateTime(2024, 2, 16), Title = "Good", Status = "I love that" }
            //        };

            //    _db.Feedbacks.AddRange(feedbacks);
            //    _db.SaveChanges();
            //}

            //if(!_db.Tables.Any())
            //{
            //    var tables = new List<Table>
            //    {
            //        new Table { Time = new TimeSpan(18,00,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(18,30,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(19,00,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(19,30,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(20,00,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(20,30,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(21,00,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(21,30,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(22,00,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(22,30,00), Type = "Indoor"},
            //        new Table { Time = new TimeSpan(18,00,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(18,30,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(19,00,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(19,30,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(20,00,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(20,30,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(21,00,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(21,30,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(22,00,00), Type = "Patio"},
            //        new Table { Time = new TimeSpan(22,30,00), Type = "Patio"}
            //    };

            //    _db.Tables.AddRange(tables);
            //    _db.SaveChanges();
            //}

        }

    }
}