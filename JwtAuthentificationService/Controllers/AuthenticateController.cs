using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthenticationService.Models;
using JwtAuthenticationService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using NotebookDatabase.Models;

namespace JwtAuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IRegisterService registerService;
        private readonly ILoginService loginService;
        private readonly IConfiguration configuration;

        public AuthenticateController(IRegisterService registerService, ILoginService loginService, IConfiguration config)
        {
            this.registerService = registerService;
            this.loginService = loginService;
            configuration = config;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            string token = await registerService.Register(model);

            if (!string.IsNullOrEmpty(token))
            {
                HttpContext.Response.Headers.Add("Jwt", token);
                return Ok();
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Authorize("IsAdmin")]
        [HttpPost]
        [Route(nameof(RegisterAdmin))]
        public async Task<UserModel> RegisterAdmin([FromBody] RegisterModel model)
        {
            return await registerService.RegisterAdmin(model);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            string token = await loginService.Login(model);

            if (!string.IsNullOrEmpty(token))
            {
                HttpContext.Response.Headers.Add("Jwt", token);
                return Ok();
            }

            return Unauthorized();
        }

        [Authorize("IsAdmin")]
        [HttpGet]
        [Route(nameof(Users))]
        public UserModel[] Users()
        {
            return registerService.GetUsers();
        }

        [Authorize("IsAdmin")]
        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(string id)
        {
            registerService.Delete(id);
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(AuthorizeTest))]
        public void AuthorizeTest()
        {

        }
    }
}
