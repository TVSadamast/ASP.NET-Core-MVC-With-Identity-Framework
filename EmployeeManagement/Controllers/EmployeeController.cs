using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Helper;
using EmployeeManagement.Models;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly EmployeeHelper _context;
        private readonly TempHelper _tempContext;
        //EmployeeModel employeeModel;

        public EmployeeController(TempHelper context)
        {
            _tempContext = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employeeModel = from p in _tempContext.EmployeeInfo
                                      join d in _tempContext.DepartmentInfo on new { DepartmentID = (int?)Convert.ToInt32(p.DepartmentName) } equals new { DepartmentID = (int?)d.DepartmentID } into employee
                                      from d in employee.DefaultIfEmpty()
                                      select new EmployeeModel
                                      {
                                          EmployeeID = p.EmployeeID,
                                          Name = p.Name,
                                          Surname = p.Surname,
                                          Address = p.Address,
                                          Qualification = p.Qualification,
                                          Contact = p.Contact,
                                          DepartmentName = d.DepartmentName
                                      };
            return View(await employeeModel.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeeModel = (from p in _tempContext.EmployeeInfo
                                join d in _tempContext.DepartmentInfo on new { DepartmentID = (int?)Convert.ToInt32(p.DepartmentName) } equals new { DepartmentID = (int?)d.DepartmentID } into employee
                                from d in employee.DefaultIfEmpty()
                                select new EmployeeModel
                                {
                                    EmployeeID = p.EmployeeID,
                                    Name = p.Name,
                                    Surname = p.Surname,
                                    Address = p.Address,
                                    Qualification = p.Qualification,
                                    Contact = p.Contact,
                                    DepartmentName = d.DepartmentName
                                }).FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(await employeeModel);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            Select_DepartmentDetails();
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,Name,Surname,Address,Qualification,Contact,DepartmentName")] EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                _tempContext.Add(employeeModel);
                await _tempContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Select_DepartmentDetails();
            var employeeModel = (from p in _tempContext.EmployeeInfo
                                 join d in _tempContext.DepartmentInfo on new { DepartmentID = (int?)Convert.ToInt32(p.DepartmentName) } equals new { DepartmentID = (int?)d.DepartmentID } into employee
                                 from d in employee.DefaultIfEmpty()
                                 select new EmployeeModel
                                 {
                                     EmployeeID = p.EmployeeID,
                                     Name = p.Name,
                                     Surname = p.Surname,
                                     Address = p.Address,
                                     Qualification = p.Qualification,
                                     Contact = p.Contact,
                                     DepartmentName = d.DepartmentName
                                 }).FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(await employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,Name,Surname,Address,Qualification,Contact,DepartmentName")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tempContext.Update(employeeModel);
                    await _tempContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeeModel = (from p in _tempContext.EmployeeInfo
                                 join d in _tempContext.DepartmentInfo on new { DepartmentID = (int?)Convert.ToInt32(p.DepartmentName) } equals new { DepartmentID = (int?)d.DepartmentID } into employee
                                 from d in employee.DefaultIfEmpty()
                                 select new EmployeeModel
                                 {
                                     EmployeeID = p.EmployeeID,
                                     Name = p.Name,
                                     Surname = p.Surname,
                                     Address = p.Address,
                                     Qualification = p.Qualification,
                                     Contact = p.Contact,
                                     DepartmentName = d.DepartmentName
                                 }).FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(await employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeModel = await (from p in _tempContext.EmployeeInfo
                                       join d in _tempContext.DepartmentInfo on new { DepartmentID = (int?)Convert.ToInt32(p.DepartmentName) } equals new { DepartmentID = (int?)d.DepartmentID } into employee
                                       from d in employee.DefaultIfEmpty()
                                       select new EmployeeModel
                                       {
                                           EmployeeID = p.EmployeeID,
                                           Name = p.Name,
                                           Surname = p.Surname,
                                           Address = p.Address,
                                           Qualification = p.Qualification,
                                           Contact = p.Contact,
                                           DepartmentName = d.DepartmentName
                                       }).FirstOrDefaultAsync(m => m.EmployeeID == id);

            _tempContext.EmployeeInfo.Remove(employeeModel);
            await _tempContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(int id)
        {
            return _tempContext.EmployeeInfo.Any(e => e.EmployeeID == id);
        }

        public void Select_DepartmentDetails()
        {
            List<SelectListItem> departmentNameList = new List<SelectListItem>();
            var departmentData = _tempContext.DepartmentInfo.ToList();

            foreach (var data in departmentData)
            {
                departmentNameList.Add(new SelectListItem { Text = data.DepartmentName, Value = data.DepartmentID.ToString() });
            }
            ViewBag.DepartmentName = departmentNameList;
        }
    }
}
