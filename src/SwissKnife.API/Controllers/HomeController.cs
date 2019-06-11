using Microsoft.AspNetCore.Mvc;

namespace SwissKnife.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Swagger Docs
    /// </summary>
    //[SwaggerResponse(HttpStatusCode.NotFound, "Could not find the person", typeof(ErrorResponse))]
    public class HomeController : Controller
    {
        // GET
        /// <summary>
        ///     Return view
        /// </summary>
        /// <returns>View result</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Get item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok</returns>
        [HttpGet]
        public IActionResult GetItem(int id)
        {
            return Ok(new {item = id});
        }

        /// <summary>
        ///     Fake post
        /// </summary>
        /// <returns>Ok</returns>
        [HttpPost]
        public IActionResult PostItem()
        {
            return Ok();
        }

        /// <summary>
        ///     Bad requests
        /// </summary>
        /// <returns>Bad</returns>
        [HttpGet]
        public IActionResult GetBadResult()
        {
            return BadRequest();
        }
    }
}