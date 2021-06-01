using Registry.BLL.DTO;
using Registry.BLL.Interfaces;
using Registry.DAL.Interfaces;
using Registry.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Registry.BLL.Services
{
    public class ServService : IService<ServiceDTO>
    {
        IUnitOfWork Database { get; set; }
        public ServService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public ServiceDTO Get(string id)
        {
            var serv = Database.Services.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Service, ServiceDTO>()).CreateMapper();
            ServiceDTO servDTO = mapper.Map<Service, ServiceDTO>(serv);
            return servDTO;
        }
        public List<ServiceDTO> GetAll()
        {
            List<Service> servs = Database.Services.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Service, ServiceDTO>()).CreateMapper();
            return mapper.Map<List<Service>, List<ServiceDTO>>(servs);
        }
        public void Create(ServiceDTO servDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ServiceDTO, Service>()).CreateMapper();
            Service serv = mapper.Map<ServiceDTO, Service>(servDTO);
            serv.Id = Guid.NewGuid().ToString();
            serv.Status = 1;
            Database.Services.Create(serv);
        }
        public void Update(ServiceDTO servDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ServiceDTO, Service>()).CreateMapper();
            Service serv = mapper.Map<ServiceDTO, Service>(servDTO);
            Database.Services.Update(serv);
        }
        public void Disable(string id)
        {
            Database.Services.Disable(id);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
