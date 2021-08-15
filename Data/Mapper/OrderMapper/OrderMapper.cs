using DutchTreat.Data.Converter;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class OrderMapper : IOrderMapper
    {
        public OrderViewModel Map(Order origin)
        {
            if (origin == null) return null;
            return new OrderViewModel
            {
                OrderId = origin.Id,
                OrderDate = origin.OrderDate,
                OrderNumber = origin.OrderNumber
            };
        }

        public IEnumerable<OrderViewModel> Map(IEnumerable<Order> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Map(item)).ToList();
        }

        public Order Map(OrderViewModel origin)
        {
            if (origin == null) return null;
            return new Order
            {
                Id = origin.OrderId,
                OrderDate = origin.OrderDate,
                OrderNumber = origin.OrderNumber
            };
        }

        public IEnumerable<Order> Map(IEnumerable<OrderViewModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Map(item)).ToList();
        }
    }
}
