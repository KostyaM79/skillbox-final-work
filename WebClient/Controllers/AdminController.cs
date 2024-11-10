using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService orderService;

        public AdminController(IOrderService service)
        {
            orderService = service;
        }

        [Authorize]
        public IActionResult AdminDesktop()
        {
            return View(orderService.GetAll());
        }

        [Authorize]
        [Route("AdminDesktop/{filterName}/{startOffset?}/{endOffset?}")]
        public IActionResult AdminDesktop(string filterName, int startOffset, int endOffset)
        {
            return View(orderService.Get(filterName, startOffset, endOffset));
        }

        [Authorize]
        public IActionResult AdminDesktopRange(DateTime d1, DateTime d2)
        {
            return View("AdminDesktop", orderService.Get("Range"));
        }

        [Authorize]
        public IActionResult AdminMain()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminProjects()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminServices()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminArticles()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminContacts()
        {
            return View();
        }
    }
}
