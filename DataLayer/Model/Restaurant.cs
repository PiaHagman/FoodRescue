using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Restaurant
    {
        public int Id { get; set; } //blir automatiskt en[Key] om du har en int som heter Id eller int som heter classnamn+id
        [MaxLength(40)] [Required]
        public string Name { get; set; }
        [MaxLength(40)] [Required]
        public string Address { get; set; }
        [MaxLength(40)] [Required]
        public string PhoneNumber { get; set; }

        public ICollection<LunchBox> LunchBoxes { get; set; }
    }
}
