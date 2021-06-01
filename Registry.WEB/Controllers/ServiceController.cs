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
    public class ServiceController : Controller
    {
        IService<ServiceDTO> service;
        public ServiceController(IService<ServiceDTO> serv)
        {
            service = serv;
        }
        public ActionResult Index()
        {
            return View("Services");
        }
        public ActionResult Read_Services([DataSourceRequest]DataSourceRequest request)
        {
            List<ServiceDTO> servDTOs = service.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ServiceDTO, ServiceViewModel>()).CreateMapper();
            List<ServiceViewModel> serviceViewModels = mapper.Map<List<ServiceDTO>, List<ServiceViewModel>>(servDTOs);
            return Json(serviceViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create_Services([DataSourceRequest]DataSourceRequest request, ServiceViewModel servViewModel)
        {
            if (servViewModel != null && ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ServiceViewModel, ServiceDTO>()).CreateMapper();
                ServiceDTO servDTO = mapper.Map<ServiceViewModel, ServiceDTO>(servViewModel);
                service.Create(servDTO);
            }
            return Json(new[] { servViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update_Services([DataSourceRequest]DataSourceRequest request, ServiceViewModel servViewModel)
        {
            if (servViewModel != null && ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ServiceViewModel, ServiceDTO>()).CreateMapper();
                ServiceDTO servDTO = mapper.Map<ServiceViewModel, ServiceDTO>(servViewModel);
                service.Update(servDTO);
            }
            return Json(new[] { servViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Disable_Services([DataSourceRequest]DataSourceRequest request, ServiceViewModel servViewModel)
        {
            if (servViewModel != null)
            {
                service.Disable(servViewModel.Id);
            }
            return Json(new[] { servViewModel }.ToDataSourceResult(request, ModelState));
        }
    }
}