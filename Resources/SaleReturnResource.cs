using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnBoard.Models;

namespace OnBoard.Resources
{
    public class SaleReturnResource
    {
        public int SaleId { get; set; }
        public DateTime DateSold { get; set; }
        public string Customer { get; set; }
        public string Store { get; set; }
        public string Product { get; set; }


    }
}