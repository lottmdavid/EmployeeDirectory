using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeDirectory.Data;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDirectoryContext _context;

        public EmployeesController(EmployeeDirectoryContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.FirstNameSortParm = sortOrder == "first_name_asc" ? "first_name_desc" : "first_name_asc";
            ViewBag.LastNameSortParm = sortOrder == "last_name_asc" ? "last_name_desc" : "last_name_asc";
            ViewBag.EmailSortParm = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            ViewBag.PhoneSortParm = sortOrder == "phone_asc" ? "phone_desc" : "phone_asc";
            ViewBag.StreetSortParm = sortOrder == "street_asc" ? "street_desc" : "street_asc";
            ViewBag.CitySortParm = sortOrder == "city_asc" ? "city_desc" : "city_asc";
            ViewBag.StateSortParm = sortOrder == "state_asc" ? "state_desc" : "state_asc";
            ViewBag.ZipSortParm = sortOrder == "zip_asc" ? "zip_desc" : "zip_asc";
            ViewBag.UpdatedDateSortParm = sortOrder == "updated_date_asc" ? "updated_date_desc" : "updated_date_asc";
            ViewBag.DepartmentNameSortParm = sortOrder == "department_name_asc" ? "department_name_desc" : "department_name_asc";
            
            var employeeDirectoryContext = _context.Employee.Include(e => e.Department);

            switch (sortOrder)
            {
                case "first_name_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.FirstName).ToListAsync());
                case "first_name_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.FirstName).ToListAsync());
                case "last_name_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.LastName).ToListAsync());
                case "last_name_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.LastName).ToListAsync());
                case "email_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.EmailAddress).ToListAsync());
                case "email_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.EmailAddress).ToListAsync());
                case "phone_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.Phone).ToListAsync());
                case "phone_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.Phone).ToListAsync());
                case "street_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.Street).ToListAsync());
                case "street_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.Street).ToListAsync());
                case "city_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.City).ToListAsync());
                case "city_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.City).ToListAsync());
                case "state_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.State).ToListAsync());
                case "state_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.State).ToListAsync());
                case "zip_desc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.Zip).ToListAsync());
                case "zip_asc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.Zip).ToListAsync());
                case "updated_date_asc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.UpdatedDate).ToListAsync());
                case "updated_date_desc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.UpdatedDate).ToListAsync());
                case "department_name_asc":
                    return View(await employeeDirectoryContext.OrderByDescending(e => e.Department.Name).ToListAsync());
                case "department_name_desc":
                    return View(await employeeDirectoryContext.OrderBy(e => e.Department.Name).ToListAsync());
                default:
                    return View(await employeeDirectoryContext.OrderBy(e => e.EmployeeId).ToListAsync());
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentList"] = new SelectList(_context.Department, "DepartmentId", "Name", null);
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,EmailAddress,Phone,Street,City,State,Zip,UpdatedDate,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.UpdatedDate = DateTime.Now;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentList"] = new SelectList(_context.Department, "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,EmailAddress,Phone,Street,City,State,Zip,UpdatedDate,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.UpdatedDate = DateTime.Now;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
