using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Deals
    {
        [Key]
        [Display(Name = "Deal id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Required]
        [Display(Name = "Price")]
        public int price { get; set; }

        [Required]
        [ForeignKey("Members")]
        public int member_id { get; set; }

        [Required]
        [ForeignKey("Companies")]
        public int company_id { get; set; }
    }
}
