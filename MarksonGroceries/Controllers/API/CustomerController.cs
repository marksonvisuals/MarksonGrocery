using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using MarksonGroceries.Models.Data;

namespace MarksonGroceries.Controllers.API
{
    public class CustomerController : ApiController
    {

        private GroceriesEntities _db;

        public CustomerController(GroceriesEntities db)
        {
            _db = db;
        }

        // GET: api/Cart
        public Customer Get(string sessionId)
        {
            var customer = new Customer();
            try
            {
                var existingCustomer = _db.Customers.FirstOrDefault(c => c.SessionId == sessionId);
                if (existingCustomer == null)
                {
                    var newCustomer = Post(sessionId);
                    customer.Id = newCustomer.Id;
                    customer.CartSizeId = newCustomer.CartSizeId;
                }
                else
                {
                    customer = existingCustomer;
                }

            }
            catch (Exception e)
            {
                
            }

            return customer;
        }

        // POST: api/Cart
        public Customer Post(string sessionId)
        {
            var newCustomer = new Customer
            {
                SessionId = sessionId,
                CartSizeId =
                    _db.CartSizes.First(c => c.Name == "None").Id
            };

            var savedCustomer = _db.Customers.Add(newCustomer);

            _db.SaveChanges();

            return savedCustomer;
        }

    }
}
