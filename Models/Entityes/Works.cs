using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Works
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime start_date { get; set; }

        [Required]
        [Display(Name = "End date")]
        public DateTime end_date { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [ForeignKey("Members")]
        public int member_id { get; set; }

        [Required]
        [ForeignKey("Companies")]
        public int facility_id { get; set; }
    }
}
