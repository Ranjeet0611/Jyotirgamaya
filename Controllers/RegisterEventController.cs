using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JYOTIRGAMAYA.Repository;
using JYOTIRGAMAYA.Models;
using System.Text;
namespace JYOTIRGAMAYA.Controllers
{
    public class RegisterEventController : Controller
    {
        //
        // GET: /RegisterEvent/
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
        [ActionName("3B53CC7F5BD6618D6AEB423619BA5")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterForm(EventViewModel model)
        {
            try
            {
                int UserID = getUserID();
            UserRepository obj=new UserRepository();
            if (UserID >0)
            {
                int result = obj.registerEvent(model, UserID);
                if (result > 0)
                {
                    TempData["Message"] = "Success";
                    return RedirectToAction("3B53CC7F5BD6618D6AEB423619BA5", "RegisterEvent");
                }
                else
                {
                    TempData["Message"] = "Error";
                    return RedirectToAction("3B53CC7F5BD6618D6AEB423619BA5", "RegisterEvent");
                }
            }
            else
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("3B53CC7F5BD6618D6AEB423619BA5", "RegisterEvent");
            }
            }
            catch(Exception e)
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("3B53CC7F5BD6618D6AEB423619BA5", "RegisterEvent");
            }
            
            

        }

    }
}
