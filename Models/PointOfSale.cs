using System;
using System.ComponentModel.DataAnnotations;

namespace Mobilis.Models
{

    public class PointOfSale
    {
        public Guid Id { get; set; }
        public string localisation { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public TimeSpan opening { get; set; }
        public TimeSpan closing { get; set; }
    }
   

}
