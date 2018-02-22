using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kimlik.Models.IdentityModels;

namespace Kimlik.Models.Entities
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public DateTime MessageDate { get; set; } = DateTime.Now;
        public string Content { get; set; }
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual  ApplicationUser Sender { get; set; }
    }
}
