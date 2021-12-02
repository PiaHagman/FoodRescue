using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class User
    {
        public int Id { get; set; } //blir automatiskt en[Key] om du har en int som heter Id eller int som heter classnamn+id
        [Column(TypeName = "date")]
        public DateTime DateRegistered { get; set; }
        public bool Admin { get; set; }
        public ICollection<ItemSale> ItemSales { get; set; }
        public UserPersonalInfo PersonalInfo { get; set; }
    }
}
