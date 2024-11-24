using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services;

namespace WebApiService.Services
{
    public interface IApiSocialsService : ISocialsService
    {
        void Create(string link, IFormFile file);
    }
}
