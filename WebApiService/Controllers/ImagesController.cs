using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpGet]
        [Route("dir/{dir}/file/{file}")]
        public IActionResult Read(string dir, string file)
        {
            ImageManager im = new ImageManager();
            return File(im.GetImage(dir, file), im.ContentType(file));
        }
    }
}
