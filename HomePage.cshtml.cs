using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SelfServiceHrProject.Pages
{
    public class HomePageModel : PageModel
    {

        [BindProperty(Name = "Role", SupportsGet = true)]
        public int Role { get; set; }

        public async Task<IActionResult> OnPostOraganisationsAsync()
        {
            return RedirectToPage("/Organisation/Index");
        }

        public async Task<IActionResult> OnPostPositionsAsync()
        {
            return RedirectToPage("/Position/Index");
        }

        public async Task<IActionResult> OnPostUsersAsync()
        {
            return RedirectToPage("/SystemUser/Index");
        }

        public void OnGet()
        {
            this.Role = (int)HttpContext.Session.GetInt32("Role");
        }
    }
}
