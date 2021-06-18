using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JYOTIRGAMAYA.Models;
using System.Web.Security;
using System.Text;
using System.Threading.Tasks;
using JYOTIRGAMAYA.Repository;
namespace JYOTIRGAMAYA.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/
        public int getUserID()
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
        [ActionName("1CE35A53FAE11C3BD5A7CE347ADEA")]
        public async Task<ActionResult> Index()
        {
            try
            {
                int UserID = getUserID();

                if (UserID > 0)
                {
                    IEnumerable<UserViewModel> objlist = new List<UserViewModel>();
                    UserRepository obj = new UserRepository();
                    objlist = await obj.getUserData(UserID);
                    return View(objlist);
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

    }
}
