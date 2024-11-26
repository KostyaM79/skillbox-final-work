using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;

namespace WebApiService.Services
{
    public interface IApiSocialsService 
    {
        SocialModel[] GetAll();

        void Update(string[] links, IFormFileCollection files);
    }
}
