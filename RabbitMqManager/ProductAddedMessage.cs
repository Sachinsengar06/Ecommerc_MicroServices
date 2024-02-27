using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqManager
{
    public class ProductAddedMessage
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
