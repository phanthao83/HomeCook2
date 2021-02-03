using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using HC.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HomeCook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Upsert(int? id)
        {
            UnitVM unitVM;
            unitVM = new UnitVM();
            unitVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(); 

            if (id == null)
            {
                 unitVM.Unit = new Unit();

            }
            else
            {
                unitVM.Unit = _unitOfWork.Unit.Get(id.GetValueOrDefault());
                if (unitVM.Unit == null)
                {
                    return NotFound();
                }
            }
            return View(unitVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UnitVM unitVM)
        {
            if (ModelState.IsValid)
            {
                if (unitVM.Unit.Id == 0)
                {
                    _unitOfWork.Unit.Add(unitVM.Unit);
                }
                else
                {
                    _unitOfWork.Unit.Update(unitVM.Unit);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));



            }
            else
            {
                unitVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                return View(unitVM);
            }

        }

        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Unit.GetAll(includedProperties:"Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deletedObjFrmDB = _unitOfWork.Unit.Get(id);
            if (deletedObjFrmDB == null)
            {
                return Json(new { success = false, message = "Unable to find this unit" });
            }
            else
            {
                try
                {
                    _unitOfWork.Unit.Remove(deletedObjFrmDB);
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