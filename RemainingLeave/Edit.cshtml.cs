using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.RemainingLeave
{
    public class EditModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public EditModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["EmployeesId"] = new SelectList(_context.Employees, "ID", "FullName");
           ViewData["LeaveReasonsId"] = new SelectList(_context.LeaveReasons, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RemainingLeaves).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemainingLeavesExists(RemainingLeaves.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RemainingLeavesExists(int id)
        {
            return _context.RemainingLeaves.Any(e => e.Id == id);
        }
    }
}
