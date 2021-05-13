using Registry.BLL.DTO;
using Registry.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Registry.WEB.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Registry.BLL.Services;

namespace Registry.WEB.Controllers
{
    public class HomeController : Controller
    {
        IService<OrganizationDTO> orgService;
        public HomeController(IService<OrganizationDTO> serv)
        {
            orgService = serv;
        }
        public ActionResult Index()
        {
            return View("Organizations");
        }
        public ActionResult Read_Organizations([DataSourceRequest]DataSourceRequest request)
        {
            List<OrganizationDTO> orgDTOs = orgService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrganizationDTO, OrganizationViewModel>()).CreateMapper();
            List<OrganizationViewModel> orgViewModels = mapper.Map<List<OrganizationDTO>, List<OrganizationViewModel>>(orgDTOs);
            return Json(orgViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create_Organization([DataSourceRequest]DataSourceRequest request, OrganizationViewModel orgViewModel)
        {
            if (orgViewModel != null && ModelState.IsValid)
            {
                OrganizationDTO orgDTO = new OrganizationDTO
                {
                    Name = orgViewModel.Name,
                    BIN = orgViewModel.BIN,
                    PhoneNumber = orgViewModel.PhoneNumber
                };
                orgService.Create(orgDTO);
            }
            return Json(new[] { orgViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update_Organization([DataSourceRequest]DataSourceRequest request, OrganizationViewModel orgViewModel)
        {
            if (orgViewModel != null && ModelState.IsValid)
            {
                OrganizationDTO orgDTO = new OrganizationDTO
                {
                    Id = orgViewModel.Id,
                    Name = orgViewModel.Name,
                    BIN = orgViewModel.BIN,
                    PhoneNumber = orgViewModel.PhoneNumber,
                    BeginDate = orgViewModel.BeginDate,
                    EndDate = orgViewModel.EndDate
                };
                orgService.Update(orgDTO);
            }
            return Json(new[] { orgViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Disable_Organization([DataSourceRequest]DataSourceRequest request, OrganizationViewModel orgViewModel)
        {
            if (orgViewModel != null)
            {
                orgService.Disable(orgViewModel.Id);
            }
            return Json(new[] { orgViewModel }.ToDataSourceResult(request, ModelState));
        }

        // Index action below is for testing the data transition from DB to the Presentation layer

        /*public ActionResult Index()
        {
            List<OrganizationDTO> orgDTOs = orgService.GetOrganizations();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrganizationDTO, OrganizationViewModel>()).CreateMapper();
            List<OrganizationViewModel> orgViewModels = mapper.Map<List<OrganizationDTO>, List<OrganizationViewModel>>(orgDTOs);
            return View(orgViewModels);
        }*/
    }
}