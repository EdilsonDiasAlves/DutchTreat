using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
        }

        public IEnumerable<Product> GetAllProductsByCategory(string category)
        {
            return _ctx.Products
                    .Where(p => p.Category == category)
                    .ToList();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public Order GetById(int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
