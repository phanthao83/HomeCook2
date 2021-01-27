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
    public class PricingController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        // private readonly UserManager<ApplicationUser> _userManager; 
        public PricingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //    _userManager = userManager; 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            

            // string userId = User.Identity.GetUserId();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId

            PricingView pricingVM = new PricingView(); 
            if (id == null)
            {
                return NotFound(); 
            }
            else
            {
                Product product = _unitOfWork.Product.Get(id.GetValueOrDefault());
                if (product == null)
                {
                    return NotFound();
                }
                pricingVM.PricingHistory = new PricingHistory()
                {
                    Id = 0, 
                    ProductId = product.Id,
                    OPrice = product.Price,
                    NPrice = product.Price,
                    UpdateDate = DateTime.Now, 
                    UserId = userId 
                    
                };
                pricingVM.ProductName = product.Name;

                //Get Histroy Prcing include the UserInfo
             //   pricingVM.PricingHistoryLst = _unitOfWork.PricingHistory.GetByProduct(product.Id); 


            }

            return View(pricingVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PricingView pricingVM)
        {
            if (ModelState.IsValid)
            {
                if (pricingVM.PricingHistory.Id == 0)
                {
                    Product product = _unitOfWork.Product.Get(pricingVM.PricingHistory.ProductId);
                    product.Price = pricingVM.PricingHistory.NPrice;
                    pricingVM.PricingHistory.UpdateDate = DateTime.Now; 
                    _unitOfWork.PricingHistory.Add(pricingVM.PricingHistory);
                   

                }
                else
                {
                    //_unitOfWork.PricingHistory.(pricing);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Upsert));
            }
            else
            {
                return View(pricingVM);
            }

        }

        #region WebAPI

        [HttpGet]
        public IActionResult GetByProduct(int id)
        {
            /*var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            if (id == 0)
            {
                return Json(null); 
            }
            else
            {
                JsonResult jsonResult = Json(new { data = _unitOfWork.PricingHistory.GetByProduct(id) });
                return jsonResult;
            }
            */
            JsonResult jsonResult = Json(new { data = _unitOfWork.PricingHistory.GetAll(filter: o => o.ProductId == id) });
            return jsonResult;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            
                JsonResult jsonResult = Json(new { data = _unitOfWork.PricingHistory.GetAll() });
                return jsonResult;
            

        }

        #endregion 
    }
}
