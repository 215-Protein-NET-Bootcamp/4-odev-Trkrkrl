using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PersonDto:IDto
    {
        [Required]
        [MaxLength(25)]
        [Display(Name = "StaffId")]
        public string PersonId { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(500)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Phone]
        [MaxLength(25)]
        public string Phone { get; set; }

        [Required]
        
        [DataType(DataType.Date)]
        [Display(Name = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }


        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
