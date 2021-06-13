using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class Organisations
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Код")]
        public string Code { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
