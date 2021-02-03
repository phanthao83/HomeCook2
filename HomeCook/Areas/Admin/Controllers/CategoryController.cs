/* Author : Thi Xuan Thao, Phan
 *Linkedin  : https://www.linkedin.com/in/phan-thao-bb782bb5/
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HomeCook.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category;
            if (id == null)
            {
                category = new Category();

            }
            else {
                category = _unitOfWork.Category.Get(id.GetValueOrDefault());
                if (category == null) {
                    return NotFound();
                }
            }
            ViewBag.Title = "ABC";
            ViewBag.Test = "Thao Phan"; 
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else {
                    _unitOfWork.Category.Update(category); 
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                return View(category); 
            }
        
        }

        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _unitOfWork.Category.GetAll(), opt =  new JsonSerializerOptions() }); 
            //Newtonsoft.Json.JsonSerializerSettings
          //  return Json(new {data = _unitOfWork.Category.GetAll(), JsonSerializerSettings = new JsonSerializerSettings() });
        //  JsonSerializerOptions
                //return Json(model, new JsonSerializerSettings
            //         {
            //           options.Formatting = Formatting.Indented,
            ///});
            
            
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deletedObjFrmDB = _unitOfWork.Category.Get(id);
            if (deletedObjFrmDB == null)
            {
                return Json( new {success = false, message = "Unable to find this catageory"}); 
            }
            else 
            {
                try
                {
                    _unitOfWork.Category.Remove(deletedObjFrmDB);
                    _unitOfWork.Save(); 
                    return Json(new { success = true, message = "Delete Successfully" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }

        #endregion

    }
}