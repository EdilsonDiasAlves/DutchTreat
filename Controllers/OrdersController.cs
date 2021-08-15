using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                const string defaultErrorMessage = "Failed to get the orders";

                _logger.LogError($"{defaultErrorMessage}: {ex}");
                return base.BadRequest(defaultErrorMessage);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Order> Get(int id)
        {
            try
            {
                var order = _repository.GetById(id);
                if(order != null)
                {
                    return Ok(order);
                } else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                const string defaultErrorMessage = "Failed to get the orders";

                _logger.LogError($"{defaultErrorMessage}: {ex}");
                return base.BadRequest(defaultErrorMessage);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] OrderViewModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order()
                    {
                        Id = order.OrderId,
                        OrderDate = order.OrderDate,
                        OrderNumber = order.OrderNumber
                    };

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }


                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        var orderAdded = new OrderViewModel()
                        {
                            OrderId = newOrder.Id,
                            OrderDate = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber
                        };

                        return Created($"/api/orders/{orderAdded.OrderId}", orderAdded);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
                return base.BadRequest(ModelState);
            }

            return BadRequest("Failed to save a new order");
        }
    }
}
