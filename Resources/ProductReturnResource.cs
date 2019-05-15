using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnBoard.Resources
{
    public class ProductReturnResource
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}