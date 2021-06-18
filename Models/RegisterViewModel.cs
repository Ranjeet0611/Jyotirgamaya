using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JYOTIRGAMAYA.Models
{
    public class RegisterViewModel
    {
        [Required]
        public int UserID { get; set; }

        [Required(ErrorMessage="Enter your FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Enter your LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "Password length between 5 - 10.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage="Enter your Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength:10, ErrorMessage="Mobile Number is invalid", MinimumLength=10)]
        public string MobileNumber { get; set; }

        public string Ip { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public HttpPostedFileBase PhotoUpload { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Pincode { get; set; }

        public string BrowserName { get; set; }
        public string BrowserType { get; set; }
        public string BrowserVersion { get; set; }
        public string BrowserPlatform { get; set; }
        public string SecurityName { get; set; }

    }
}