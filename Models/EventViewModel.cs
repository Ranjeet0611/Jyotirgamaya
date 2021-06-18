using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JYOTIRGAMAYA.Models
{
    public class EventViewModel:UserViewModel
    {
        [Required]
        public int EventID { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventDescription { get; set; }

        [Required]
        public string EventDate { get; set; }

        public IEnumerable<EventViewModel> getlist { get; set; }
    }
}