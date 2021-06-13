using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Employee
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
        ViewData["OrganisationsId"] = new SelectList(_context.Set<Organisations>(), "Id", "Name");
        ViewData["PositionsId"] = new SelectList(_context.Set<Positions>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Employees Employees { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employees.Add(Employees);
            await _context.SaveChangesAsync();

            var leaveReasons = _context.LeaveReasons.ToList();

            var employees = _context.Employees.ToList();

            foreach (var employee in employees)
            {
                if (!this.RemainingLeavesExists(employee.ID))
                {
                    foreach (var leaveReason in leaveReasons)
                    {

                        var remainingLeaves = new RemainingLeaves
                        {
                            RemainingDays = leaveReason.Days,
                            EmployeesId = employee.ID,
                            LeaveReasonsId = leaveReason.Id
                        };

                        _context.RemainingLeaves.Add(remainingLeaves);

                    }
                }

            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool RemainingLeavesExists(int employeeId)
        {
            return _context.RemainingLeaves.Any(e => e.EmployeesId == employeeId);
        }
    }
}
