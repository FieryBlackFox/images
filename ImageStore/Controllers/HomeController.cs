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
        }

        public IActionResult Index()
        {
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
                Image file = new Image { Name = uploadedFile.FileName, Path = path, User = HttpContext.Request.Cookies["UserCookies"] };
                _context.Images.Add(file);
                _context.SaveChanges();
                return View("About", file);
            }
            return RedirectToAction("Index");

        }

        public IActionResult Photos()
        {
            ViewData["User"] = HttpContext.Request.Cookies["UserCookies"];
            return View(_context.Images.ToList());
        }
        
    }
}
