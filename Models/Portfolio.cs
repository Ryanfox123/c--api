using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public required string AppUserId { get; set; }

        [ForeignKey("Stock")]     
        public int stockId { get; set; }
        public required AppUser AppUser { get; set; }
        public required Stock Stock { get; set; }
    }
}