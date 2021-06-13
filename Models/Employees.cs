using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Models
{
    public class Employees
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Презиме")]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string Family { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рождена дата")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        public int OrganisationsId { get; set; }

        [Display(Name = "Организационна единица")]
        public Organisations Oragnisation { get; set; }

        public int PositionsId { get; set; }

        [Display(Name = "Позиция")]
        public Positions Position { get; set; }

        public Salary Salary { get; set; }

        public SystemUsers SystemUser { get; set; }

        public Address Address { get; set; }

        public ICollection<RemainingLeaves> RemainingLeaves { get; set; }

        public ICollection<Leaves> Leaves { get; set; }

        [Display(Name = "Пълно име")]
        public string FullName
        {
            get
            {
                return Name + " " + Surname + " " + Family;
            }
        }
    }
}
