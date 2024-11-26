using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IClientSocialsService
    {
        SocialModel[] GetAll();

        void Update(string[] links, IFormFileCollection files, string token);
    }
}
