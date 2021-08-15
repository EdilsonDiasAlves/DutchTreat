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
        private readonly OrderMapper _mapper;

        public OrdersController(IDutchRepository repository, 
            ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = new OrderMapper();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                var result = _repository.GetAllOrders();
                return Ok(_mapper.Map(result));
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
                    return Ok(_mapper.Map(order));
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
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map(model);

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map(newOrder));
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
