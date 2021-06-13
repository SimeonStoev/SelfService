using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.RemainingLeave
{
    public class DetailsModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DetailsModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public RemainingLeaves RemainingLeaves { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RemainingLeaves = await _context.RemainingLeaves
                .Include(r => r.Employee)
                .Include(r => r.LeaveReason).FirstOrDefaultAsync(m => m.Id == id);

            if (RemainingLeaves == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
