using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JYOTIRGAMAYA.Models
{
    public class UserViewModel:AdminViewModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public bool? Active { get; set; }
        public int CreatedBy { get; set; }
        public string SubmittedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Point { get; set; }
        public string Photo { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public string BrowserName { get; set; }
        public string BrowserType { get; set; }
        public string BrowserVersion { get; set; }
        public string BrowserPlatform { get; set; }
        public string Ip { get; set; }
        public string CreatedOnDate { get; set; }
        public string SecurityName { get; set; }
        public IEnumerable<UserViewModel> getUserList { get; set; }


        
        

    }
}