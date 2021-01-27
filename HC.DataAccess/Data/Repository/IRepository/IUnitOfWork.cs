using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRespository Category { get; }
        IProductImageRepository ProductImage { get; }
        IProductRepository Product { get; }
        IProductReviewRepository ProductReview { get; }
        
        IUnitRepository Unit { get; }

        IPricingHistoryRespository PricingHistory { get; }
        IStoreProcedure SP { get; }

        void Save(); 
    }
}
