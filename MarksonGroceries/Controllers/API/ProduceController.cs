using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MarksonGroceries.Models.Data;

namespace MarksonGroceries.Controllers.API
{
    public class ProduceController : ApiController
    {

        private GroceriesEntities _db;

        public ProduceController(GroceriesEntities db)
        {
            _db = db;
        }

        // GET: api/Cart
        public List<Produce> GetAllProduceItems()
        {
            return _db.Produces.ToList();
        }

        public List<Produce> GetAllProduceInCart(int id)
        {
            //return _db.Carts.Where(c => c.CustomerId == id).ToList();
            return (from c in _db.Carts
                join p in _db.Produces on c.ProduceId equals p.Id
                where c.CustomerId == id
                select p).ToList();
        } 

        public bool PostItemToCart([FromUri]int id, [FromUri]int produceId)
        {
            var added = false;
            try
            {
                var cart = new Cart
                {
                    CustomerId = id,
                    ProduceId = produceId
                };
                _db.Carts.Add(cart);
                _db.SaveChanges();
                added = true;
            }
            catch (Exception e)
            {
                
            }
            return added;
        }

        // POST: api/Cart
        public bool DeleteItemFromCart([FromUri]int id, [FromUri]int produceId)
        {
            var removed = false;
            try
            {

                var cartItem = _db.Carts.FirstOrDefault(p => p.ProduceId == produceId);
                if (cartItem != null)
                {
                    _db.Carts.Remove(cartItem);
                    _db.SaveChanges();
                    removed = true;
                }
                
                
            }
            catch (Exception e)
            {

            }
            return removed;
        }

    }
}
