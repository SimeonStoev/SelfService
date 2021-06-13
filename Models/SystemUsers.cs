using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class SystemUsers
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Потребителски профил")]
        public int Role { get; set; }

        public int EmployeesId { get; set; }

        [Display(Name = "Служител")]
        public Employees Employee { get; set; }
    }
}
