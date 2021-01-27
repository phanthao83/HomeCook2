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

namespace HomeCook.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        // private readonly UserManager<ApplicationUser> _userManager; 
        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnviroment;
            //    _userManager = userManager; 
        }
     

        [HttpPost]
        public ActionResult UploadAvatar()
        {

            //create session 
            if (HttpContext.Session.GetObject<string>(SessionType.UploadImage) == null)
            {
                HttpContext.Session.SetObject(SessionType.UploadImage, Guid.NewGuid().ToString());
            }
            string folderName = HttpContext.Session.GetObject<string>(SessionType.UploadImage);
            var files = HttpContext.Request.Form.Files;

            ImageManagment.UploadAvatarTemporary(files, _hostEnvironment, folderName); 

            return Json(true);
        }

    }
}