using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile
{
    [Table("RoleAction")]
    public class RoleActionRecord
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? Create { get; set; }
        public int? Edit { get; set; }
        public int? Delete { get; set; }

    }
}
