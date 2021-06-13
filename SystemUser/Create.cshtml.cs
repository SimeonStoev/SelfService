using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.SystemUser
{
    public class CreateModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public CreateModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var roles = new List<Roles>{
                            new Roles {Id = 1, Value = "Employee"},
                            new Roles{ Id = 2, Value = "Manager"}
            };

            ViewData["EmployeesId"] = new SelectList(_context.Employees, "ID", "FullName");

            ViewData["Roles"] = new SelectList(roles, "Id", "Value");

            return Page();
        }

        [BindProperty]
        public SystemUsers SystemUsers { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SystemUsers.Add(SystemUsers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
