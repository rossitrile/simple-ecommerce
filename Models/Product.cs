using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnBoard.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price has to be greater than $0")]
        public double Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

    }
}