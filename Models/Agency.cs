using System;
using System.ComponentModel.DataAnnotations;

namespace Mobilis.Models
{

    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string localisation { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime opening { get; set; }
        public DateTime closing { get; set; }
    }
   

}
