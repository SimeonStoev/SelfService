using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.LeaveReason
{
    public class DeleteModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DeleteModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeaveReasons LeaveReasons { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveReasons = await _context.LeaveReasons.FirstOrDefaultAsync(m => m.Id == id);

            if (LeaveReasons == null)
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

            LeaveReasons = await _context.LeaveReasons.FindAsync(id);

            if (LeaveReasons != null)
            {
                _context.LeaveReasons.Remove(LeaveReasons);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
