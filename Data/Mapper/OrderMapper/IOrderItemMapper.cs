using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Data.Converter
{
    public interface IOrderItemMapper : IMapper<OrderItem, OrderItemViewModel>, IMapper<OrderItemViewModel, OrderItem>
    {
        
    }
}
