using DutchTreat.Data;
using DutchTreat.Data.Entities;
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
    }
}
