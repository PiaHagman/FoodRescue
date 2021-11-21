using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class ItemSale
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime SalesDate { get; set; }
        
        public ICollection<LunchBox> LunchBoxes { get; set; }
        public User User { get; set; }

    }
}
