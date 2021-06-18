using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JYOTIRGAMAYA.Models
{
    public class AdminViewModel
    {
        public int AdminID { get; set; }

        [Required]
        public string AdminUsername { get; set; }

        [Required]
        public string AdminPassword { get; set; }
    }
}