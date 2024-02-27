using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Model;
using ProductServices.RabbitMqService;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>();
        private static List<PublishProduct> publishProducts= new List<PublishProduct>();
        private static int _nextProductId = 1;
        private readonly IMessageProducer _messageProducer;
        public ProductController(IMessageProducer messageProducer) 
        {
            _messageProducer = messageProducer;
        }

        [HttpPost("addToCart")]
        public IActionResult AddToCart([FromBody] AddToCart request)
        {
            if(request.Id<0)
            {
                return BadRequest();
            }
            var product = _products.FirstOrDefault(x => x.Id == request.Id);
            if(product == null)
            {
                return NotFound();
            }
            var newPublishProduct = new PublishProduct
            {
                ProductId = product.Id,
                ProductDescription = product.Description,
                ProductName = product.Name,
                ProductPrice = product.Price,
                ProductSize = product.Size,
                ProductQty = 1,
                UserId = request.UserId
            };

            _messageProducer.SendingMessage<PublishProduct>(newPublishProduct);
            //_messageProducer.SendingMessage<PublishProduct>(newPublishProduct);

            return Ok(newPublishProduct);
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct(Product data)
        {
            try
            {

                if (data == null)
                {
                    return BadRequest("Invalid product data");
                }

                var newProduct = new Product
                {
                    Id = _nextProductId++,
                    Name = data.Name,
                    Description = data.Description,
                    Size = data.Size,
                    Price = data.Price,
                };
                _products.Add(newProduct);

               
                return Ok("Product added successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding product");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("getAllProducts")]
        public IActionResult GetAllProducts()
        {
            return Ok(_products);
        }


        [HttpPost("updateProduct/{id}")]

        public IActionResult EditProduct(int id, Product data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingProduct = _products.FirstOrDefault(x => x.Id == id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.Name = data.Name;
                existingProduct.Description = data.Description;
                existingProduct.Size = data.Size;
                existingProduct.Price = data.Price;

                return Ok("Product updated successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding product");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("getProduct/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpDelete("deleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var index = _products.Find(p => p.Id == id);
                if (index == null)
                    return NotFound();

                _products.Remove(index);
                return Ok("product deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
