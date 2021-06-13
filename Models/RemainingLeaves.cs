using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class RemainingLeaves
    {
        public int Id { get; set; }

        [Display(Name = "Оставащи дни по основание")]
        public int RemainingDays { get; set; }

        public int EmployeesId { get; set; }

        [Display(Name = "Служител")]
        public Employees Employee { get; set; }

        public int LeaveReasonsId { get; set; }

        [Display(Name = "Основание за отпуск")]
        public LeaveReasons LeaveReason { get; set; }
    }
}
