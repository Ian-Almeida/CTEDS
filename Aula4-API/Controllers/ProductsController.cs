using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aula4_API.Models;
using Aula4_API.Repositories;

namespace Aula4_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductRepository _product { get; set; }

        public ProductsController()
        {
            _product = new ProductRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            { 
                return Ok(_product.ReadAll());
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }

        }

        [HttpPost]
        public IActionResult Post(Product newProduct)
        {
            try
            {
                _product.Create(newProduct);
                return StatusCode(201);
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }
    }
}
