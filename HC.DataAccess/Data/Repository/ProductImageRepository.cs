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
    public class ProductImageRepository : Respository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

      

        public IEnumerable<ProductImage> GetByProduct(int productId)
        {

            return  _db.ProductImage.Where(s => s.ProductId == productId); 

        }

        public void Update(ProductImage productImg)
        {
            var selectedItem = _db.ProductImage.FirstOrDefault(s => s.Id == productImg.Id);
            selectedItem.FileName = productImg.FileName;
            selectedItem.ProductId = productImg.ProductId; 

            _db.SaveChanges();
        }

        
    }
   
}
