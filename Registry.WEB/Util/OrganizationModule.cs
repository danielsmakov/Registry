using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Registry.BLL.Interfaces;
using Registry.BLL.Services;

namespace Registry.WEB.Util
{
    public class OrganizationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrganizationService>().To<OrganizationService>();
        }
    }
}