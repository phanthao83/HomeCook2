using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "File Name")]
        [MaxLength(500)]
        public string FileName { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Boolean IsDefault { get; set; }
    }
}
