//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarksonGroceries.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> ProduceId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Produce Produce { get; set; }
    }
}
