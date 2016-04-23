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
    public class CheckoutController : ApiController
    {

        private GroceriesEntities _db;

        public CheckoutController(GroceriesEntities db)
        {
            _db = db;
        }

        public List<CheckoutType> GetAllCheckoutTypes()
        {
            return _db.CheckoutTypes.ToList();
        }

        public bool PostCheckoutType(int id, int checkoutTypeId)
        {
            var checkedOut = false;

            try
            {
                var customer = _db.Customers.FirstOrDefault(c => c.Id == id);
                if (customer == null)
                {
                    throw new Exception();
                }
                customer.CheckoutTypeId = checkoutTypeId;
                customer.CheckoutTime = DateTime.Now;
                _db.SaveChanges();
                checkedOut = true;
            }
            catch (Exception e)
            {

            }

            return checkedOut;
        }

    }
}
