using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile.ModelView
{
    public class ProductUpdateRepuest
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Decription { get; set; }
        public string Content { get; set; }
        public int Hot { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
