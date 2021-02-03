/* Author : Thi Xuan Thao, Phan
 *Linkedin  : https://www.linkedin.com/in/phan-thao-bb782bb5/
 */
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HC.DataAccess.Data.Repository
{
    public class CategoryRespository : Respository<Category>, ICategoryRespository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRespository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }
        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Category.Select(i => new SelectListItem()
                                            {
                                                Text = i.Name,
                                                Value = i.Id.ToString()
                                            }); 
        }

        public void Update(Category category)
        {
            var selectedCategory = _db.Category.FirstOrDefault(s => s.Id == category.Id);
            selectedCategory.Name = category.Name;
            selectedCategory.Description = category.Description;
            selectedCategory.DisplayOrder = category.DisplayOrder;

        //    _db.SaveChanges(); 
        }
    }
}
