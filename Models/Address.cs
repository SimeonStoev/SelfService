using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class Address
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Държава")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Оснвовен адрес")]
        public string LocalAddress { get; set; }

        [Display(Name = "Друг адрес")]
        public string OtherAddress { get; set; }

        public int EmployeesId { get; set; }

        [Display(Name = "Служител")]
        public Employees Employee { get; set; }

        [Display(Name = "Адрес")]
        public string fullAddress
        {
            get
            {
                return Country + ", " + City + ", " + LocalAddress;
            }
        }
    }
}
