using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.RemainingLeave
{
    public class CreateModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public CreateModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmployeesId"] = new SelectList(_context.Employees, "ID", "FullName");
        ViewData["LeaveReasonsId"] = new SelectList(_context.LeaveReasons, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public RemainingLeaves RemainingLeaves { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RemainingLeaves.Add(RemainingLeaves);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
