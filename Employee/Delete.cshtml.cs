using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DeleteModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employees Employees { get; set; }
        public Salary Salary { get; set; }
        public Address Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employees = await _context.Employees
                .Include(e => e.Oragnisation)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.ID == id);

            Salary = await _context.Salary
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeesId == id);

            Address = await _context.Address
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeesId == id);

            if (Employees == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employees = await _context.Employees.FindAsync(id);

            if (Employees != null)
            {
                _context.Employees.Remove(Employees);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
