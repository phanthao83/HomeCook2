using System;
using System.Collections.Generic;
using System.Text;
using HC.Model;
namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IPricingHistoryRespository : IRepository<PricingHistory>
    {
        //    void Update(PricingHistory pricingHistory);

        IEnumerable<PricingHistory> GetByProduct(int productId);

    }
}
