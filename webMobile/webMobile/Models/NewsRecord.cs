using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile
{
    [Table("News")]
    public class NewsRecord
    {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Content { get; set; }
            public int Hot { get; set; }
            public string Photo { get; set; }

    }
}
