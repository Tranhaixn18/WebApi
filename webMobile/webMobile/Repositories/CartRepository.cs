using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webMobile.Models;
using webMobile.ModelView;

namespace webMobile.Repositories
{
    public interface ICartRepository
    {
        Task<CartRepository> GetCart(string userName);
        Task<int> UpdateAmount(int amount);
        Task AddCart(string user, int idProduct);
        Task RemoveCart(int idCart);
       
    }
    public class CartRepository : ICartRepository
    {
        private readonly MyDbContext _db;
        public CartRepository(MyDbContext db)
        {
            _db = db;
        }
        public Task AddCart(string user, int idProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<CartRepository> GetCart(string userName)
        {
            var cart = await _db.CartRecords.SingleOrDefaultAsync(tbl=>tbl.UserName==userName);
            if(cart != null)
            {
                var result = from c in _db.CartRecords
                             join p in _db.Products
                             on c.ProductId equals p.ID
                             select new { c, p };
                var data= await result.Select(new CartModel() {

                })
            }
        }

        public Task RemoveCart(int idCart)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAmount(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
