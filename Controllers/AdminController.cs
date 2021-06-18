using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JYOTIRGAMAYA.Repository;
using System.Text;
using System.Web.Security;
using JYOTIRGAMAYA.Models;
using System.Threading.Tasks;
namespace JYOTIRGAMAYA.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [ActionName("17587DF33C6A745EFC7A17773839A")]
        public ActionResult Index()
        {
            return View();
        }
        int getAdminID()
        {
            try
            {
                AdminViewModel model = new AdminViewModel();
                var bytes = Convert.FromBase64String(Request.Cookies["AdminID"].Value);
                var output = MachineKey.Unprotect(bytes, "ProtectCookie");
                model.AdminID = Convert.ToInt32(Encoding.UTF8.GetString(output));
                return model.AdminID;
            }
            catch(Exception e)
            {
                return -1;
            }
            
        }
        [ActionName("A598EEBB5949D672DD38197CFB7F2")]
        public async Task<ActionResult> AdminDashboard()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int AdminID = getAdminID();
                    if (AdminID>0)
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
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
            }
            catch (Exception e)
            {
                return Content("Something Went Wrong " + e);
            }

            
        }
        [ActionName("6E43BF62D4FB37B1643DC1979B64B")]
        public async Task<ActionResult> RegisteredComplaints()
        {
            try
            {
                int AdminID = getAdminID();
                if (AdminID >0)
                {
                    UserRepository obj = new UserRepository();
                    ComplaintModel model = new ComplaintModel();
                    model.getComplaintList = await obj.getUsersComplaint();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
                
            }
            catch (Exception e)
            {
                return Content("Something Went Wrong " + e);
            }
        }
        public ActionResult Login(AdminViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminRepository obj = new AdminRepository();
                    string result = obj.validateAdmin(model);
                    if (obj != null)
                    {
                        if (result != null)
                        {
                            var cookie = Encoding.UTF8.GetBytes(model.AdminID.ToString());
                            var ecryptedcookie = Convert.ToBase64String(MachineKey.Protect(cookie, "ProtectCookie"));
                            HttpCookie cookieobj = new HttpCookie("AdminID", ecryptedcookie);
                            cookieobj.Expires.AddDays(10);
                            Response.Cookies.Add(cookieobj);
                            Session["AdminID"] = model.AdminID;
                            return RedirectToAction("A598EEBB5949D672DD38197CFB7F2", "Admin");
                        }
                        else
                        {
                            TempData["Message"]="Error1";
                            return RedirectToAction("17587DF33C6A745EFC7A17773839A", "Admin");
                        }
                    }
                    else
                    {
                       TempData["Message"]="Error2";
                       return RedirectToAction("17587DF33C6A745EFC7A17773839A", "Admin");
                    }
                }
                else{
                    TempData["Message"]="Error3";
                    return RedirectToAction("17587DF33C6A745EFC7A17773839A", "Admin");
                }
            }
            catch(Exception e)
            {
                TempData["Message"]="Error4";
                return RedirectToAction("Index","Admin");
            } 
           }
          
        public ActionResult DeleteComplaint(ComplaintModel model)
        {
            try
            {
                    int AdminID = getAdminID();
                    if (AdminID > 0)
                    {
                        AdminRepository obj = new AdminRepository();
                        int result = obj.deleteCompliant(model);
                        if (obj != null)
                        {
                            if (result > 0)
                            {
                                TempData["Message"] = "Success";
                                return RedirectToAction("6E43BF62D4FB37B1643DC1979B64B", "Admin");
                            }
                            else
                            {
                                TempData["Message"] = "Error1";
                                return RedirectToAction("6E43BF62D4FB37B1643DC1979B64B", "Admin");
                            }
                        }
                        else
                        {
                            TempData["Message"] = "Error2";
                            return RedirectToAction("6E43BF62D4FB37B1643DC1979B64B", "Admin");
                        }

                    }
                    else
                    {
                        return RedirectToAction("ErrorIndex", "Error");
                    }
                    
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error3";
                return RedirectToAction("6E43BF62D4FB37B1643DC1979B64B", "Admin");
            }
            
            
        }
        [ActionName("CBA1CE8D736952C9CB98DBA754AB7")]
        public async Task<ActionResult> RegisteredUser()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int AdminID = getAdminID();
                    if (AdminID > 0)
                    {
                        AdminRepository obj = new AdminRepository();
                        UserViewModel userobj = new UserViewModel();
                        //IEnumerable<UserViewModel> objlist = new List<UserViewModel>();
                        userobj.getUserList = await obj.getRegisteredUsers();
                        return View(userobj);
                    }
                    else
                    {
                        return RedirectToAction("ErrorIndex", "Error");
                    }
                    

                }
            }
            catch (Exception e)
            {
                return Content("Something Went Wrong " + e);
            }

            return Content("Something Went Wrong If you can fix this join us");
        }
        [ActionName("927F5816BA78F7BA5AE878C898A94")]
        public ActionResult RegisteredEvents()
        {
            try
            {
                int AdminID = getAdminID();
                if (AdminID > 0)
                {
                    AdminRepository obj = new AdminRepository();
                    EventViewModel model = new EventViewModel();
                    //IEnumerable<EventViewModel> objlist = new List<EventViewModel>();
                    model.getlist = obj.getRegisteredEvents();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
               
            }
            catch(Exception e)
            {
                return Content("Something Went Wrong " + e);
            }
        }
        public ActionResult DeleteUser(UserViewModel model)
        {
            try
            {
                int AdminID = getAdminID();
                if (AdminID > 0)
                {
                    AdminRepository obj = new AdminRepository();
                    int result = obj.deleteUser(model);
                    if (obj != null)
                    {
                        if (result > 0)
                        {
                            TempData["Message"] = "Success";
                            return RedirectToAction("CBA1CE8D736952C9CB98DBA754AB7", "Admin");
                        }
                        else
                        {
                            TempData["Message"] = "Error1";
                            return RedirectToAction("CBA1CE8D736952C9CB98DBA754AB7", "Admin");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Error2";
                        return RedirectToAction("CBA1CE8D736952C9CB98DBA754AB7", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
                
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error3";
                return RedirectToAction("CBA1CE8D736952C9CB98DBA754AB7", "Admin");
            }
        }
        public ActionResult DeleteEvent(EventViewModel model)
        {
            try
            {
                int AdminID = getAdminID();
                if (AdminID > 0)
                {
                    AdminRepository obj = new AdminRepository();
                    int result = obj.deleteevent(model);
                    if (obj != null)
                    {

                        if (result > 0)
                        {
                            TempData["Message"] = "Success";
                            return RedirectToAction("927F5816BA78F7BA5AE878C898A94", "Admin");
                        }
                        else
                        {
                            TempData["Message"] = "Error1";
                            return RedirectToAction("927F5816BA78F7BA5AE878C898A94", "Admin");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Error2";
                        return RedirectToAction("927F5816BA78F7BA5AE878C898A94", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
                
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("RegisteredEvents", "Admin");
            }
        }
        [ActionName("ADB43F65767AD8BA8D5C5DC12BC55")]
        public ActionResult DustbinForm()
        {
            int AdminID = getAdminID();
            if (AdminID > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ErrorIndex", "Error");
            }
            
        }
        [ActionName("6837AC9FB12AE88592DCA16BD165A")]
        public ActionResult ToiletForm()
        {
            int AdminID = getAdminID();
            if (AdminID > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ErrorIndex", "Error");
            }
        }
        public ActionResult SubmitDustbinForm(MapViewModel model)
        {
            try
            {
                int AdminID = getAdminID();
                if (AdminID > 0)
                {
                    AdminRepository obj = new AdminRepository();
                    int result = obj.submitDustbinData(model);
                    if (result > 0)
                    {
                        TempData["Message"] = "Success";
                        return RedirectToAction("ADB43F65767AD8BA8D5C5DC12BC55", "Admin");
                    }
                    else
                    {
                        TempData["Message"] = "Error1";
                        return RedirectToAction("ADB43F65767AD8BA8D5C5DC12BC55", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
                
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("DustbinForm", "Admin"); 
            }
        }
        public ActionResult SubmitToiletForm(MapViewModel model)
        {
            try
            {
                int AdminID = getAdminID();
                if (AdminID > 0)
                {
                    AdminRepository obj = new AdminRepository();
                    int result = obj.submitToiletData(model);
                    if (result > 0)
                    {
                        TempData["Message"] = "Success";
                        return RedirectToAction("6837AC9FB12AE88592DCA16BD165A", "Admin");
                    }
                    else
                    {
                        TempData["Message"] = "Error1";
                        return RedirectToAction("6837AC9FB12AE88592DCA16BD165A", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
                
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error2";
                return RedirectToAction("6837AC9FB12AE88592DCA16BD165A", "Admin");
            }
        }
        public ActionResult Logout()
        {
            if (Request.Cookies["AdminID"] != null)
            {
                HttpCookie cookie = new HttpCookie("AdminID");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
                Session["AdminID"] = null;
                return RedirectToAction("17587DF33C6A745EFC7A17773839A", "Admin");
            }
            else
            {
                return RedirectToAction("ErrorIndex", "Error");
            }
            
        }

    }
}
