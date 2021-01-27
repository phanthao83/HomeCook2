using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;

namespace HC.DataAccess.Data.Repository
{
    public class PricingHistoryRespository : Respository<PricingHistory>, IPricingHistoryRespository
    {
        private readonly ApplicationDbContext _db;

        public PricingHistoryRespository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<PricingHistory> GetByProduct(int productId)
        {

            return _db.PricingHistories.Where(s => s.ProductId == productId);

        }
    }
}
