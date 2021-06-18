using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using JYOTIRGAMAYA.Models;
using JYOTIRGAMAYA.Repository;
using System.Web.Security;
using System.Text;
namespace JYOTIRGAMAYA.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
       [ActionName("38385DDEE93E5A6CDE6D75E8C8462")]
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("719427CA15E25D911DA4B4715F313")]
        public ActionResult UserRegister()
        {
            return View();
        }
        [ActionName("C777B919A149B1D249FE3F3AAB3E8")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                HttpBrowserCapabilitiesBase browser = HttpContext.Request.Browser;
                model.BrowserName = browser.Browser;
                model.BrowserType = browser.Type;
                model.BrowserVersion = browser.Version;
                model.BrowserPlatform = browser.Platform;
             //string filename="";
             //string fullpath = "";
             model.Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(model.Ip))
            {
                model.Ip = Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                String IP = model.Ip;
            }
            /*string path = Server.MapPath("~/Uploads");
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (model.PhotoUpload != null)
            {
                filename = Path.GetFileName(model.PhotoUpload.FileName);
                model.PhotoUpload.SaveAs(Path.Combine(path, filename));
                
            }*/
            UserRepository obj = new UserRepository();
            int result = obj.registerUser(model);
            if (result > 0)
            {
                ModelState.Clear();
                TempData["Message"] = "Success";
                return RedirectToAction("719427CA15E25D911DA4B4715F313", "Account");
            }
            else
            {
                TempData["Meesage"] = "Error";
                return RedirectToAction("719427CA15E25D911DA4B4715F313", "Account");
            }
            }
            catch(Exception e)
            {
                TempData["Meesage"] = "Error1";
                return RedirectToAction("719427CA15E25D911DA4B4715F313", "Account");
            }
            
        }
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                    UserRepository obj = new UserRepository();
                    string result = obj.validateUser(model);
                    if (obj != null)
                    {
                        if (result !=null)
                        {
                            var cookie = Encoding.UTF8.GetBytes(model.UserID.ToString());
                            var encrytedCookie = Convert.ToBase64String(MachineKey.Protect(cookie, "ProtectCookie"));
                            HttpCookie cookieobj = new HttpCookie("UserID", encrytedCookie);
                            cookieobj.Expires.AddDays(5);
                            Response.Cookies.Add(cookieobj);
                            Session["UserID"] = model.UserID;
                            return RedirectToAction("4AFA9223CF9E7FB47C64CAD5383D5", "Dashboard");
                        }
                        else
                        {
                            TempData["Message"] = "Error";
                            return RedirectToAction("38385DDEE93E5A6CDE6D75E8C8462", "Account");
                        }

                    }
                    else
                    {
                        TempData["Message"] = "Error";
                        return RedirectToAction("38385DDEE93E5A6CDE6D75E8C8462", "Account");
                    }
                
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("38385DDEE93E5A6CDE6D75E8C8462", "Account");
                
            }  
           
        }
        public ActionResult Logout()
        {
            try
            {
                if (Request.Cookies["UserID"] != null)
                {
                    HttpCookie cookie = new HttpCookie("UserID");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    Session["UserID"] = null;
                    return RedirectToAction("38385DDEE93E5A6CDE6D75E8C8462", "Account");
                }
                else
                {
                    return RedirectToAction("ErrorIndex", "Error");
                }
            }
            catch (Exception e)
            {
                return Content("<center><h1>Something went wrong We Will look into it.</h1></center>");
            }
            
        }
        public ActionResult RecoverPasswordForm(RegisterViewModel model)
        {
            try
            {
                UserRepository obj = new UserRepository();
                int result = obj.recoverPassword(model);
                if (result > 0)
                {
                    TempData["Message"] = "Success";
                    return RedirectToAction("C777B919A149B1D249FE3F3AAB3E8", "Account");
                }
                else
                {
                    TempData["Message"] = "Error";
                    return RedirectToAction("C777B919A149B1D249FE3F3AAB3E8", "Account");
                }
            }
            catch(Exception e)
            {
                TempData["Message"] = "Error1";
                return RedirectToAction("C777B919A149B1D249FE3F3AAB3E8", "Account");
            }
        }
        
    }
}
