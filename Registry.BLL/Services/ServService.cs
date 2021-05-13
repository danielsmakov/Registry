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
    class ServService : IService<ServiceDTO>
    {
        IUnitOfWork Database { get; set; }
        public ServService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public ServiceDTO Get(string id)
        {
            var serv = Database.Services.Get(id);
            if (serv == null)
            {
                throw new Exception($"Service with Id {id} is not found");
            }
            ServiceDTO servDTO = new ServiceDTO
            {
                Id = serv.Id,
                Name = serv.Name,
                Code = serv.Code,
                Price = serv.Price,
                Status = serv.Status,
                BeginDate = serv.BeginDate,
                EndDate = serv.EndDate
            };
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
            if (servDTO == null)
            {
                throw new Exception("Passed service equals null");
            }
            Service serv = new Service
            {
                Id = Guid.NewGuid().ToString(),
                Name = servDTO.Name,
                Code = servDTO.Code,
                Price = servDTO.Price,
                Status = 1,
                BeginDate = servDTO.BeginDate
            };
            Database.Services.Create(serv);
        }
        public void Update(ServiceDTO servDTO)
        {
            if (servDTO == null)
            {
                throw new Exception("Passed organization equals null");
            }

            Service serv = new Service
            {
                Id = servDTO.Id,
                Name = servDTO.Name,
                Code = servDTO.Code,
                Price = servDTO.Price,
                Status = servDTO.Status,
                BeginDate = servDTO.BeginDate,
            };
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
