using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.LeaveReason
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
            return Page();
        }

        [BindProperty]
        public LeaveReasons LeaveReasons { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LeaveReasons.Add(LeaveReasons);
            await _context.SaveChangesAsync();

            var employees = _context.Employees.ToList();

            var leaveReasons = _context.LeaveReasons.ToList();

            foreach (var leaveReason in leaveReasons)
            {
                if (!this.RemainingLeavesExists(leaveReason.Id))
                {
                    foreach (var employee in employees)
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

        private bool RemainingLeavesExists(int leaveReasonId)
        {
            return _context.RemainingLeaves.Any(e => e.LeaveReasonsId == leaveReasonId);
        }
    }
}
