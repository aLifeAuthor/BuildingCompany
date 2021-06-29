using BuildingCompany.Models.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models.HelperEntityes
{
    public class WorksV2
    {
        public Works work { get; set; }
        public Facility facility { get; set; }
        public Members worker { get; set; }
        public int worker_id { get; set; }
    }
}
