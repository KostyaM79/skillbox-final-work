using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthService authenticationService;

        public AuthenticateController(IAuthService service)
        {
            authenticationService = service;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login(LoginModel model)
        {
            string token = authenticationService.Authenticate(model);
            HttpContext.Response.Cookies.Append("jwt", token);
            return Redirect("/Admin/AdminDesktop");
            //return Ok();
        }
    }
}
