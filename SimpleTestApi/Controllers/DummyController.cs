
using Microsoft.AspNetCore.Mvc;
using SimpleTestApi.Model;

namespace SimpleTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController() : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
            new Product { Id = 2, Name = "Mouse", Price = 25.50m }
        };

      
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_products);
        }

 
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product not found.");
            var userIpAddress =HttpContext.Connection.RemoteIpAddress;
            var IPV4 = userIpAddress.MapToIPv4();
            return Ok(new { product, IP = userIpAddress.ToString(), IP2= IPV4.ToString() });
        }

    
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product newProduct)
        {
            newProduct.Id = _products.Max(p => p.Id) + 1;
            _products.Add(newProduct);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            var index = _products.FindIndex(p => p.Id == id);
            if (index == -1) return NotFound();

            _products[index] = updatedProduct;
            _products[index].Id = id; 
            return NoContent();
        }

   
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return NoContent();
        }
    }
}

