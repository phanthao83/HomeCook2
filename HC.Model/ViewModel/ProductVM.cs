using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model.ViewModel
{
    public class ProductVM
    {

        public Product Product { get; set; }

        public string PicFileNames { get; set; }
        public string UploadedImgIdBeRemoved { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }


        public IEnumerable<ProductReview> Reviews { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> UnitList { get; set; }

        public IEnumerable<PricingHistory> PricingHistory { get; set; }

        public string ErrorMessage { get; set; }
        public int MaxUploadFileNumber { get; set; }


    }
}
