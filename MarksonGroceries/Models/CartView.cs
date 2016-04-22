using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarksonGroceries.Models
{
    public class CartView
    {
        public int Id { get; set; }

        public int currentCartSize { get; set; }
        public List<CartViewSize> cartSizes { get; set; } 
        public List<ProduceView> produceItems { get; set; }
    }

    public class CartViewSize
    {
        public int Id { get; set; }  

        public string CartSize { get; set; }
    }
}