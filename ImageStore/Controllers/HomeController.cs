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

namespace ImageStore.Controllers
{
    public class HomeController : Controller
    {
        StoreContext _context;
        IHostingEnvironment _appEnvironment;


        public HomeController(StoreContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            ViewBag.allPath = _appEnvironment.WebRootPath;
        }

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
        }

    }
}
