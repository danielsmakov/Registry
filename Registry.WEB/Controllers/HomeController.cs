using Registry.BLL.DTO;
using Registry.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Registry.WEB.Models;

namespace Registry.WEB.Controllers
{
    public class HomeController : Controller
    {
        IOrganizationService orgService;
        public HomeController() { }
        public HomeController(IOrganizationService serv)
        {
            orgService = serv;
        }
        public ActionResult Index()
        {
            List<OrganizationDTO> orgDTOs = orgService.GetOrganizations();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrganizationDTO, OrganizationViewModel>()).CreateMapper();
            List<OrganizationViewModel> orgViewModels = mapper.Map<List<OrganizationDTO>, List<OrganizationViewModel>>(orgDTOs);
            return View(orgViewModels);
        }
    }
}