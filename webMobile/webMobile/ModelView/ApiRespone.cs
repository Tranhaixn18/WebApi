using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile.ModelView
{
    public class ApiRespone
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
