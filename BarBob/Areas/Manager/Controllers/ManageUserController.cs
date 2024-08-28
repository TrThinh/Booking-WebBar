using BarBob.Models;
using BarBob.Models.ViewModels;
using BarBob.Repository;
using BarBob.Repository.IRepository;
using BarBob.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BarBob.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = SD.Role_Manager)]
    public class ManageUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public ManageUserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            UserVM userVM = CreateUserVM();

            return View(userVM);
        }

        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private UserVM CreateUserVM()
        {
            UserVM userVM = new UserVM()
            {
                User = new User(),
                RoleList = _roleManager.Roles
                    .Where(role => role.Name != SD.Role_Manager && role.Name != "Admin")
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Name
                    })
            };
            return userVM;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = userVM.User.FirstName;
                user.LastName = userVM.User.LastName;
                user.Email = userVM.User.Email;
                user.UserName = userVM.User.Email;
                user.PhoneNumber = userVM.User.PhoneNumber;
                user.Birthday = userVM.User.Birthday;
                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, userVM.Password);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(userVM.User.Role))
                    {
                        await _userManager.AddToRoleAsync(user, userVM.User.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }
                    TempData["Success"] = "User created successfully";
                    return Redirect("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            UserVM newUserVM = CreateUserVM();
            newUserVM.User = userVM.User;
            TempData["Error"] = "Error creating user";
            return View(newUserVM);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _unitOfWork.User.GetAllIncluding().ToList();
            var userList = new List<User>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.Role = roles.FirstOrDefault();
                userList.Add(user);
            }

            return Json(new { data = userList });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID is required");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // If user has role is Admin will display "message"
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                return BadRequest(new { success = false, message = "Cannot delete a user with the Admin role" });
            }

            var deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                _unitOfWork.Save();
                return Ok(new { success = true, message = "User deleted successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Error while deleting user" });
            }
        }


        #endregion
    }
}
