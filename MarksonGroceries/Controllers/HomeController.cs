using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MarksonGroceries.Helpers;
using MarksonGroceries.Helpers.HttpClient;
using MarksonGroceries.Models;
using MarksonGroceries.Models.Data;

namespace MarksonGroceries.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _sessionId;
        public HomeController(IHttpClientInitializer httpClientInterface)
        {
            _httpClient = httpClientInterface.GetHttpClient();
            //Adding a useless session variable a ASP.Net_SessionId
            //cookie will be created in browser
            System.Web.HttpContext.Current.Session["setCookie"] = "1";
        }

        public ActionResult Index()
        {
            _sessionId = HttpContext.Session.SessionID;

            var cartView = GetCurrentCustomer();
            var produce = GetAllProduce();
            var produceInCart = GetAllProduceInCart(cartView.Id);
            cartView.CartSizes = GetAllCartSizes();
            cartView.CheckoutTypes = GetAllCheckoutTypes();
            cartView.ProduceItems = SetProduceListWithProduceInCart(produce, produceInCart);

            return View(cartView);
        }

        public void ResetSession()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

        }

        public List<ProduceView> GetAllProduce()
        {
            var allProduce = new List<ProduceView>();

            try
            {
                var httpResponse = _httpClient.GetAsync("/api/Produce/GetAllProduceItems").Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = httpResponse.Content.ReadAsAsync<List<Produce>>().Result;
                    allProduce = MapDataListProduceToListProduceView(content);
                }


            }
            catch (Exception e)
            {

            }

            return allProduce;
        }



        public List<ProduceView> GetAllProduceInCart(int id)
        {
            var allProduce = new List<ProduceView>();

            try
            {
                var httpResponse = _httpClient.GetAsync("/api/Produce/GetAllProduceInCart/" + id).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = httpResponse.Content.ReadAsAsync<List<Produce>>().Result;
                    allProduce = MapDataListProduceToListProduceView(content);
                }


            }
            catch (Exception e)
            {

            }

            return allProduce;
        }

        public List<CheckoutView> GetAllCheckoutTypes()
        {
            var allCheckoutTypes = new List<CheckoutView>();

            try
            {
                var httpResponse = _httpClient.GetAsync("/api/Checkout/GetAllCheckoutTypes/").Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = httpResponse.Content.ReadAsAsync<List<CheckoutType>>().Result;
                    allCheckoutTypes = MapDataListCheckoutTypeToListCheckoutView(content);
                }


            }
            catch (Exception e)
            {

            }

            return allCheckoutTypes;
        }

        public CartView GetCurrentCustomer()
        {
            var cart = new CartView();

            try
            {
                var httpResponse = _httpClient.GetAsync("/api/Customer/Get?sessionId=" + _sessionId).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = httpResponse.Content.ReadAsAsync<Customer>().Result;
                    cart = MapDataCustomerToCartView(content);
                }


            }
            catch (Exception e)
            {

            }

            return cart;
        }


        public List<CartViewSize> GetAllCartSizes()
        {
            var cartSizes = new List<CartViewSize>();

            try
            {
                var httpResponse = _httpClient.GetAsync("/api/Cart/GetAllCartSizes").Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = httpResponse.Content.ReadAsAsync<List<CartSize>>().Result;
                    cartSizes =  MapDataListCartSizeToListCartViewSize(content);
                }


            }
            catch (Exception e)
            {

            }

            return cartSizes;

        }

        private CartView MapDataCustomerToCartView(Customer customer)
        {
            return new CartView
            {
                Id = customer.Id,
                CurrentCartSize = customer.CartSizeId,
                CartSizes = new List<CartViewSize>(),
                ProduceItems = new List<ProduceView>(),
                CheckoutTypes = new List<CheckoutView>()

            };
        }

        private List<CartViewSize> MapDataListCartSizeToListCartViewSize(List<CartSize> cartsizes)
        {
            return cartsizes.Select(MapDataCartSizeToCartViewSize).ToList();
        }

        private CartViewSize MapDataCartSizeToCartViewSize(CartSize cartsize)
        {
            return new CartViewSize
            {
              Id  = cartsize.Id,
              CartSize = cartsize.Name
            };
        }

        private List<CheckoutView> MapDataListCheckoutTypeToListCheckoutView(List<CheckoutType> checkoutTypes)
        {
            return checkoutTypes.Select(MapDataCheckoutTypeToheckoutView).ToList();
        }

        private CheckoutView MapDataCheckoutTypeToheckoutView(CheckoutType checkoutType)
        {
            return new CheckoutView
            {
                CheckoutId = checkoutType.Id,
                CheckoutName = checkoutType.Name
            };
        }

        private List<ProduceView> MapDataListProduceToListProduceView(List<Produce> produces)
        {
            return produces.Select(MapDataProduceToProduceView).ToList();
        }

        private ProduceView MapDataProduceToProduceView(Produce produce)
        {
            return new ProduceView
            {
                Id = produce.Id,
                CurrentlyInCart = false,
                ItemName = produce.Name,
                ItemPrice = produce.Price
            };
        }

        private List<ProduceView> SetProduceListWithProduceInCart(List<ProduceView> masterList,
            List<ProduceView> currentlyInCart)
        {
            var combinedProduce = new List<ProduceView>();

            foreach (var m in masterList)
            {
                if (currentlyInCart.FirstOrDefault(c=>c.Id == m.Id)!= null)
                {
                    combinedProduce.Add(new ProduceView
                    {
                        Id = m.Id,
                        CurrentlyInCart = true,
                        ItemPrice = m.ItemPrice,
                        ItemName = m.ItemName
                    });
                }
                else
                {
                    combinedProduce.Add(m);
                }
            }

            return combinedProduce;
        }
    }
};