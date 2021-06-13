using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfServiceHrProject.Data;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Leave
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
            var status = new List<LeaveStatus>{
                            //new LeaveStatus {Id = 0, Value = "Canceled"},
                            new LeaveStatus{ Id = 1, Value = "Standing"}
                            //new LeaveStatus{ Id = 2, Value = "Approved"}
            };

            ViewData["EmployeesId"] = new SelectList(_context.Employees, "ID", "FullName");
            ViewData["LeaveReasonsId"] = new SelectList(_context.LeaveReasons, "Id", "Name");
            ViewData["Status"] = new SelectList(status, "Id", "Value");
            return Page();
        }

        [BindProperty]
        public Leaves Leaves { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Set the status to standing
            Leaves.Status = 1;

            var employeeId = (int)HttpContext.Session.GetInt32("EmployeeId");

            Leaves.EmployeesId = employeeId;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var days = Leaves.Days;
            var leaveReason = Leaves.LeaveReasonsId;
            var employee = Leaves.EmployeesId;
            var startDate = Leaves.StartDate;
            var endDate = Leaves.EndDate;

            var isLeaveAcceptable = this.isLeaveForReasonsAcceptable(days, leaveReason, employee, startDate, endDate);

            if (isLeaveAcceptable)
            {
                await this.updateRamainingLeavesDaysAsync(days, leaveReason, employee);
            }
            else
            {
                return RedirectToPage("../Error");
            }

            _context.Leaves.Add(Leaves);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public bool isLeaveForReasonsAcceptable(int days, int leaveReason, int employee, DateTime startDate, DateTime endDate)
        {
            var remainingLeavesForEmployeeTemp = _context.RemainingLeaves
                                            .Where(e => e.EmployeesId == employee);

            var leavesAboutEmployee = _context.Leaves.Where(e => e.EmployeesId == employee).ToList();

            bool areDatesNotCorrect = false;

            foreach(var leave in leavesAboutEmployee)
            {
                if(DateTime.Compare(leave.StartDate, endDate) <= 0 && DateTime.Compare(leave.EndDate, startDate) >= 0)
                {
                    areDatesNotCorrect = true;
                }
            }

            var remainingLeavesForEmployee = remainingLeavesForEmployeeTemp.Where(e => e.LeaveReasonsId == leaveReason).Single();

            if(remainingLeavesForEmployee.RemainingDays < days || areDatesNotCorrect)
            {
                return false;
            }

            return true;
        }

        public async Task updateRamainingLeavesDaysAsync(int days, int leaveReason, int employee)
        {
            var remainingLeavesForEmployeeTemp = _context.RemainingLeaves
                                            .Where(e => e.EmployeesId == employee);

            var remainingLeavesForEmployee = remainingLeavesForEmployeeTemp.Where(e => e.LeaveReasonsId == leaveReason).Single();

            remainingLeavesForEmployee.RemainingDays -= days;

            await _context.SaveChangesAsync();

        }
    }
}
