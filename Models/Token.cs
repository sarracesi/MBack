using System;
using System.ComponentModel.DataAnnotations;

namespace Mobilis.Models
{
   
    public class Token
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TokenValue { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        
        public int UserId { get; set; }
        
    }

   
}
