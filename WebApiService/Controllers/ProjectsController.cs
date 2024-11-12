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
using Models;

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
        [Route("Read/{id}")]
        public IActionResult Read(int id)
        {
            ProjectModel project = service.Get(id);
            if (project != null) return Ok(project);
            else return StatusCode(500, new { Message = "Не удалось получить проект из БД!" });
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
            IFormFileCollection files = HttpContext.Request.Form.Files;

            string title = HttpContext.Request.Form["ProjectTitle"];
            string descr = HttpContext.Request.Form["ProjectDescr"];
            string fileName = HttpContext.Request.Form.Files[0].FileName;
            string imgFileName = CreateFileName(fileName);

            int id = service.Add(title, descr, imgFileName);

            if (id > 0)
            {
                using Stream s = System.IO.File.Create($"img\\projects-images\\{imgFileName}");
                files[0].OpenReadStream().CopyTo(s);
                s.Close();
            }

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Edit))]
        public IActionResult Edit()
        {
            string fileName = null;
            Stream stream = null;

            IFormFileCollection files = HttpContext.Request.Form.Files;

            int id = int.Parse(HttpContext.Request.Form["Id"]);
            string title = HttpContext.Request.Form["ProjectTitle"];
            string descr = HttpContext.Request.Form["ProjectDescr"];

            if (files.Count > 0)
            {
                fileName = files[0].FileName;
                stream = files[0].OpenReadStream();
            }

            service.Edit(id, title, descr, stream, fileName);

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
