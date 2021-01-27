using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model.ViewModel
{
    public class PricingView
    {
        public PricingHistory PricingHistory { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<PricingHistory> PricingHistoryLst { get; set; }


    }
}
