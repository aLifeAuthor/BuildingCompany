using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Equipments_works
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [ForeignKey("Works")]
        public int id_works { get; set; }

        [Required]
        [ForeignKey("Equipments")]
        public int id_equipments { get; set; }
    }
}
