using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product{ get; set; }

        [Display(Name = "Purchased quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Amount")]
        public float Amount { get; set; }

        [Display(Name = "Order Detail Status")]
        [MaxLength(1)] 
        public string Status { get; set; }

        [Display(Name = "Note")]
        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
