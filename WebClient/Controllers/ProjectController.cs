using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;
using Microsoft.Extensions.Configuration;

namespace WebClient.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IConfiguration configuration;

        public ProjectController(IProjectService projectService, IConfiguration configuration)
        {
            this.projectService = projectService;
            this.configuration = configuration;
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public IActionResult ReadAll()
        {
            ProjectModel[] model = projectService.GetAll();

            foreach (ProjectModel m in model)
            {
                m.ModifyFileName(configuration["ApiLocation"]);
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ProjectModel project = projectService.Get(id);
            project.ModifyFileName(configuration["ApiLocation"]);
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(ProjectModel model)
        {
            if (projectService.Edit(
                model,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].ContentType : null,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].OpenReadStream() : null,
                HttpContext.Request.Form.Files.Any() ? HttpContext.Request.Form.Files[0].FileName : null))
                return Redirect("/Project/ReadAll");

            else return Problem();
        }

        public IActionResult Delete(int id)
        {
            projectService.Delete(id);
            return Redirect("/Project/ReadAll");
        }
    }
}
