using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
namespace JYOTIRGAMAYA.Controllers
{
    public class ToiletController : Controller
    {
        //
        // GET: /Toilet/
        [ActionName("61BC912658E6A4A13E35A47D2187E")]
        public ActionResult Index()
        {
            try
            {
                string markers = "[";
                string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection DB = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GET_TOILET", DB);
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

