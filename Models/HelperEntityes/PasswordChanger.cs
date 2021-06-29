using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.Entityes
{
    public class PasswordChanger
    {
        public int memberId { get; set; }
        public string Password { get; set; }
        public string oldPassword { get; set; }
        public string newPassword1 { get; set; }
        public string newPassword2 { get; set; }
    }
}
