using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Entities
{
    public class Service
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }
        public int Status { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
    }
}
