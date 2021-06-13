using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.SystemUser
{
    public class EditModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public EditModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SystemUsers SystemUsers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SystemUsers = await _context.SystemUsers
                .Include(s => s.Employee).FirstOrDefaultAsync(m => m.ID == id);

            if (SystemUsers == null)
            {
                return NotFound();
            }

            var roles = new List<Roles>{
                            new Roles {Id = 1, Value = "Employee"},
                            new Roles{ Id = 2, Value = "Manager"}
            };

            ViewData["EmployeesId"] = new SelectList(_context.Employees, "ID", "FullName");

            ViewData["Roles"] = new SelectList(roles, "Id", "Value");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SystemUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemUsersExists(SystemUsers.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SystemUsersExists(int id)
        {
            return _context.SystemUsers.Any(e => e.ID == id);
        }
    }
}
