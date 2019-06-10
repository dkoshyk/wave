using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SwissKnifeDotNetCore.Commands;
using SwissKnifeDotNetCore.Data.Entities;
using SwissKnifeDotNetCore.Persistence;
using SwissKnifeDotNetCore.Queries;

namespace SwissKnifeDotNetCore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IQueriesService _queries;
        private readonly ICommandService _command;
        private readonly AppDbContext _context;

        public ProductsController(IQueriesService queries, ICommandService command, AppDbContext context)
        {
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _context = context;
        }

        /// <summary>
        /// Creates a <paramref name="product"/>
        /// </summary>
        /// <remarks>
        /// ## Heading 1
        /// 
        ///     POST /products
        ///     {
        ///         "id": "123",
        ///         "description": "Some product"
        ///     }
        /// 
        /// </remarks>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> Create([FromBody, Required] Product product)
        {
            await _command.SaveProduct(product.Name);

            return NoContent();
        }

        /// <summary>
        /// Searches the collection of products by description key words
        /// </summary>
        /// <param name="keywords">A list of search terms</param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet(Name = "SearchProducts")]
        public async Task<IActionResult> Get()
        {
            //return Ok(_context.Products.ToList());

            return Ok((await _queries.GetAll()).ToList());
        }

        /// <summary>
        /// Returns a specific product 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet("{id:int:min(0)}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            //return new Product {Id = id, Description = "A product"};

            var product = new Product() { Id = "2", Description = "1" };

            return new OkResult();
        }

        /// <summary>
        /// Updates all properties of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}", Name = "UpdateProduct")]
        public void Update(int id, [Microsoft.AspNetCore.Mvc.FromBody, Required] Product product)
        {
        }

        /// <summary>
        /// Updates some properties of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updates"></param>
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}", Name = "PatchProduct")]
        public void Patch(int id, [Microsoft.AspNetCore.Mvc.FromBody, Required] IDictionary<string, object> updates)
        {
        }

        /// <summary>
        /// Deletes a specific product
        /// </summary>
        /// <param name="id"></param>
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}", Name = "DeleteProduct")]
        public void Delete(int id)
        {
        }
    }
}