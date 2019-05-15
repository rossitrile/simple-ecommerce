using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnBoard.Resources
{
    public class ProductUpdateResource
    {
        public string Name { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price has to be greater than $0")]
        public double Price { get; set; }

    }
}