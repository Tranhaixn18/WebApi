using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile.ModelView
{
    public class CartModel
    {
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public ProductsRecord ProductItem { get; set; }
        public double ThanhTien => Convert.ToDouble(Quantity * (ProductItem.Price - (ProductItem.Price * ProductItem.Discount) / 100));
    }
}
