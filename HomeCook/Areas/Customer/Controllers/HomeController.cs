using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HC.Models;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model.ViewModel;
using HC.Ultility;
using HomeCook.Areas.Extension;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HomeCook.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration; 

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork,  IWebHostEnvironment hostEnviroment, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnviroment;
            _configuration = configuration; 
        }

        public IActionResult Index()
        {
            //Get Top 
            var homeviewMD = new HomeView();
            homeviewMD.ProductImagePath = PathConfiguration.GetProductImgStoreFolder();
            homeviewMD.AvatarPath = PathConfiguration.GetAvatarStoreFolder(); 

            homeviewMD.BestProducts = _unitOfWork.SP.ReturnList<ProductSimpleView>(SP.SelectTop4Product);
            homeviewMD.CategoryList = _unitOfWork.Category.GetAll();
            homeviewMD.NewProducts = _unitOfWork.SP.ReturnList<ProductSimpleView>(SP.SelectTop4NewProduct);
            homeviewMD.BestSuppliers = _unitOfWork.SP.ReturnList<AppUserView>(SP.SelectTop10Seller);

            //Check pics if not existed , replaced by default pic 
           foreach (ProductSimpleView p in homeviewMD.BestProducts) {
                SetDefaultValueForProductAvatar(p); 

            }
            foreach (ProductSimpleView p in homeviewMD.NewProducts)
            {
                SetDefaultValueForProductAvatar(p); 
            }

            foreach (AppUserView s in homeviewMD.BestSuppliers) 
            {
                var filePath = PathConfiguration.GetAvatarStoreFolder(_hostEnvironment) + "\\" + s.AvartarUrl;
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)//check file exsit or not
                {
                    //p.FileName = 
                    s.AvartarUrl = PathConfiguration.GetDefaultSupplierImg(_configuration);

                }

            }
            return View(homeviewMD);
        }

        private void SetDefaultValueForProductAvatar(ProductSimpleView p)
        {
            if (p.FileName == null || (p.FileName != null && p.FileName.Length == 0 )) p.FileName = PathConfiguration.GetDefaultProductImg(p.CategoryName, _configuration);
            else
            {
                var filePath = PathConfiguration.GetProductImgStoreFolder(_hostEnvironment) + "\\" + p.FileName;
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)//check file exsit or not
                {
                    p.FileName = PathConfiguration.GetDefaultProductImg(p.CategoryName, _configuration);

                    //p.FileName = "HomeCook.jpg"; 

                }

            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
