using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Entity
{
    [Table("Donator")]
    public class Donator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]      
        [Column("ID")]
        public int DonatorId { get; set; }
        [StringLength(10,MinimumLength =1)]
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DonateDate { get; set; }    
        
        public virtual ICollection<PayWay> PayWays { get; set; }
    }
}
