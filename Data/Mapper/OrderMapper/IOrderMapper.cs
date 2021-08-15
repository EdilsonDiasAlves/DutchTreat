using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.Converter
{
    public interface IOrderMapper : IMapper<Order, OrderViewModel>, IMapper<OrderViewModel, Order>
    {
        
    }
}
