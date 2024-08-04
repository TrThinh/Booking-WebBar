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
                return View(new TableType());
            }
            TableType tabletype = _unitOfWork.TableType.Get(t => t.Id == id);
            return View(tabletype);
        }

        public IActionResult Details(int? id)
        {
            var tableTypes = _unitOfWork.TableType.GetAll().Select(tt => new SelectListItem
            {
                Value = tt.Id.ToString(),
                Text = tt.Table_name
            }).ToList();

            if (id == null || id == 0)
            {
                return View(new TableVM
                {
                    TableTypeList = tableTypes
                });
            }

            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            TableType tableType = _unitOfWork.TableType.Get(tt => tt.Id == table.TableTypeId);
            if (tableType == null)
            {
                return NotFound();
            }

            var viewModel = new TableVM
            {
                Table = table,
                TableType = tableType,
                TableTypeList = tableTypes
            };

            return View(viewModel);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tabletypes = _unitOfWork.TableType.GetAllIncluding().ToList();
            return Json(new { data = tabletypes });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTable()
        {
            var tableList = _unitOfWork.Table.GetAll(includeProperties: "TableType").ToList();

            var tabledetails = tableList.Select(t => new
            {
                id = t.Id,
                description = t.Description,
                tableType = new { table_Name = t.TableType.Table_name }
            }).ToList();

            return Json(new { data = tabledetails });
        }

        [HttpPost]
        public IActionResult Index(TableType tabletype)
        {
            if (ModelState.IsValid)
            {
                if (tabletype.Id == 0)
                {
                    _unitOfWork.TableType.Add(tabletype);
                    TempData["success"] = "Table created successfully";
                }
                else
                {
                    _unitOfWork.TableType.Update(tabletype);
                    TempData["success"] = "Table updated successfully";
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "ManageBranch");
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            TableType tabletype = _unitOfWork.TableType.Get(t => t.Id == id);
            if (tabletype == null)
            {
                return NotFound();
            }

            return Json(new { data = tabletype });
        }

        [HttpPost]
        public IActionResult CreateTable(TableType tabletype)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TableType.Add(tabletype);
                _unitOfWork.Save();
                TempData["success"] = "Dinner created successfully";
                return RedirectToAction("Index", "ManageBranch");
            }
            return View(tabletype);
        }

        //[HttpPost]
        //public IActionResult CreateDetailTable(Table table)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Table.Add(table);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Table created successfully";
        //        return RedirectToAction("Details", "ManageBranch");
        //    }
        //    return View(table);
        //}

        [HttpPost]
        public IActionResult CreateOrUpdateTable(TableVM tableVM)
        {
            if (ModelState.IsValid)
            {
                if (tableVM.Table.Id == 0)
                {
                    _unitOfWork.Table.Add(tableVM.Table);
                }
                else
                {
                    _unitOfWork.Table.Update(tableVM.Table);
                }
                _unitOfWork.Save();
                TempData["success"] = "Table saved successfully";
                return RedirectToAction(nameof(Details));
            }

            tableVM.TableTypeList = _unitOfWork.TableType.GetAll().Select(tt => new SelectListItem
            {
                Value = tt.Id.ToString(),
                Text = tt.Table_name
            });

            return View("Details", tableVM);
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            TableType tabletype = _unitOfWork.TableType.Get(t => t.Id == id);
            if (tabletype == null)
            {
                return BadRequest(new { success = false, message = "Error while deleting table" });
            }
            _unitOfWork.TableType.Remove(tabletype);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Delete table: \"" + tabletype.Id + "\" successful" });
        }

        [HttpDelete]
        public IActionResult DeleteDetailsTableById(int id)
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
