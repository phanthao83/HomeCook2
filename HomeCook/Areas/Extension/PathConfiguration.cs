using HC.Ultility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace HomeCook.Areas.Extension
{
    public static class PathConfiguration
    {
        /*
         * Return the directory path that Product Images upload from client to temparory folder
         */
        public static string GetProductImgUploadFolder(IWebHostEnvironment hostEnviroment, string folderName)
        {

            var webRootPath = hostEnviroment.WebRootPath;
            var uploadToFolder = Path.Combine(webRootPath, @"resource\\tmp", folderName);

            return uploadToFolder;

        }

        public static string GetProductImgStoreFolder(IWebHostEnvironment hostEnviroment = null )
        {
            var webRootPath =  @"\"; 
                if (hostEnviroment !=  null ) webRootPath =  hostEnviroment.WebRootPath ;
            return Path.Combine(webRootPath, @"resource\\product");
        }

        public static string GetAvatarUploadFolder(IWebHostEnvironment hostEnviroment, string folderName)
        {

            var webRootPath = hostEnviroment.WebRootPath;
            var uploadToFolder = Path.Combine(webRootPath, @"resource\\tmp", folderName);

            return uploadToFolder;

        }

        public static string GetAvatarStoreFolder(IWebHostEnvironment hostEnviroment = null)
        {
            var webRootPath = @"\";
            if (hostEnviroment != null) webRootPath = hostEnviroment.WebRootPath;
            return Path.Combine(webRootPath, @"resource\\avatar");
        }


        /*
          "DefaultPic": {
                        "Supplier": "avartar.jpg",
                        "Service": "service.jpg",
                        "MainDishes": "MainDishes.jpeg",
                        "Dessert": "Dessert.jpg",
                        "Product" :  "HomeCook.jpg"

                        }
         */

        public static string GetDefaultProductImg(string categoryName, IConfiguration configuration)
        {
            string fileName;
            if (categoryName.ToLower().Contains("main"))
            {
                fileName = configuration.GetSection("DefaultPic").GetSection("MainDishes").Value;
            }
            else if (categoryName.ToLower().Contains("dessert"))
            {
                fileName = configuration.GetSection("DefaultPic").GetSection("Dessert").Value;
            }
            else if (categoryName.ToLower().Contains("service"))
            {
                fileName = configuration.GetSection("DefaultPic").GetSection("Service").Value;
            }
            else
            {
                fileName = configuration.GetSection("DefaultPic").GetSection("Product").Value;
            }

            if (fileName.ToString() == string.Empty) fileName = "HomeCook.jpg"; 


            return fileName; 
        }



        public static string GetDefaultSupplierImg(IConfiguration configuration)
        {
            string fileName;
            fileName = configuration.GetSection("DefaultPic").GetSection("Supplier").Value;
            if (fileName.ToString() == string.Empty) fileName = "avartar.jpg";
            return fileName;
        }





    }
}
