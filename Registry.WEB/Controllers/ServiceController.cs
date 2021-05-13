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
        // GET: Service
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
                ServiceDTO servDTO = new ServiceDTO
                {
                    Name = servViewModel.Name,
                    Code = servViewModel.Code,
                    Price = servViewModel.Price,
                    BeginDate = servViewModel.BeginDate
                };
                service.Create(servDTO);
            }
            return Json(new[] { servViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update_Services([DataSourceRequest]DataSourceRequest request, ServiceViewModel servViewModel)
        {
            if (servViewModel != null && ModelState.IsValid)
            {
                ServiceDTO servDTO = new ServiceDTO
                {
                    Id = servViewModel.Id,
                    Name = servViewModel.Name,
                    Code = servViewModel.Code,
                    Price = servViewModel.Price,
                    BeginDate = servViewModel.BeginDate,
                    EndDate = servViewModel.EndDate
                };
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