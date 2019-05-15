using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnBoard.Resources
{
    public class StoreReturnResource
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}