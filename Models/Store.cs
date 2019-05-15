using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnBoard.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }

    }
}