using System.ComponentModel.DataAnnotations;

namespace MarksonGroceries.Models
{
    public class CartModel
    {
        public CartSize cart { get; set; }
    }

    public enum CartSize
    {
        [Display(Name = "Small")]
        Small,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "Large")]
        Large,
        [Display(Name = "Jumbo")]
        Jumbo
    }
}