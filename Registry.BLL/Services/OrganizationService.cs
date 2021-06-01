using AutoMapper;
using Registry.BLL.DTO;
using Registry.BLL.Infrastructure;
using Registry.BLL.Interfaces;
using Registry.DAL.Entities;
using Registry.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.BLL.Services
{
    public class OrganizationService : IService<OrganizationDTO>
    {
        IUnitOfWork Database { get; set; }
        public OrganizationService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public OrganizationDTO Get(string id)
        {
            var org = Database.Organizations.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Organization, OrganizationDTO>()).CreateMapper();
            OrganizationDTO orgDTO = mapper.Map<Organization, OrganizationDTO>(org);
            return orgDTO;
        }
        public List<OrganizationDTO> GetAll()
        {
            List<Organization> orgs = Database.Organizations.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Organization, OrganizationDTO>()).CreateMapper();
            return mapper.Map<List<Organization>, List<OrganizationDTO>>(orgs);
        }
        public void Create(OrganizationDTO orgDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrganizationDTO, Organization>()).CreateMapper();
            Organization org = mapper.Map<OrganizationDTO, Organization>(orgDTO);
            org.Id = Guid.NewGuid().ToString();
            org.Status = 1;
            org.BeginDate = DateTime.Now.ToString();
            Database.Organizations.Create(org);
        }
        public void Update(OrganizationDTO orgDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrganizationDTO, Organization>()).CreateMapper();
            Organization org = mapper.Map<OrganizationDTO, Organization>(orgDTO);
            Database.Organizations.Update(org);
        }
        public void Disable(string id)
        {
            Database.Organizations.Disable(id);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
