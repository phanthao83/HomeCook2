using HC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IProductReviewRepository : IRepository<ProductReview>
    {
        void Update(ProductReview review);
    }

   
}
