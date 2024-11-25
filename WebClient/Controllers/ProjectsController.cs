using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;
using Microsoft.Extensions.Configuration;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IClientProjectService projectService;
        private readonly IConfiguration configuration;

        public ProjectsController(IClientProjectService projectService, IConfiguration configuration)
        {
            this.projectService = projectService;
            this.configuration = configuration;
        }


        /// <summary>
        /// Возвращает форму для добавления нового проекта
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Create()
        {
            return View(new ProjectModel());
        }

        /// <summary>
        /// Добавляет новый проект
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Create(ProjectModel model)
        {
            string token = HttpContext.Request.Cookies["jwt"];

            projectService.Add(
                model,
                HttpContext.Request.Form.Files[0].ContentType,
                HttpContext.Request.Form.Files[0].OpenReadStream(),
                HttpContext.Request.Form.Files[0].FileName,
                token);

            return Redirect("/Projects/ReadAll");
        }

        /// <summary>
        /// Получает из базы данных все проекты
        /// </summary>
        /// <returns></returns>
        public IActionResult ReadAll()
        {
            ProjectModel[] model = projectService.GetAll();

            foreach (ProjectModel m in model)
            {
                m.ModifyFileName(configuration["ApiLocation"]);
            }

            return View(model);
        }


        public IActionResult Read(int id)
        {
            ProjectModel project = projectService.Get(id);
            project.ModifyFileName(configuration["ApiLocation"]);
            return View(project);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ProjectModel model)
        {
            string token = HttpContext.Request.Cookies["jwt"];

            if (projectService.Edit(
                model,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].ContentType : null,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].OpenReadStream() : null,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].FileName : null, token))
                return Redirect("/Projects/ReadAll");

            else return Problem();
        }

        [Route("Delete/{id:int}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            projectService.Delete(id, token);
            return Redirect("/Projects/ReadAll");
        }

        public IActionResult ViewProject(int id)
        {
            ProjectModel project = projectService.Get(id);
            project.ModifyFileName(configuration["ApiLocation"]);
            return View(project);
        }
    }
}
