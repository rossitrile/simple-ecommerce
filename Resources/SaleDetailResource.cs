using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnBoard.Models;

namespace OnBoard.Resources
{
    public class SaleDetailResource
    {
        public int SaleId { get; set; }
        public DateTime DateSold { get; set; }
        public CustomerReturnResource Customer { get; set; }
        public StoreReturnResource Store { get; set; }
        public ProductReturnResource Product { get; set; }


    }
}