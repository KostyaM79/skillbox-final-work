using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;

namespace WebClient.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthService authenticationService;

        public AuthenticateController(IAuthService service)
        {
            authenticationService = service;
        }

        /// <summary>
        /// Возаращает форму входа
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Отправляет данные пользователя на сервер аутентификации
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT токен</returns>
        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login(LoginModel model)
        {
            string token = authenticationService.Authenticate(model);
            if (!string.IsNullOrEmpty(token))
            {
                HttpContext.Response.Cookies.Append("jwt", token);
                return RedirectToAction("Desktop", "Home");
            }

            return View();
        }
    }
}
