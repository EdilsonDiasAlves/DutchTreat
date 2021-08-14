using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders();
        Order GetById(int id);

        bool SaveAll();
        
    }
}