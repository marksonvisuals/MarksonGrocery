using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarksonGroceries.Models
{
    public class CartView
    {
        public int Id { get; set; }
        public int CurrentCartSize { get; set; }
        public List<CartViewSize> CartSizes { get; set; } 
        public List<ProduceView> ProduceItems { get; set; }
        public List<CheckoutView> CheckoutTypes { get; set; } 
    }

    public class CartViewSize
    {
        public int Id { get; set; }  

        public string CartSize { get; set; }
    }
}