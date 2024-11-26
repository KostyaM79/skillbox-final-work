using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Services;

namespace WebClient.Services
{
    /// <summary>
    /// Управляет авторизацией
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Authenticate(LoginModel model)
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration["AuthApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Authenticate/Login", JsonContent.Create(model)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string[] s = responseMessage.Headers.GetValues("jwt").ToArray();
                return s[0];
            }
            else return default;
        }
    }
}
