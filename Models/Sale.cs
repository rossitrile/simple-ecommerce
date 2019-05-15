using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnBoard.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime DateSold { get; set; } = DateTime.Now;

        // Relationship to customer 
        [ForeignKey("CustomerId")]
        public virtual Customer customer { get; set; }
        public int CustomerId { get; set; }

        // Relationship to product 

        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }
        public int ProductId { get; set; }

        // Relationship to product 

        [ForeignKey("StoreId")]
        public virtual Store store { get; set; }
        public int StoreId { get; set; }

    }
}