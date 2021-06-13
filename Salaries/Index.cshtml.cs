using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Salaries
{
    public class IndexModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public IndexModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty(Name = "Role", SupportsGet = true)]
        public int Role { get; set; }

        [BindProperty(Name = "Employee id", SupportsGet = true)]
        public int EmployeeId { get; set; }

        public IList<Salary> Salary { get;set; }

        public async Task OnGetAsync()
        {
            this.Role = (int)HttpContext.Session.GetInt32("Role");

            this.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");

            Salary = await _context.Salary
                .Include(s => s.Employee).ToListAsync();
        }
    }
}
