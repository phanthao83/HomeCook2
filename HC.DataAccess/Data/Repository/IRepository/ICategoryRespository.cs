using HC.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRespository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();
        void Update(Category category); 
    }
}
