using DutchTreat.Data;
using DutchTreat.Data.Converter;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId}/items")]
    [Produces("application/json")]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IOrderItemMapper _ordemItemMapper;

        public OrderItemsController(IDutchRepository repository,
            ILogger<OrderItemsController> logger,
            IOrderItemMapper orderItemMapper)
        {
            _repository = repository;
            _logger = logger;
            _ordemItemMapper = orderItemMapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable> Get(int orderId)
        {
            try
            {
                Order order = _repository.GetOrderById(orderId);
                IEnumerable<OrderItem> items = order.Items;
                if (order != null) return Ok(_ordemItemMapper.Map(items));
                else return NotFound();
            }
            catch (Exception ex)
            {
                const string defaultErrorMessage = "Failed to get the order Items";

                _logger.LogError($"{defaultErrorMessage}: {ex}");
                return base.BadRequest(defaultErrorMessage);
            }
        }

        [HttpGet]
        [Route("{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable> Get(int orderId, int orderItemId)
        {
            try
            {
                Order order = _repository.GetOrderById(orderId);
                IEnumerable<OrderItem> items = order.Items;
                OrderItem orderItem = null;

                if (order != null)
                {
                    orderItem = items.Where(oi => oi.Id == orderItemId).FirstOrDefault();
                }

                if (order != null && orderItem != null) return Ok(_ordemItemMapper.Map(orderItem));
                else return NotFound();
            }
            catch (Exception ex)
            {
                const string defaultErrorMessage = "Failed to get the order Item";

                _logger.LogError($"{defaultErrorMessage}: {ex}");
                return base.BadRequest(defaultErrorMessage);
            }
        }
    }
}
