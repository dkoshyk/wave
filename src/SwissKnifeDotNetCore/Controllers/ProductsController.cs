using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwissKnifeDotNetCore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
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
        [Microsoft.AspNetCore.Mvc.HttpPost(Name = "CreateProduct")]
        public Product Create([Microsoft.AspNetCore.Mvc.FromBody, Required] Product product)
        {
            return product;
        }

        /// <summary>
        /// Searches the collection of products by description key words
        /// </summary>
        /// <param name="keywords">A list of search terms</param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet(Name = "SearchProducts")]
        public IEnumerable<Product> Get([FromQuery(Name = "kw")] string keywords = "foobar")
        {
            return new[]
            {
                new Product {Id = 1, Description = "A product"},
                new Product {Id = 2, Description = "Another product"},
            };
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

            var product = new Product() { Id = 1, Description = "1", Status = ProductStatus.All };

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

        public enum ProductStatus
        {
            All = 0,
            OutOfStock = 1,
            InStock = 2
        }

        /// <summary>
        /// Represents a product
        /// </summary>
        public class Product
        {
            /// <summary>
            /// Uniquely identifies the product
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Describes the product
            /// </summary>
            public string Description { get; set; }

            public ProductStatus Status { get; set; }
        }
    }
}