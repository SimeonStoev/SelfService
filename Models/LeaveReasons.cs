using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class LeaveReasons
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Основание")]
        public string Name { get; set; }

        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "Дни")]
        public int Days { get; set; }

        public ICollection<RemainingLeaves> RemainingLeaves { get; set; }

        public ICollection<Leaves> Leaves { get; set; }
    }
}
