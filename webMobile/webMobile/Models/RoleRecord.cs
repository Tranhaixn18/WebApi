using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile
{
    [Table("tblRole")]
    public class RoleRecord
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MenuCategory { get; set; }
        public string MenuProduct { get; set; }
        public string MenuOrder { get; set; }
        public string MenuReport { get; set; }
        public string MenuUser { get; set; }

        public string MenuNew { get; set; }


    }
}
