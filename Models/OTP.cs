using System;
using System.ComponentModel.DataAnnotations;

namespace Mobilis.Models
{
    
    public class OTP
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public bool IsValid { get; set; }

        public int UserId { get; set; }
    }
}
