using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public class ProductReview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [MaxLength(150)]
        public string Comment { get; set; }


        [Required]
        [Display(Name = "Rate")]
         public int Rate { get; set; }


        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
