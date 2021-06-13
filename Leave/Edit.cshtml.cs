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

namespace SelfServiceHrProject.Pages.Leave
{
    public class EditModel : PageModel
    {
        private readonly SelfServiceHrProject.Data.SelfServiceHrProjectContext _context;

        public EditModel(SelfServiceHrProject.Data.SelfServiceHrProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Leaves Leaves { get; set; }
        public string Status { get; set; }

        public string AcceptStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leaves = await _context.Leaves
                .Include(l => l.Emplpoyee)
                .Include(l => l.LeaveReasons).FirstOrDefaultAsync(m => m.Id == id);

            if (Leaves == null)
            {
                return NotFound();
            }

            if (Leaves.Status == 0)
            {
                Status = "Отхвърлена";
            }
            else if (Leaves.Status == 1)
            {
                Status = "Чакаща";
            }
            else
            {
                Status = "Потвърдена";
            }

            var status = new List<LeaveStatus>{
                            new LeaveStatus {Id = 0, Value = "Отхвърли"},
                            new LeaveStatus{ Id = 2, Value = "Подвърди"}
            };

            ViewData["AcceptStatus"] = new SelectList(status, "Id", "Value");
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

            _context.Attach(Leaves).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavesExists(Leaves.Id))
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

        private bool LeavesExists(int id)
        {
            return _context.Leaves.Any(e => e.Id == id);
        }

        //public async Task OnPostAcceptAsync(int? id)
        //{
        //    var _id = id;
        //    // return RedirectToPage("/Leave/Index");
        //}

        //public async Task OnPostDeclineAsync(int? id)
        //{
        //    var _id = id;
        //    // return RedirectToPage("/Leave/Index");
        //}
    }
}
