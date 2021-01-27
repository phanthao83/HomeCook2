using HC.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
       void Update(Product product);
    }
    
}
