using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public static class OrderStatus
    {
        public static string Active = "A";
        public static string Cancelled = "C";

    }

    public static class OrderType
    {
        public static string CashOnDelivery = "COD";
        public static string CashOnPickUp = "COP";

    }
    public class Order
    {
        [Key]
        public int Id { get; set; }
              
        
        [Display(Name = "Buyer")]
        public string BuyerId { get; set; }
       
        [ForeignKey("BuyerId")]
        public ApplicationUser User { get; set; }


        [Display(Name = "Sub Total ")]
        public float SubTotal { get; set; }

        [Display(Name = "Tax")]
        public float TaxAmount { get; set; }

        [Display(Name = "Total")]
        public float TotalAmount { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime DelveryDate { get; set; }

        [Display(Name = "Order Type")]
        [MaxLength(3)]
        public string OrderType { get; set; }

        [Display(Name = "Order Status")]
        [MaxLength(1)]
        public string OrderStatus { get; set; }

      

    }
}
