using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.BLL.DTO
{
    public class OrganizationDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BIN { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
    }
}
