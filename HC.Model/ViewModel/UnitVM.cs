using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model.ViewModel
{
    public class UnitVM
    {
        public Unit Unit { get; set;}
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
