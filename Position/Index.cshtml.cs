using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Position
{
    public class IndexModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public IndexModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }

        public IList<Positions> Positions { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            IQueryable<Positions> postions = from s in _context.Positions
                                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                postions = postions.Where(s => s.Name.Contains(searchString));
            }

            Positions = await postions.ToListAsync();
        }
    }
}
