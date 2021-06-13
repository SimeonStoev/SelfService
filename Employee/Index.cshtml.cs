using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Pages.Employee
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

        public string NameSort { get; set; }
        public string BirthdaySort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }

        public IList<Employees> Employees { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            this.Role = (int)HttpContext.Session.GetInt32("Role");

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            BirthdaySort = sortOrder == "Date" ? "date_desc" : "Date";

            this.CurrentFilter = searchString;

            IQueryable<Employees> employees = from s in _context.Employees
                                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Name.Contains(searchString)
                                       || s.Family.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    employees = employees.OrderBy(s => s.Birthdate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.Birthdate);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Name);
                    break;
            }

            Employees = await employees
                .Include(e => e.Oragnisation)
                .Include(e => e.Position).ToListAsync();

        }

        public async Task<FileResult> OnPostExportAsync()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("ID"),
                                    new DataColumn("Name"),
                                    new DataColumn("Surname"),
                                    new DataColumn("Family"),
                                    new DataColumn("EGN"),
                                    new DataColumn("Birthdate"),
                                    new DataColumn("PhoneNumber"),
                                    new DataColumn("Organisation"),
                                    new DataColumn("Position")});

            var employees = await _context.Employees
                .Include(e => e.Oragnisation)
                .Include(e => e.Position).ToListAsync();

            foreach (var employee in employees)
            {
                dt.Rows.Add(employee.ID, employee.Name, employee.Surname, employee.Family,
                        employee.EGN, employee.Birthdate, employee.PhoneNumber, employee.Oragnisation.Name, employee.Position.Name);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
                }
            }
        }
    }
}
