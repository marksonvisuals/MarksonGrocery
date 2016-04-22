using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarksonGroceries.Models
{
    public class ProduceView
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }

        public bool CurrentlyInCart { get; set; }
    }

}