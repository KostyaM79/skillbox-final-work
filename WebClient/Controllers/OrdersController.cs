﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;
using Exceptions;
using WebClient.Models;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IClientOrderService orderService;

        public OrdersController(IClientOrderService service)
        {
            orderService = service;
        }

        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }


        public IActionResult ReadAll()
        {
            return View(orderService.GetAll());
        }

        /// <summary>
        /// Возвращает все заявки
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="startOffset"></param>
        /// <param name="endOffset"></param>
        /// <returns></returns>
        [Route("read/filter/{filterName}/{startOffset?}/{endOffset?}")]
        public IActionResult Read(string filterName, int startOffset = 0, int endOffset = 0)
        {
            return View("ReadAll", orderService.Get(filterName, startOffset, endOffset));
        }

        /// <summary>
        /// Возвращает заявку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("read/{id:int}")]
        public IActionResult Find(int id)
        {
            return View(orderService.Get(id));
        }

        /// <summary>
        /// Если данные валидны, отправляет их в сервис для добавления в базу данныхю
        /// Затем перенаправляет пользователя на начальную странице
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Order(OrderModel model)
        {
            if (ModelState.IsValid) orderService.Add(model);

            return RedirectToAction("Main", "Home");
        }

        [HttpGet]
        [Route("edit/{id}")]
        [Authorize]
        public IActionResult Update(int id)
        {
            return View(orderService.Get(id));
        }

        /// <summary>
        /// Обновляет заявку
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Update(UpdateOrderModel model)
        {
            try
            {
                string token = HttpContext.Request.Cookies["jwt"];
                orderService.Update(model, token);
                return View("ReadAll", orderService.GetAll());
            }
            catch (DatabaseServiceException ex)
            {
                return View("DbError", new ErrorViewModel() { ErrorMessage = $"При обращении к сервису базы данных произошла ошибка: {ex.Message}" });
            }
        }
    }
}
