using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using JYOTIRGAMAYA.Repository.contract;
using System.Configuration;
namespace JYOTIRGAMAYA.Repository
{
    /*public class MapRepository:IMapRepository
    {
        SqlConnection DB;
        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string getMap()
        {
            string markers = "[";
            SqlCommand cmd = new SqlCommand("GETMAP");
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        markers += "{";
                        markers += string.Format("'title': '{0}',", sdr["CityName"]);
                        markers += string.Format("'lat': '{0}',", sdr["CityLatitude"]);
                        markers += string.Format("'lng': '{0}',", sdr["CityLongitude"]);
                        markers += string.Format("'description': '{0}'", sdr["CityDescription"]);
                        markers += "},";
                    }
                }
                con.Close();
            }

            markers += "];"; 
            return markers;

        }
    }*/
}