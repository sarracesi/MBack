using System;
using System.ComponentModel.DataAnnotations;

namespace Mobilis.Models
{

    public class Notification
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string DeviceToken { get; set; }
       
    }
   

}
