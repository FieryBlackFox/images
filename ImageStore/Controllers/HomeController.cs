using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ImageStore.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        StoreContext _context;
        IHostingEnvironment _appEnvironment;


        public HomeController(StoreContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public void UploadFiless([FromForm] IFormFileCollection files)
        {
            Console.WriteLine(files.Count);
            var uploadedFile = files[0];
            Response.ContentType = "text/html";
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
                Image file = new Image { Name = uploadedFile.FileName, Path = path, User = Request.Cookies["UserCookies"] };
                _context.Images.Add(file);
                _context.SaveChanges();
                Response.StatusCode = (int)HttpStatusCode.Created;
                Response.WriteAsync(file.Path);
            }
            else Response.StatusCode = (int)HttpStatusCode.NoContent;

            Console.WriteLine("Ура");

        }

        /*
        public IActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("UserCookies"))
            {
                Response.Cookies.Append("UserCookies", new Random().Next(0, 10000000).ToString(), new CookieOptions() { Expires = DateTime.MaxValue });
            }
            return View(_context.Images.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Image file = new Image { Name = uploadedFile.FileName, Path = path, User = Request.Cookies["UserCookies"] };
                _context.Images.Add(file);
                _context.SaveChanges();
                return View("About", file);
            }
            return RedirectToAction("Index");

        }

        public IActionResult Photos()
        {
            return View(_context.Images.Where(f => f.User == Request.Cookies["UserCookies"]).ToList());
        }
        
        public IActionResult GetFile(string name)
        {
            if(name!=null)
            {
                return PhysicalFile(Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/Files/" + name), "application/octet-stream", name);
            }
            return View("Index");
        }*/

    }
}
