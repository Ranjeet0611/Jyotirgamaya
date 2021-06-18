using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JYOTIRGAMAYA.Models
{
    public class ComplaintModel:UserViewModel
    {
        [Required]
        public int ConfirmComplaintID { get; set; }

        public int ComplaintID { get; set; }
        [Required]
        public Complaint ComplaintType { get; set; }
        [Required]
        public string ComplaintDescription { get; set; }

        [Required]
        public string ComplaintPhoto { get; set; }

        [Required]
        public HttpPostedFileBase ComplaintPhotoUpload { get; set; }

        public string getComplaintType { get; set; }

        public enum Complaint
        {
            Toilet,
            Dustbins
        }
        public IEnumerable<ComplaintModel> getComplaintList { get; set; }
        public string RegisteredDate { get; set; }
    }
}