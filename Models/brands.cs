using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class brands
    {

        public String brandName { get; set; }
        [Key]
        public int brandId { get; set; }
        public String glassType { get; set; }
    }
}
