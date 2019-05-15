using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnBoard.Resources
{
    public class SaleUpdateResource
    {
        public string CustomerId { get; set; }

        public string ProductId { get; set; }

        public string StoreId { get; set; }

    }
}