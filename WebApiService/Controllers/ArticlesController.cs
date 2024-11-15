﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Models;
using WebApiService.Services;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService service;

        public ArticlesController(IArticleService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            ArticleModel[] models = service.GetAll();
            if (models != null) return Ok(models);
            else return StatusCode(500, new { Message = "Не удалось получить список статей из БД!" });
        }
    }
}
