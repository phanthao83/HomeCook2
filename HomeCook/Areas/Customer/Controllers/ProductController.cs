using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model.ViewModel;
using HC.Ultility;
using HomeCook.Areas.Extension;
using Microsoft.AspNetCore.Mvc;

namespace HomeCook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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



            return View();
        }

        public IActionResult ProductDetail(int? id )
        {
            //Get Top 
            ProductVM productVM;
            productVM = new ProductVM()

            {
                UnitList = _unitOfWork.Unit.GetUnitListForDropDown(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                PicFileNames = String.Empty,
                ErrorMessage = String.Empty,
                MaxUploadFileNumber = HCConstant.MAX_PRODUCT_UPLOAD_FILES,
            };

            
            if (id == null)
            {
                return NotFound(); 

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
                        
            return View(productVM);
        }


        public IActionResult TopProduct(int id)
        {

            // @Html.DropDownListFor ("categoryList", @Model.CategoryList, "All", new { onchange = "changeCategory();" })
            var productView = new ListProductView();
            // homeviewMD.SelectedCatgory = selectedCategoryId == null ? 0 : selectedCategoryId.GetValueOrDefault();
            productView.SelectedCatgory = id;
            productView.ProductImagePath = PathConfiguration.GetProductImgStoreFolder();

            if (id > 0)
            {
                var category = _unitOfWork.Category.Get(id);
                productView.SelectedCategoryName = (category is null ) ?  string.Empty  : category.Name;
            }
           

          

            return View(productView);
        }


        [HttpGet]
        public IActionResult GetActiveProducts(int selectedCategoryId)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", null);

            return Json(new { data = _unitOfWork.SP.ReturnList<ProductSimpleView>(SP.SelectActiveProduct, parameters) });

        }
    }
}