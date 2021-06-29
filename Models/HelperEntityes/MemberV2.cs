using BuildingCompany.Models.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.HelperEntityes
{
    public class MemberV2
    {
        public Members member { get; set; }
        public Members foreman { get; set; }
        public PasswordChanger passwordChanger { get; set; }
        public string Error { get; set; }
        public int WorkAmount { get; set; }
    }

}
