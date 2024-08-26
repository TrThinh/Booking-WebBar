using BarBob.Data;
using BarBob.Models;
using BarBob.Models.ViewModels;
using BarBob.Repository.IRepository;
using BarBob.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BarBob.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ManageBranchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public ManageBranchController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Table());
            }
            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            return View(table);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var tables = _unitOfWork.Table.GetAllIncluding().Select(t => new
            {
                table_name = t.Table_name,
                description = t.Description,
                price = t.Price,
                quantity = t.Quantity,
                id = t.Id
            }).ToList();

            return Json(new { data = tables });
        }

        [HttpPost]
        public IActionResult Index(Table table)
        {
            if (ModelState.IsValid)
            {
                if (table.Id == 0)
                {
                    _unitOfWork.Table.Add(table);
                    TempData["success"] = "Table created successfully";
                }
                else
                {
                    _unitOfWork.Table.Update(table);
                    TempData["success"] = "Table updated successfully";
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "ManageBranch");
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return Json(new { data = table });
        }

        [HttpPost]
        public IActionResult CreateTable(Table table)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Table.Add(table);
                _unitOfWork.Save();
                TempData["success"] = "Dinner created successfully";
                return RedirectToAction("Index", "ManageBranch");
            }
            return View(table);
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            if (table == null)
            {
                return BadRequest(new { success = false, message = "Error while deleting table" });
            }
            _unitOfWork.Table.Remove(table);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Delete table: \"" + table.Id + "\" successful" });
        }

        #endregion
    }
}
