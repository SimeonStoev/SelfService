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

namespace SelfServiceHrProject.Pages.RemainingLeave
{
    public class IndexModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public IndexModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public IList<RemainingLeaves> RemainingLeaves { get;set; }

        [BindProperty(Name = "Role", SupportsGet = true)]
        public int Role { get; set; }

        public async Task OnGetAsync()
        {

            this.Role = (int)HttpContext.Session.GetInt32("Role");

            var employeeId = (int)HttpContext.Session.GetInt32("EmployeeId");

            if(this.Role == 2)
            {
                RemainingLeaves = await _context.RemainingLeaves
                .Include(r => r.Employee)
                .Include(r => r.LeaveReason).ToListAsync();
            }
            else
            {
                RemainingLeaves = await _context.RemainingLeaves
                .Include(r => r.Employee)
                .Include(r => r.LeaveReason)
                .Where(r => r.EmployeesId == employeeId)
                .ToListAsync();
            }
        }
    }
}
