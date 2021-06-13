using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class Salary
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Net salary")]
        public decimal NetSalary { get; set; }

        [Display(Name = "Bonus salary")]
        public decimal? BonusSalary { get; set; }

        public int EmployeesId { get; set; }
        public Employees Employee { get; set; }

        [Display(Name = "Full salary")]
        public decimal FullNetSalary 
        {
            get
            {
                return (decimal)(NetSalary + BonusSalary);
            }
        }

    }
}
