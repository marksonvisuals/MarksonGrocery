using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MarksonGroceries.Models.Data;

namespace MarksonGroceries.Controllers.API
{
    public class CartController : ApiController
    {

        private GroceriesEntities _db;
        private CustomerController _customerController;

        public CartController(GroceriesEntities db, CustomerController customerController)
        {
            _db = db;
            _customerController = customerController;
        }

        // GET: api/Cart
        public List<CartSize> GetAllCartSizes()
        {
            return _db.CartSizes.ToList();
        }

        public int GetCartSizeByName(string name)
        {
            try
            {
                return _db.CartSizes.First(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase)).Id;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NoContent
                });
            }
        }


        public List<Cart> GetExistingCart(string sessionId)
        {
            try
            {
                var existingCustomer =  _customerController.Get(sessionId);
                
                return _db.Carts.Where(c => c.CustomerId == existingCustomer.Id).ToList();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NoContent
                });
            }
        }

        public bool PutCartSize([FromUri]int id, [FromUri]int cartSizeId)
        {
            var changed = false;

            try
            {
                var customer = _db.Customers.First(c => c.Id == id);
                customer.CartSizeId = cartSizeId;
                _db.SaveChanges();
                changed = true;

            }
            catch (Exception e)
            {
                
            }

            return changed;
        }

    }
}
