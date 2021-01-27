using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HC.DataAccess.Data.Repository
{
    public class UnitRepository : Respository<Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public UnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetUnitListForDropDown()
        {
            return _db.Unit.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Unit unit)
        {
            var selectedCategory = _db.Unit.FirstOrDefault(s => s.Id == unit.Id);
            selectedCategory.Name = unit.Name;
            selectedCategory.Description = unit.Description;
           
            _db.SaveChanges();
        }
    }
    
}
