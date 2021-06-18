using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JYOTIRGAMAYA.Repository;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
namespace JYOTIRGAMAYA.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        [ActionName("4AFA9223CF9E7FB47C64CAD5383D5")]
        public ActionResult Index()
        {
            try
            {
                string markers = "[";
                string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection DB = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GETMAP", DB);
                    cmd.Connection = DB;
                    DB.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        markers += "{";
                        markers += string.Format("'title': '{0}',", sdr["CityName"]);
                        markers += string.Format("'lat': '{0}',", sdr["CityLatitude"]);
                        markers += string.Format("'lng': '{0}',", sdr["CityLongitude"]);
                        markers += string.Format("'icon': '{0}',", sdr["Icon"]);
                        markers += string.Format("'photo': '{0}',", sdr["Photo"]);
                        markers += string.Format("'description': '{0}'", sdr["CityDescription"]);
                        markers += "},";
                        markers = Regex.Replace(markers, @"\s", "");
                    }
                }
                markers += "];";
                ViewBag.Markers = markers;
                return View();
            }
            catch (Exception e)
            {
                return Content("<center><h1>Error While Loading Maps</h1></center>");
            }

            }
            

    }
}
