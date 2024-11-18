using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services;

namespace WebApiService.Services
{
    public interface IApiArticlesService : IArticleService
    {
        void Create(string title, string text, IFormFile file);
    }
}
