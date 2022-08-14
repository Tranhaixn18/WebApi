using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using webMobile.Models;

namespace webMobile
{
    [Table("tblProducts")]
    public class ProductsRecord
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public string Decription { get; set; }
        public string Content { get; set; }
        public int Hot { get; set; }
        public string? Photo { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public int Amount { get; set; }
        public List<ProductImageRecord> ProductImages { get; set; }
    }
}
