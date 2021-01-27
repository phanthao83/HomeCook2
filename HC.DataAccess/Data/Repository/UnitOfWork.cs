using HC.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRespository Category { get; private set; }
         public IProductImageRepository ProductImage { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductReviewRepository ProductReview { get; private set; }
        public  IUnitRepository Unit { get; private set; }

        public IPricingHistoryRespository PricingHistory { get; private set; }

        public IStoreProcedure SP { get; private set; }

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRespository(_db);
            Product = new ProductRepository(_db);
            ProductImage = new ProductImageRepository(_db);
            ProductReview = new ProductReviewRepository(_db);
            Unit = new UnitRepository(_db);
            PricingHistory = new PricingHistoryRespository(_db); 
            SP = new StoreProcedureRepository(_db); 
        
        }
        public void Dispose()
        {
            _db.Dispose(); 
        }

        public void Save()
        {
            _db.SaveChanges(); 
        }
    }
}
