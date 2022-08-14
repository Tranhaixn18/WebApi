using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile.Models
{
    [Table("Carts")]
    public class CartRecord
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        public double TotalAmount { get; set; }
        public List<ProductsRecord> listProduct { get; set; }

    }
}
