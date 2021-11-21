using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class LunchBox
    {
        public int Id { get; set; }         //blir automatiskt en [Key] om du har en int som heter Id eller int som heter classnamn+id
        [MaxLength(40)] [Required]
        public string DishName { get; set; }
        [MaxLength(10)] [Required]
        public string DishType { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime ConsumeBefore { get; set; }
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }
        //public int CustomerId { get; set; }


        public Restaurant Restaurant { get; set; }
        public ItemSale ItemSale { get; set; }  


    }
}
