using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JYOTIRGAMAYA.Models
{
    public class LoginViewModel
    {
        [Required]
        public int UserID { get; set; }

        [Required(ErrorMessage="Email Address is Required")]
        public string Email { get; set; }


        [Required(ErrorMessage="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool? Active { get; set; }

        

    }
}