using HC.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IUnitRepository : IRepository<Unit>
    {
        IEnumerable<SelectListItem> GetUnitListForDropDown();
        void Update(Unit category);
    }
}

