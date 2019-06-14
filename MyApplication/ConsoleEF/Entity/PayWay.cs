using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Entity
{
    [Table("PayWay")]    
    public class PayWay
    {
        [Key]
        public int ID { get; set; }
        public int? DonatorID { get; set; }
        public string PayWayName { get; set; }
    }
}
