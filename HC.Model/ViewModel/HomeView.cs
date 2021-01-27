using HC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model.ViewModel
{
    public class HomeView
    {
        // Category list 
        public IEnumerable<Category> CategoryList { get; set; }
        
        
        public IEnumerable<ProductSimpleView> BestProducts { get; set; }

        public IEnumerable<ProductSimpleView> NewProducts { get; set; }
        // Product List 
        // Supplier List 
        // New Customer List 
        
        public IEnumerable<AppUserView> BestSuppliers { get; set; }

        public string ProductImagePath { get; set; }
        public string AvatarPath { get; set; }
    }
}
