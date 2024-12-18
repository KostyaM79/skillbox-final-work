﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Services;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Models;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IApiProjectService service;

        public ProjectsController(IApiProjectService service)
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
        [Authorize]
        [Route(nameof(Create))]
        public void Create()
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;

            string title = HttpContext.Request.Form["ProjectTitle"];
            string descr = HttpContext.Request.Form["ProjectDescr"];
            string fileName = files[0].FileName;

            service.Add(title, descr, files[0]);
        }

        [HttpPost]
        [Route(nameof(Edit))]
        [Authorize]
        public void Edit()
        {
            IFormFile file = null;

            IFormFileCollection files = HttpContext.Request.Form.Files;

            int id = int.Parse(HttpContext.Request.Form["Id"]);
            string title = HttpContext.Request.Form["ProjectTitle"];
            string descr = HttpContext.Request.Form["ProjectDescr"];

            if (files.Count > 0)
                file = files[0];

            service.Edit(id, title, descr, file);
        }

        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
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
