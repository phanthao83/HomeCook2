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
    public class ProductRepository : Respository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       

        public void Update(Product product)
        {
            var selectedItem = _db.Product.FirstOrDefault(s => s.Id == product.Id);
            selectedItem.Name = product.Name;
            selectedItem.Description = product.Description;
            selectedItem.UnitId = product.UnitId;
            selectedItem.Price = product.Price; 


          //  _db.SaveChanges();
        }
    }
   
}
