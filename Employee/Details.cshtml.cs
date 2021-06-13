using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Employee
{
    public class DetailsModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DetailsModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public Employees Employees { get; set; }
        public Salary Salary { get; set; }
        public Address Address { get; set; }

        [BindProperty(Name = "Role", SupportsGet = true)]
        public int Role { get; set; }

        [BindProperty(Name = "Birthday", SupportsGet = true)]
        public string BirthDate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.Role = (int)HttpContext.Session.GetInt32("Role");

            Employees = await _context.Employees
                .Include(e => e.Oragnisation)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.ID == id);

            this.BirthDate = Employees.Birthdate.ToString("dd MMMM",
                  CultureInfo.CreateSpecificCulture("en-US"));

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
    }
}
