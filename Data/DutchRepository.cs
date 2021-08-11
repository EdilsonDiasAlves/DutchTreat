using DutchTreat.Data.Entities;
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

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
