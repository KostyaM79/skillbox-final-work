using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient
{
    /// <summary>
    /// Берёт JWT из Cookie и кладёт в Authorization
    /// </summary>
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string s = context.Request.Cookies["jwt"];
            context.Request.Headers.Add("Authorization", $"Bearer {s}");
            await next(context);
        }
    }
}
