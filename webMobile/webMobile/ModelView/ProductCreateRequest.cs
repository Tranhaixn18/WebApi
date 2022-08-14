using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile.ModelView
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public string Decription { get; set; }
        public string Content { get; set; }
        public int Hot { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public int Amount { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
