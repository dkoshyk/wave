using Microsoft.AspNetCore.Mvc;
using SwissKnife.API.Application.Commands;
using SwissKnife.API.Application.Queries;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SwissKnife.Infrastructure;

namespace SwissKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IQueriesService _queries;
        private readonly IMediator _mediator;

        public ProductsController(
            IQueriesService queries, 
            AppDbContext context,
            IMediator mediator)
        {
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _context = context;
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createProductCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand createProductCommand)
        {
            await _mediator.Send(createProductCommand);

            return NoContent();
        }

        /// <summary>
        ///     Searches the collection of products by description key words
        /// </summary>
        /// <param name="keywords">A list of search terms</param>
        /// <returns></returns>
        [HttpGet(Name = "SearchProducts")]
        public async Task<IActionResult> Get()
        {
            //return Ok(_context.Products.ToList());

            return Ok((await _queries.GetAll()).ToList());
        }

        /// <summary>
        ///     Returns a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int:min(0)}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            //return new Product {Id = id, Description = "A product"};

            var product = new Product { Description = "1"};

            return new OkResult();
        }

        /// <summary>
        ///     Updates all properties of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [HttpPut("{id}", Name = "UpdateProduct")]
        public void Update(int id, [FromBody] [Required] Product product)
        {
        }

        /// <summary>
        ///     Updates some properties of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updates"></param>
        [HttpPatch("{id}", Name = "PatchProduct")]
        public void Patch(int id, [FromBody] [Required] IDictionary<string, object> updates)
        {
        }

        /// <summary>
        ///     Deletes a specific product
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public void Delete(int id)
        {
        }
    }
}