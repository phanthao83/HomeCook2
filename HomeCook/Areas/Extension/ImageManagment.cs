using HC.Model;
using HC.Ultility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCook.Areas.Extension
{
    public static class ImageManagment
    {

        public static void UploadAvatarTemporary(IFormFileCollection files,  IWebHostEnvironment hostEnvironment, string folderName )
        {
            if (files.Count > 0)
            {

                var uploadToFolder = PathConfiguration.GetAvatarUploadFolder(hostEnvironment, folderName);
                if (!Directory.Exists(uploadToFolder))
                {
                    Directory.CreateDirectory(uploadToFolder);
                }

                using (var fileStreams = new FileStream(Path.Combine(uploadToFolder, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

            }
        }

        /* Return filename of the avatar saved on server. 
         */
        public static string SaveAvatarPicToServer ( IWebHostEnvironment hostEnvironment, string folderName, string uploadedAvatarName)
        {

            string uploadDirectoryPath = PathConfiguration.GetAvatarUploadFolder(hostEnvironment, folderName);
            string uploadedFilePath = Path.Combine(uploadDirectoryPath, uploadedAvatarName);
            FileInfo uploadFile = new FileInfo(uploadedFilePath);
            if (uploadFile.Exists == false)
            {
                return string.Empty; 
            }


            string storageFolder = PathConfiguration.GetAvatarStoreFolder(hostEnvironment);
            string newFileName = String.Concat(Guid.NewGuid(), uploadFile.Extension);
            FileInfo file = new FileInfo(newFileName);
            while (file.Exists)
            {
                newFileName = String.Concat(Guid.NewGuid(), uploadFile.Extension);
                file = new FileInfo(newFileName);

            }
            string newFilePath = Path.Combine(storageFolder, newFileName);
            //Move file to storage and create product image 
            uploadFile.CopyTo(newFilePath);

            

            if (Directory.Exists(uploadDirectoryPath))
            {
                Directory.Delete(uploadDirectoryPath, true);
            }
            return newFileName;


        }


        public static void DeleteOldAvatar(IWebHostEnvironment hostEnvironment, string avatarFileName)
        {
            string storageFolder = PathConfiguration.GetAvatarStoreFolder(hostEnvironment);
            string avartarFilePath = Path.Combine(storageFolder, avatarFileName);
            FileInfo avatar = new FileInfo(avartarFilePath);
            if (avatar.Exists )
            {
                avatar.Delete();
            }


        }


    }
}
