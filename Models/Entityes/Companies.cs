using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Companies
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string phone_number { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Customer FIO")]
        public string comp_name { get; set; }
    }
}
