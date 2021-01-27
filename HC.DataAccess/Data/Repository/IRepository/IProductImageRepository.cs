using HC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void Update(ProductImage productImg);

        IEnumerable<ProductImage> GetByProduct(int productId);

        
    }
   

}
