using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class product
    {
        [Key]
        public int productId { get; set; }
        public String productType { get; set; }
        public String productName { get; set; }
        public String color { get; set; }
        public double price { get; set; }
        

    }
}
