using DutchTreat.Data.Converter;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class OrderItemMapper : IOrderItemMapper
    {
        public OrderItemViewModel Map(OrderItem origin)
        {
            if (origin == null) return null;
            return new OrderItemViewModel
            {
                Id = origin.Id,
                Quantity = origin.Quantity,
                UnitPrice = origin.UnitPrice,
                ProductId = origin.Product.Id,
                ProductCategory = origin.Product.Category,
                ProductSize = origin.Product.Size,
                ProductTitle = origin.Product.Title,
                ProductArtist = origin.Product.Artist,
                ProductArtId = origin.Product.ArtId
            };
        }

        public IEnumerable<OrderItemViewModel> Map(IEnumerable<OrderItem> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Map(item)).ToList();
        }

        public OrderItem Map(OrderItemViewModel origin)
        {
            if (origin == null) return null;
            return new OrderItem
            {
                Id = origin.Id,
                Quantity = origin.Quantity,
                UnitPrice = origin.UnitPrice
            };
        }

        public IEnumerable<OrderItem> Map(IEnumerable<OrderItemViewModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Map(item)).ToList();
        }
    }
}
