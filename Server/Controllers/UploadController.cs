using KatMemeABlazing.Server.Models;
using KatMemeABlazing.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadSample.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }


        [HttpPost]
        public async Task Post()
        {
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    foreach (var item in HttpContext.Request.Form.Files)
                    {
                        var path = Path.Combine(environment.ContentRootPath, @"wwwroot\Upload", item.FileName);
                        //var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\" + item.FileName + @"\");
                        //path = path.Replace("\\Server\\", "\\Client\\");
                        using (var stream = new FileStream(path.Replace("\\Server\\", "\\Client\\"), FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                            
                    }
                }
            }
        }

        /*[HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> SaveProfilePicture([FromBody] KatPostP katPostP)
        {
            foreach (var file in katPostP.Files)
            {
                string fileExtenstion = file.FileType.ToLower().Contains("png") ? "png" : "jpg";
                string fileName = Path.Combine(environment.ContentRootPath, "Upload\"Profile", "weqeqwe." + fileExtenstion);
                using (var fileStream = System.IO.File.Create(fileName))
                {
                    await fileStream.WriteAsync(file.Data);
                }
            }
            return Ok();
        }*/
    }
}