using Autofac;
using Autofac.Integration.Mvc;
using Registry.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Registry.DAL.Entities;
using Registry.DAL.Interfaces;
using Registry.BLL.Interfaces;
using Registry.BLL.DTO;
using Registry.BLL.Services;

namespace Registry.WEB.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            /*builder.RegisterType<OrganizationRepository>().As<IRepository<Organization>>();
            builder.RegisterType<ServiceRepository>().As<IRepository<Service>>();*/
            builder.RegisterType<OrganizationService>().As<IService<OrganizationDTO>>();
            builder.RegisterType<ServService>().As<IService<ServiceDTO>>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}