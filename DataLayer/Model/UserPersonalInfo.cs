using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class UserPersonalInfo
    {
        public int Id { get; set; }  //blir automatiskt en [Key] om du har en int som heter Id eller int som heter classnamn+id

        [MaxLength(40)]
        public string FullName { get; set; }
        [MaxLength(40)]
        [Required]
        public string Username { get; set; }
        [MaxLength(16)]
        [Required]
        public string Password { get; set; }
        [MaxLength(40)]
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "date")]


        [ForeignKey("Id")]
        [Required]
        public User User { get; set; }
    }
}
