using CartService.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace CartService.BackGroundServices
{
    public class ReceivedData
    {
        //public static readonly List<Product> _products = new List<Product>();
        public  readonly  Dictionary<string, List<Product>> _userProducts = new Dictionary<string, List<Product>>();

        public ReceivedData()
        {
            ReceiveProduct();
        }

        public void ReceiveProduct()
        {


            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq", // RabbitMQ server host
                UserName = "myuser",
                Password = "myuser1",
                Port = 5672
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "product_Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                if (body != null)
                {
                    var message = Encoding.UTF8.GetString(body);

                    var product = JsonConvert.DeserializeObject<Product>(Encoding.UTF8.GetString(body));
                    //Console.WriteLine(message);
                    if (product != null)
                    {
                        
                        //_products.Add(product);
                        AddProductToUser(product);
                    }
                }
            };

            channel.BasicConsume("product_Queue", true, consumer);
            
        }

        private void AddProductToUser(Product product)
        {
            string userId = Convert.ToString(product.UserId);
            lock(_userProducts)
            {
                if (!_userProducts.ContainsKey(userId))
                {
                    _userProducts.Add(userId, new List<Product>());
                }

                var userProducts = _userProducts[userId];
                int existingProductIndex = userProducts.FindIndex(p => p.ProductId == product.ProductId);

                if (existingProductIndex != -1)
                {
                    // Increase quantity of existing product
                    userProducts[existingProductIndex].ProductQty += product.ProductQty;
                }
                else
                {
                    // Add new product to user's list
                    userProducts.Add(product);
                }
            }
          
        }
    }
}
