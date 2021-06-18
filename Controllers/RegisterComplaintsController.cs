using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JYOTIRGAMAYA.Models;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Threading.Tasks;
using JYOTIRGAMAYA.Repository;
namespace JYOTIRGAMAYA.Controllers
{
    public class RegisterComplaintsController : Controller
    {
        //
        // GET: /RegisterComplaints/
        int getUserID()
        {
            try
            {
                UserViewModel model = new UserViewModel();
                var bytes = Convert.FromBase64String(Request.Cookies["UserID"].Value);
                var output = MachineKey.Unprotect(bytes, "ProtectCookie");
                model.UserID = Convert.ToInt32(Encoding.UTF8.GetString(output));
                return model.UserID;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        [ActionName("C54F1A25FFFA849BE5FDE3F7D726B")]
        public async Task<ActionResult> Index()
        {
            try
            {
                UserRepository obj = new UserRepository();
                ComplaintModel model = new ComplaintModel();
                model.getComplaintList= await obj.getUsersComplaint();
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("ErrorIndex", "Error");
            }
            
           
        }
        public ActionResult RegisterComplaint(ComplaintModel model)
        {
            try
            {
                string fullpath = "";
                string filename = "";
                int UserID = getUserID();
                string path = Server.MapPath("~/ComplaintPhoto");
                if (UserID >0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (model.ComplaintPhotoUpload != null)
                    {
                        filename = Path.GetFileName(model.ComplaintPhotoUpload.FileName);
                        model.ComplaintPhotoUpload.SaveAs(Path.Combine(path, filename));
                    }
                    UserRepository obj = new UserRepository();
                    int result = obj.registercomplaint(model, filename, UserID);
                    if (result > 0)
                    {
                        TempData["Message"] = "Success";
                        return RedirectToAction("C54F1A25FFFA849BE5FDE3F7D726B", "RegisterComplaints");
                    }
                    else
                    {
                        TempData["Message"] = "Error";
                        return RedirectToAction("C54F1A25FFFA849BE5FDE3F7D726B", "RegisterComplaints");
                    }


                }
                else
                {
                    TempData["Message"] = "Error2";
                    return RedirectToAction("C54F1A25FFFA849BE5FDE3F7D726B", "RegisterComplaints");
                }
               

            }
            catch(Exception e)
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("C54F1A25FFFA849BE5FDE3F7D726B", "RegisterComplaints");
            }
            
            
        }
        

    }
}
