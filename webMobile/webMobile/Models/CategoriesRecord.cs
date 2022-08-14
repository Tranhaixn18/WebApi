using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile
{
    [Table("tblCategories")]
    public class CategoriesRecord
    {
        [Key]
        public int Id { get; set; }
       
        public string Name { get; set; }

    }
}
