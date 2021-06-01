using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.BLL.DTO
{
    public class ServiceDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
    }
}
