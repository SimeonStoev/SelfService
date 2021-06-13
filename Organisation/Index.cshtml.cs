using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Organisation
{
    public class IndexModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public IndexModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }

        public IList<Organisations> Organisations { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            this.CurrentFilter = searchString;

            IQueryable<Organisations> organisations = from s in _context.Organisations
                                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                organisations = organisations.Where(s => s.Name.Contains(searchString));
            }

            Organisations = await organisations.ToListAsync();
        }
    }
}
