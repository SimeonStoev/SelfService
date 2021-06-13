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

namespace SelfServiceHrProject.Pages.Adresses
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

        public IList<Address> Address { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            this.Role = (int)HttpContext.Session.GetInt32("Role");

            this.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");

            this.CurrentFilter = searchString;

            IQueryable<Address> addresses = from s in _context.Address
                                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                addresses = addresses.Where(s => s.LocalAddress.Contains(searchString) ||
                                                    s.Country.Contains(searchString) ||
                                                    s.City.Contains(searchString));
            }

            Address = await addresses
                .Include(a => a.Employee).ToListAsync();
        }
    }
}
