using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using HC.Model.ViewModel;
using HC.Ultility;
using HomeCook.Areas.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace HomeCook.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    [Authorize]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
       // private readonly UserManager<ApplicationUser> _userManager; 
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnviroment;
        //    _userManager = userManager; 
        }
        #region Generate View
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM;
            productVM = new ProductVM()

            {
                UnitList = _unitOfWork.Unit.GetUnitListForDropDown(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                PicFileNames = String.Empty,
                ErrorMessage = String.Empty,
                MaxUploadFileNumber = HCConstant.MAX_PRODUCT_UPLOAD_FILES,
            };

            // string userId = User.Identity.GetUserId();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId

            if (id == null)
            {
                productVM.Product = new Product();
                productVM.Product.Name = "Product Name 1 ";
                productVM.Product.Description = "Description 1";
                productVM.Product.Price = 10;
                productVM.Product.Status = ProductStatus.Pending;
                productVM.Product.UserId = userId;
                productVM.Product.CreateDate = DateTime.Today;
                productVM.Product.AvgRating = 0;

            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                productVM.ImagePath = PathConfiguration.GetProductImgStoreFolder();
                productVM.Images = _unitOfWork.ProductImage.GetByProduct(id.GetValueOrDefault());

            }

            //String SessionId 
            HttpContext.Session.SetObject(SessionType.UploadImage, Guid.NewGuid());

            return View(productVM);

        }

        #endregion

        #region SubmitDataFromView
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                SaveProductInfo(productVM);
                bool isSucces = SaveProductImages(productVM);
                if (!isSucces)
                {
                    return View(productVM);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else

            {
                productVM.ErrorMessage = "";
                productVM.UnitList = _unitOfWork.Unit.GetUnitListForDropDown();
                productVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                productVM.ImagePath = PathConfiguration.GetProductImgStoreFolder();
                productVM.Images = _unitOfWork.ProductImage.GetByProduct(productVM.Product.Id);

                return View(productVM);
            }

        }

        private bool SaveProductInfo(ProductVM productVM)
        {
            if (productVM.Product.Id == 0)
            {
                productVM.Product.CreateDate = DateTime.Now;
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                var updatedProduct = _unitOfWork.Product.Get(productVM.Product.Id);
                updatedProduct.Name = productVM.Product.Name;
                updatedProduct.Price = productVM.Product.Price;
                updatedProduct.CategoryId = productVM.Product.CategoryId;
                updatedProduct.UnitId = productVM.Product.UnitId;
                updatedProduct.Description = productVM.Product.Description;

                _unitOfWork.Product.Update(updatedProduct);

            }
            return true;

        }
        /*
         * One product has at max 3 pics & the first one must be the Default one. 
         */
        private bool SaveProductImages(ProductVM productVM)
        {
            int countProductImg = 0;
            IEnumerable<ProductImage> pImgs = _unitOfWork.ProductImage.GetByProduct(productVM.Product.Id);

            foreach (var imag in pImgs)
            {
                countProductImg++;

            }

            String[] deletedImgIds = null;
            String[] picNames = null;
            if (productVM.UploadedImgIdBeRemoved != null && productVM.Product.Id > 0 && productVM.UploadedImgIdBeRemoved.Trim().Length > 0)
            {
                deletedImgIds = productVM.UploadedImgIdBeRemoved.Split(";");
                countProductImg = countProductImg - deletedImgIds.Length;
            }
            if (productVM.PicFileNames != null && productVM.PicFileNames.Trim().Length > 0)
            {
                picNames = productVM.PicFileNames.Split(";");
                countProductImg = countProductImg + picNames.Length;
            }
            if (countProductImg > HCConstant.MAX_PRODUCT_UPLOAD_FILES)
            {
                productVM.UnitList = _unitOfWork.Unit.GetUnitListForDropDown();
                productVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                productVM.ErrorMessage = "Only 3 images are uploaded for each product.";

                productVM.ImagePath = PathConfiguration.GetProductImgStoreFolder();
                productVM.Images = pImgs;
                return false;
            }
            else
            {
                if (deletedImgIds != null) DeleteProductPics(productVM.Product.Id, deletedImgIds);
                if (picNames != null) CreateProductImages(picNames, productVM.Product);

                //Set IsDefault for the first one pic
                IEnumerable<ProductImage> productImgs = _unitOfWork.ProductImage.GetByProduct(productVM.Product.Id);
                Boolean defaultImg = true;
                foreach (ProductImage pImg in productImgs)
                {
                    pImg.IsDefault = defaultImg;
                    defaultImg = false;
                }
            }
            return true;

        }

        private void DeleteProductPics(int productId, string[] imgProductIds)
        {
            foreach (string imgId in imgProductIds)
            {
                if (Int32.TryParse(imgId, out int id) == true)
                {

                    ProductImage img = _unitOfWork.ProductImage.Get(id);
                    if (img != null)
                    {
                        var deletedFilePath = Path.Combine(PathConfiguration.GetProductImgStoreFolder(_hostEnvironment), img.FileName);
                        FileInfo file = new FileInfo(deletedFilePath);
                        if (file.Exists)//check file exsit or not
                        {
                            file.Delete();

                        }
                        _unitOfWork.ProductImage.Remove(img);
                    }
                }
            }


        }

        private void CreateProductImages(string[] picNames, Product product)
        {
            //Move File from upload folder to store folder 
            // Create Product Image
            string folderName = HttpContext.Session.GetObject<string>(SessionType.UploadImage);
            string uploadDirectoryPath = PathConfiguration.GetProductImgUploadFolder(_hostEnvironment, folderName);
            foreach (string picName in picNames)
            {

                string uploadedFilePath = Path.Combine(uploadDirectoryPath, picName);
                FileInfo uploadFile = new FileInfo(uploadedFilePath);
                if (uploadFile.Exists)
                {
                    string storageFolder = PathConfiguration.GetProductImgStoreFolder(_hostEnvironment);
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

                    ProductImage newImg = new ProductImage();
                    newImg.FileName = newFileName;
                    if (product.Id > 0) newImg.ProductId = product.Id;
                    else newImg.Product = product;

                    _unitOfWork.ProductImage.Add(newImg);

                }

            }
            if (Directory.Exists(uploadDirectoryPath))
            {
                Directory.Delete(uploadDirectoryPath, true);
            }



        }
        #endregion
       
        #region API CALL
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            if (id == 0) return Json(new { data = _unitOfWork.Product.GetAll(includedProperties: "Unit,Category") });
            else return Json(new { data = _unitOfWork.Product.GetAll(filter: o => o.Id == id, includedProperties: "Unit,Category") });

        }

        [HttpGet]
        public IActionResult GetAll(string productStatus)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            if (User.IsInRole(UserType.AdminRole))
            {
                if (productStatus == null) return Json(new { data = _unitOfWork.Product.GetAll(includedProperties: "Unit,Category") });
                else
                    return Json(new { data = _unitOfWork.Product.GetAll(filter: o=> o.Status == productStatus, includedProperties: "Unit,Category") });
            }
            else
            {
                if (User.IsInRole(UserType.SupplierRole))
                {
                    if (productStatus == null)  return Json(new { data = _unitOfWork.Product.GetAll(filter: o => o.UserId == userId, includedProperties: "Unit,Category") });
                    else  return Json(new { data = _unitOfWork.Product.GetAll(filter: o => o.UserId == userId && o.Status == productStatus, includedProperties: "Unit,Category") });
                }
                else
                {
                   return Json(new { data = _unitOfWork.Product.GetAll(filter: o => o.Status ==  ProductStatus.Active , includedProperties: "Unit,Category") });
                }
            }
           
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var deletedObjFrmDB = _unitOfWork.Product.Get(id);
            if (deletedObjFrmDB == null)
            {
                return Json(new { success = false, message = "Unable to find this product" });
            }
            else
            {
                try
                {
                    deletedObjFrmDB.Status = ProductStatus.Deleted; 
                    _unitOfWork.Product.Update(deletedObjFrmDB);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Delete Successfully" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }


        [HttpPost]
        public IActionResult Activate(int id)
        {
            var deletedObjFrmDB = _unitOfWork.Product.Get(id);
            if (deletedObjFrmDB == null)
            {
                return Json(new { success = false, message = "Unable to find this product" });
            }
            else
            {
                try
                {
                    deletedObjFrmDB.Status = ProductStatus.Active;
                    _unitOfWork.Product.Update(deletedObjFrmDB);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Activate Successfully" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }


        [HttpPost]
        public ActionResult UploadFile()
        {

            string folderName = HttpContext.Session.GetObject<string>(SessionType.UploadImage);
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {

                var uploadToFolder = PathConfiguration.GetProductImgUploadFolder(_hostEnvironment, folderName);
                if (!Directory.Exists(uploadToFolder))
                {
                    Directory.CreateDirectory(uploadToFolder);
                }
                using (var fileStreams = new FileStream(Path.Combine(uploadToFolder, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }
            }
            return Json(true);
        }

        #endregion
    }
}