using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models
{
    public class DB_User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public DB_User() { }
        public DB_User(string[] vs)
        {
            this.Login = vs[0];
            this.Password = vs[1];
        }
    }
}
