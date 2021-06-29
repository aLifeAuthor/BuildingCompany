using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class Members
    {
        [Key]
        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "foreman_id")]
        public int foreman_id { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string surname { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string name { get; set; }

        [Display(Name = "Отчество")]
        public string patronymic { get; set; }

        [Required]
        [Display(Name = "Зарплата")]
        public int salary { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        public string phone_number { get; set; }

        [Required]
        [Display(Name = "Specialisation")]
        public string specialisation { get; set; }

        [Required]
        [Display(Name = "Емейл")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string password_hash { get; set; }

        [Required]
        [Display(Name = "Дата трудоустройства")]
        public DateTime date_of_employment { get; set; }

        [Required]
        [ForeignKey("Roles")]
        public int role_id { get; set; }
    }
}
