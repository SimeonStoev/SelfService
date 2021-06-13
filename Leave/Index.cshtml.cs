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

namespace SelfServiceHrProject.Pages.Leave
{
    public class IndexModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public IndexModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnPostReasons()
        {
            return RedirectToPage("/LeaveReason/Index");
        }

        public IActionResult OnPostRemaining()
        {
            return RedirectToPage("/RemainingLeave/Index");
        }

        public IList<Leaves> Leaves { get;set; }

        [BindProperty(Name = "Role", SupportsGet = true)]
        public int Role { get; set; }

        public async Task OnGetAsync()
        {
            this.Role = (int)HttpContext.Session.GetInt32("Role");

            Leaves = await _context.Leaves
                .Include(l => l.Emplpoyee)
                .Include(l => l.LeaveReasons).ToListAsync();
        }
    }
}
