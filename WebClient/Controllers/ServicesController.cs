using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServicesService service;

        public ServicesController(IServicesService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Возарвщает все услуги
        /// </summary>
        /// <returns></returns>
        public IActionResult ReadAll()
        {
            return View(service.GetAll());
        }

        /// <summary>
        /// Возвращает услугу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Read(int id)
        {
            return View(service.Get(id));
        }

        /// <summary>
        /// Возвращает форму для добавления услуги
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Добавляет новую услугу
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Create(ServiceModel model)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            service.Add(model, token);
            return Redirect("/Services/ReadAll");
        }

        /// <summary>
        /// Обновляет услугу
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Update(ServiceModel model)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            service.Update(model, token);
            return Redirect("/Services/ReadAll");
        }

        /// <summary>
        /// Удаляет услугу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Delete(int id)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            service.Delete(id, token);
            return Redirect("/Services/ReadAll");
        }
    }
}
