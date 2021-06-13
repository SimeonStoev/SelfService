using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Leave
{
    public class DeleteModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DeleteModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Leaves Leaves { get; set; }
        public string Status { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leaves = await _context.Leaves
                .Include(l => l.Emplpoyee)
                .Include(l => l.LeaveReasons).FirstOrDefaultAsync(m => m.Id == id);

            if (Leaves == null)
            {
                return NotFound();
            }

            if (Leaves.Status == 0)
            {
                Status = "Rejected";
            }
            else if(Leaves.Status == 1)
            {
                Status = "Standing";
            }
            else
            {
                Status = "Approved";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leaves = await _context.Leaves.FindAsync(id);

            if (Leaves != null)
            {
                _context.Leaves.Remove(Leaves);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
