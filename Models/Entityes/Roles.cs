using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Roles
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string name { get; set; }
    }
}
