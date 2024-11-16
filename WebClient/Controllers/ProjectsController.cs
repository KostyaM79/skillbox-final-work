using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;
using Microsoft.Extensions.Configuration;
using Services;

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
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Добавляет новый проект
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProjectModel model)
        {
            projectService.Add(
                model,
                HttpContext.Request.Form.Files[0].ContentType,
                HttpContext.Request.Form.Files[0].OpenReadStream(),
                HttpContext.Request.Form.Files[0].FileName);

            return Redirect("ReadAll");
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
        public IActionResult Update(ProjectModel model)
        {
            if (projectService.Edit(
                model,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].ContentType : null,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].OpenReadStream() : null,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].FileName : null))
                return Redirect("ReadAll");

            else return Problem();
        }

        public IActionResult Delete(int id)
        {
            projectService.Delete(id);
            return Redirect("/Project/ReadAll");
        }
    }
}
