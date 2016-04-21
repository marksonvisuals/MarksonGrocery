using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarksonGroceries.Models;

namespace MarksonGroceries.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cart2 = new CartModel();
            //For Null Propogation, you need to use Visual Studio 2015 (C# 6.0)
            if (HttpContext.Session?["cartSize"] != null)
            {
                cart2 = (CartModel)HttpContext.Session["cartSize"];
            }

            return View(cart2);
        }


        public void SetVar()
        {
            var changeCart = new CartModel { cart = CartSize.Jumbo };
            if (HttpContext.Session != null) HttpContext.Session["cartSize"] = changeCart;
        }

        public void ChangeVar()
        {

            var changeCart = new CartModel { cart = CartSize.Medium };
            if (HttpContext.Session != null) HttpContext.Session["cartSize"] = changeCart;
        }
    }
}