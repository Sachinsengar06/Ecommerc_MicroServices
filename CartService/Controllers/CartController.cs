using CartService.BackGroundServices;
using CartService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;


namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ReceivedData _receivedData;


        public CartController(ReceivedData receivedData)
        {
            _receivedData = receivedData;
            //_receivedData.ReceiveProduct();
        }

        

       
        [HttpGet("items/{UserId}")]
        public ActionResult GetCartItems(int UserId)
        {
            try
            {
                //return Ok(ReceivedData._userProducts);
                var userId = Convert.ToString(UserId);
                if (_receivedData._userProducts.ContainsKey(userId))
                {
                    // Return a copy of the product list to avoid modifying the original

                    return Ok(_receivedData._userProducts[userId]);
                }
                else
                {
                    // User not found, return an empty list
                    return Ok(new List<Product>());
                }
                //return Ok(ReceivedData._products);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}