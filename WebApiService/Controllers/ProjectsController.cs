using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Models;
using WebApiService.Services;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectService service;

        public ProjectsController(IProjectService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            ProjectModel[] projects = service.GetAll();
            if (projects != null) return Ok(projects);
            else return StatusCode(500, new { Message = "Не удалось получить список проектов из БД!" });
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            string title = HttpContext.Request.Form["ProjectTitle"];
            string descr = HttpContext.Request.Form["ProjectDescr"];
            string fileName = HttpContext.Request.Form.Files[0].FileName;
            string imgFileName = CreateFileName(fileName);

            int id = service.Add(title, descr, imgFileName);

            if (id > 0)
            {
                using Stream s = System.IO.File.Create($"img\\projects-images\\{imgFileName}");
                HttpContext.Request.Form.Files[0].OpenReadStream().CopyTo(s);
            }


            return Ok();
        }

        private string CreateFileName(string srcFileName)
        {
            Regex regex = new Regex("(\\..+)$");
            string ext = regex.Match(srcFileName).Value;
            string str = $"{srcFileName}{DateTime.Now}";
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hash = MD5.Create().ComputeHash(bytes);
            string[] ss = hash.Select(e => $"{e:X2}").ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (string s in ss)
                sb.Append(s);
            sb.Append(ext);
            return sb.ToString();
        }
    }
}
