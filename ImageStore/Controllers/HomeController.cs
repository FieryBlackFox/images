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
      try
      {
        if ((files!=null)&&(files.Count() != 0))
        {
          var uploadedFile = files[0];
          string extension = Path.GetExtension(files[0].FileName);
          string name = "";

          do
          {
            name = new Random().Next(0, 10000000).ToString() + extension;
          } while (System.IO.File.Exists(_appEnvironment.ContentRootPath + "/Files/" + name));
          
          string path = "/Files/" + name;
          using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
          {
            uploadedFile.CopyTo(fileStream);
          }
          
          Image file = new Image { Name = name, Path = path, User = Request.Cookies["UserCookies"] };
          _context.Images.Add(file);
          _context.SaveChanges();
          Response.StatusCode = (int)HttpStatusCode.Created;
          Response.WriteAsync(file.Path);
        }
        else Response.StatusCode = (int)HttpStatusCode.NoContent;
      }catch(Exception e)
      {
        Response.StatusCode = 500;
        Response.WriteAsync(e.Message);
      }
    }

    [HttpGet("/api/home/photos")]
    public ActionResult Photos()
    {
      return Json(_context.Images.Where(f => f.User == Request.Cookies["UserCookies"]).ToList());
    }

    [HttpGet("/api/home")]
    public void UseCookes()
    {
      if (!Request.Cookies.ContainsKey("UserCookies"))
      {
        Response.ContentType = "text/html";
        Response.Cookies.Append("UserCookies", new Random().Next(0, 10000000).ToString(), new CookieOptions() { Expires = DateTime.MaxValue });
      }
    }
  }
}
