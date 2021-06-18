using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JYOTIRGAMAYA.Repository;
using JYOTIRGAMAYA.Models;
using System.Threading.Tasks;
namespace JYOTIRGAMAYA.Controllers
{
    public class LeaderBoardController : Controller
    {
        //
        // GET: /LeaderBoard/
        [ActionName("3159AE1E1CA2312DFFE7283F3EB3B")]
        public async Task<ActionResult> Index()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserRepository obj = new UserRepository();
                    LeaderBoardViewModel leaderobj = new LeaderBoardViewModel();
                    IEnumerable<UserViewModel> objlist = new List<UserViewModel>();
                    leaderobj.listuser = await obj.getAllUser();
                    return View(leaderobj);

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
