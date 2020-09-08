using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Helper;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly DepartmentHelper _context;
        private readonly TempHelper _tempContext;

        public DepartmentController(TempHelper context)
        {
            _tempContext = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            return View(await _tempContext.DepartmentInfo.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentModel = await _tempContext.DepartmentInfo
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentModel == null)
            {
                return NotFound();
            }

            return View(departmentModel);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentID,DepartmentName,Description")] DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                _tempContext.Add(departmentModel);
                await _tempContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(departmentModel);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentModel = await _tempContext.DepartmentInfo.FindAsync(id);
            if (departmentModel == null)
            {
                return NotFound();
            }
            return View(departmentModel);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,DepartmentName,Description")] DepartmentModel departmentModel)
        {
            if (id != departmentModel.DepartmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tempContext.Update(departmentModel);
                    await _tempContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentModelExists(departmentModel.DepartmentID))
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
            return View(departmentModel);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentModel = await _tempContext.DepartmentInfo
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentModel == null)
            {
                return NotFound();
            }

            return View(departmentModel);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentModel = await _tempContext.DepartmentInfo.FindAsync(id);
            _tempContext.DepartmentInfo.Remove(departmentModel);
            await _tempContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentModelExists(int id)
        {
            return _tempContext.DepartmentInfo.Any(e => e.DepartmentID == id);
        }
    }
}
