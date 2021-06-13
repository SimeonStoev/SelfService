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
    public class DetailsModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DetailsModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public Leaves Leaves { get; set; }

        [BindProperty]
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
            else if (Leaves.Status == 1)
            {
                Status = "Standing";
            }
            else
            {
                Status = "Approved";
            }

            return Page();
        }
    }
}
