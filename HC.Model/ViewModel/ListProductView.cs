using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model.ViewModel
{
    public class ListProductView
    {
       public string ProductImagePath { get; set; }

        public int SelectedCatgory { get; set; }

        public string SelectedCategoryName { get; set; }
    }
}
