using Registry.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.BLL.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationDTO GetOrganization(string id);
        List<OrganizationDTO> GetOrganizations();
        void RegisterOrganization(OrganizationDTO orgDTO);
        void UpdateOrganization(OrganizationDTO orgDTO);
        void Disable(string id);
        void Dispose();
    }
}
