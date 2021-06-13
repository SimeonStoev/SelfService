using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Salaries
{
    public class DetailsModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public DetailsModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public Salary Salary { get; set; }
        
        [BindProperty]
        public double GrossSalary { get; set; }
        public double HealthInsuarenceTax { get; set; }

        public double PensionTax { get; set; }

        public double OtherTaxes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Salary = await _context.Salary
                .Include(s => s.Employee).FirstOrDefaultAsync(m => m.Id == id);

            if (Salary == null)
            {
                return NotFound();
            }
            this.GrossSalary = (double)(Salary.NetSalary + Salary.BonusSalary) / 0.8;

            // Taxes - HealthIns is 25 %, Pension is 25 %, Other taxes is 50 %

            var taxes = this.GrossSalary - (double)(Salary.FullNetSalary);

            this.HealthInsuarenceTax = taxes * 0.25;

            this.PensionTax = taxes * 0.25;

            this.OtherTaxes = taxes * 0.5;

            return Page();
        }
    }
}
