using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnBoard.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Customer Address is required")]
        public string Address { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }


    }
}