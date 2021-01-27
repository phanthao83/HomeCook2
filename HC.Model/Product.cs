using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public static class ProductStatus
    {
        public static string Pending = "P";
        public static string Active = "A";
        public static string Deleted = "D";

    }
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "Product Description")]
        [MaxLength(100)]
        public string Description { get; set; }


        [Display(Name = "Price")]
         public double Price { get; set; }

        [Display(Name = "Avg. Rate")]
        public int AvgRating { get; set; }

        public string  UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        [Display(Name = "Product Type ")] 
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }



         public int UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }

        [Required]
        [Display(Name = "Product Status")]
        [MaxLength(10)]
        public string Status { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
    }
}
