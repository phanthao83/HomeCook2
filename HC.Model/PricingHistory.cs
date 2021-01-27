using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public class PricingHistory
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        public DateTime UpdateDate { get; set; }

        [Display(Name = "Old Price")]
        public double OPrice { get; set; }

        [Display(Name = "New Price")]
        public double NPrice { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
