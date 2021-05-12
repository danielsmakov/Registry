﻿using AutoMapper;
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
    public class OrganizationService : IOrganizationService
    {
        IUnitOfWork Database { get; set; }
        public OrganizationService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public OrganizationDTO GetOrganization(string id)
        {
            var org = Database.Organizations.Get(id);
            if (org == null)
            {
                throw new ValidationException("Organization is not found", "");
            }
            OrganizationDTO orgDTO = new OrganizationDTO
            {
                Id = org.Id,
                Name = org.Name,
                BIN = org.BIN,
                PhoneNumber = org.PhoneNumber,
                Status = org.Status,
                BeginDate = org.BeginDate,
                EndDate = org.EndDate
            };
            return orgDTO;
        }
        public List<OrganizationDTO> GetOrganizations()
        {
            List<Organization> orgs = Database.Organizations.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Organization, OrganizationDTO>()).CreateMapper();
            return mapper.Map<List<Organization>, List<OrganizationDTO>>(orgs);
        }
        public void RegisterOrganization(OrganizationDTO orgDTO)
        {
            if (orgDTO == null)
            {
                throw new ValidationException("Passed organization equals null", "");
            }
            Organization org = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = orgDTO.Name,
                BIN = orgDTO.BIN,
                PhoneNumber = orgDTO.PhoneNumber,
                Status = 1,
                BeginDate = DateTime.Now.ToString()
            };
            Database.Organizations.Create(org);
        }
        public void UpdateOrganization(OrganizationDTO orgDTO)
        {
            if (orgDTO == null)
            {
                throw new ValidationException("Passed organization equals null", "");
            }
            Organization org = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = orgDTO.Name,
                BIN = orgDTO.BIN,
                PhoneNumber = orgDTO.PhoneNumber
            };
            Database.Organizations.Create(org);
        }
        public void Disable(string id)
        {
            if (Database.Organizations.Get(id) == null)
                throw new ValidationException($"Organization with Id {id} is not found", "");
            Database.Organizations.Disable(id);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}