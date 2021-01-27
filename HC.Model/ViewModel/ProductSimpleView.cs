using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HC.Model.ViewModel
{
    public class ProductSimpleView
    {
        // Product id , name , price, unit descripiton , category id,  category name , first picture 
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [MaxLength(30)]
        public string Name { get; set; }

       [Display(Name = "Price")]
        public int Price { get; set; }

       

        [Display(Name = "Product Type ")]
        public int CategoryId { get; set; }


        [Display(Name = "Product Type ")]
        public string CategoryName { get; set; }



        public int UnitId { get; set; }

        [Display(Name = "Unit ")]
        public string UnitName { get; set; }

        

        [Display(Name = "Purchased Quantity")]
        public int PQuantity { get; set; }

        public string FileName { get; set; }

    }
}
