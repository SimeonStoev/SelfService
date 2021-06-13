using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.SystemUser
{
    public class DeleteModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DeleteModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SystemUsers SystemUsers { get; set; }
        public string Role { get; set; }

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
            if(SystemUsers.Role == 2)
            {
                Role = "Manager";
            }
            else
            {
                Role = "Employee";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SystemUsers = await _context.SystemUsers.FindAsync(id);

            if (SystemUsers != null)
            {
                _context.SystemUsers.Remove(SystemUsers);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
