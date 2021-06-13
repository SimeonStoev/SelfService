using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class Leaves
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Начална дата")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Крайна дата")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Статус")]
        public int Status { get; set; } = 1;

        public int EmployeesId { get; set; }

        [Display(Name = "Служител")]
        public Employees Emplpoyee { get; set; }

        public int LeaveReasonsId { get; set; }

        [Display(Name = "Основание за отпуск")]
        public LeaveReasons LeaveReasons { get; set; }

        [Display(Name = "Дни")]
        public int Days
        {
            get
            {
                return (int)EndDate.Subtract(StartDate).TotalDays;
            }
        }
    }
}
