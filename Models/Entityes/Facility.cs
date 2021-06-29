using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Facility
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Date of Start")]
        public DateTime date_of_start { get; set; }

        [Required]
        [Display(Name = "Time")]
        public int time { get; set; }

        [Required]
        [Display(Name = "Number of floors")]
        public int number_of_floors { get; set; }

        [Required]
        [Display(Name = "Area")]
        public double area { get; set; }

        [Required]
        [Display(Name = "Adress")]
        public string adress { get; set; }

        [Required]
        [ForeignKey("Deals")]
        public int deal_id { get; set; }
    }
}
