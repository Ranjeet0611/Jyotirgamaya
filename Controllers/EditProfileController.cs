using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using JYOTIRGAMAYA.Models;
using JYOTIRGAMAYA.Repository;
namespace JYOTIRGAMAYA.Controllers
{
    public class EditProfileController : Controller
    {
        //
        // GET: /EditProfile/
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
            catch(Exception e)
            {
                return -1;
            }
           
        }
        [ActionName("6E39DB9F7DBA2585626A99F4EDEDC")]
        public ActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            try
            {
                int UserID = getUserID();
                if (UserID >0)
                {
                    UserRepository obj = new UserRepository();
                    model = obj.getProfileData(UserID);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ErrorIndex", "Error");
            }


        }
        public ActionResult Edit(UserViewModel model)
        {
            try
            {
                HttpBrowserCapabilitiesBase browser = HttpContext.Request.Browser;
                model.BrowserName = browser.Browser;
                model.BrowserType = browser.Type;
                model.BrowserVersion = browser.Version;
                model.BrowserPlatform = browser.Platform;
                int UserID = getUserID();
                UserRepository obj = new UserRepository();
                int result = obj.editUserProfile(model, UserID);
                if (result > 0)
                {
                    TempData["Message"] = "Success";
                    return RedirectToAction("6E39DB9F7DBA2585626A99F4EDEDC", "EditProfile");

                }
                else
                {
                    TempData["Message"] = "Error1";
                    return RedirectToAction("6E39DB9F7DBA2585626A99F4EDEDC", "EditProfile");
                }
            }
            catch(Exception e)
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("6E39DB9F7DBA2585626A99F4EDEDC", "EditProfile");

            }
            
        }

    }
}
