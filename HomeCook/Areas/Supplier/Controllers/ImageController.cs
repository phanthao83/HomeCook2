using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Ultility;
using HomeCook.Areas.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnviroment; 
        }
        public IActionResult Index()
        {
            return View();
        }

      
        [HttpDelete]
        public ActionResult DeleteFile(string id )
        {
            //  string deletedFileName = deletedFile.
          //  var files = HttpContext.Request.Form.Files;
      //      if (files.Count > 0)
     //       {
       //        var deletedFileName = files[0].FileName; 
    //  //      }
            return Json(true);
        }
    }
}