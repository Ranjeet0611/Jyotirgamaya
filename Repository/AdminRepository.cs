using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JYOTIRGAMAYA.Repository.contract;
using JYOTIRGAMAYA.Models;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;
namespace JYOTIRGAMAYA.Repository
{
    public class AdminRepository:IAdminRepository
    {
        SqlConnection DB;
        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string validateAdmin(AdminViewModel model)
        {
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VALIDATEADMIN"))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminUsername", model.AdminUsername);
                    cmd.Parameters.AddWithValue("@AdminPassword", model.AdminPassword);
                    DB.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            model.AdminID = Convert.ToInt32(sdr["AdminID"].ToString());
                        }
                    }
                    string result = cmd.ExecuteScalar().ToString();
                    DB.Close();
                    return result;   
                }
            }
        }
        public int deleteCompliant(ComplaintModel model)
        {
            int result = 0;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETECOMPLIANT", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ComplaintID", model.ComplaintID);
                    DB.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public Task<IEnumerable<UserViewModel>> getRegisteredUsers()
        {
            IList<UserViewModel> objlist = new List<UserViewModel>();
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GETREGISTEREDUSERS", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while(sdr.Read())
                        {
                            UserViewModel model = new UserViewModel();
                            model.FirstName = sdr["FirstName"].ToString();
                            model.LastName = sdr["LastName"].ToString();
                            model.Email = sdr["Email"].ToString();
                            model.MobileNumber = sdr["MobileNumber"].ToString();
                            model.Password = sdr["Password"].ToString();
                            model.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                            model.Ip = sdr["IP_Address"].ToString();
                            model.BrowserName = sdr["BrowserName"].ToString();
                            model.BrowserType = sdr["BrowserType"].ToString();
                            model.BrowserVersion = sdr["BrowserVersion"].ToString();
                            model.BrowserPlatform = sdr["BrowserPlatform"].ToString();
                            model.City = sdr["City"].ToString();
                            model.Pincode = Convert.ToInt32(sdr["Pincode"].ToString());
                            model.SecurityName = sdr["SecurityName"].ToString();
                            objlist.Add(model);
                        }
                    }
                }
            }
            return Task.FromResult<IEnumerable<UserViewModel>>(objlist);
            DB.Close();
        }
        public int deleteUser(UserViewModel model)
        {
            int result = 0;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETEUSERS",DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    result = cmd.ExecuteNonQuery();

                }
                DB.Close();
            }
            return result;
        }
        public IEnumerable<EventViewModel> getRegisteredEvents()
        {
            IList<EventViewModel> objlist = new List<EventViewModel>();
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GETREGISTEREDEVENTS",DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EventViewModel model = new EventViewModel();
                            model.FirstName = sdr["FirstName"].ToString();
                            model.LastName = sdr["LastName"].ToString();
                            model.MobileNumber = sdr["MobileNumber"].ToString();
                            model.Email = sdr["Email"].ToString();
                            model.EventID = Convert.ToInt32(sdr["EventID"].ToString());
                            model.EventDescription = sdr["EventDescription"].ToString();
                            model.EventName = sdr["EventName"].ToString();
                            model.EventDate = sdr["EventDate"].ToString();
                            model.CreatedOnDate = sdr["CreatedOn"].ToString();
                            objlist.Add(model);
                        }
                    }
                }
            }
            return objlist;
        }
        public int deleteevent(EventViewModel model)
        {
            int result = 0;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETEEVENT",DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EventID",model.EventID);
                    DB.Open();
                    result = cmd.ExecuteNonQuery();

                }
                DB.Close();
            }
            return result;
        }
        public int submitDustbinData(MapViewModel model)
        {
            int result=0;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DUSTBINFORM", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    cmd.Parameters.AddWithValue("@CityName",model.CityName);
                    cmd.Parameters.AddWithValue("@CityLatitude", model.CityLatitude);
                    cmd.Parameters.AddWithValue("@CityLongitude", model.CityLongitude);
                    cmd.Parameters.AddWithValue("@CityDescription", model.CityDescription);
                    cmd.Parameters.AddWithValue("@Icon",model.Icon);
                    result = cmd.ExecuteNonQuery();
                }
            }
            DB.Close();
            return result;
        }
        public int submitToiletData(MapViewModel model)
        {
            int result = 0;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TOILETFORM", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    cmd.Parameters.AddWithValue("@CityName", model.CityName);
                    cmd.Parameters.AddWithValue("@CityLatitude", model.CityLatitude);
                    cmd.Parameters.AddWithValue("@CityLongitude", model.CityLongitude);
                    cmd.Parameters.AddWithValue("@CityDescription", model.CityDescription);
                    cmd.Parameters.AddWithValue("@Icon", model.Icon);
                    result = cmd.ExecuteNonQuery();
                }
            }
            DB.Close();
            return result;
        }
       
       /* public Task<IEnumerable<ComplaintModel>> getUsersComplaint()
        {
            IList<ComplaintModel> objlist = new List<ComplaintModel>();

            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GETUSERCOMPLAINTS", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ComplaintModel obj = new ComplaintModel();
                            obj.FirstName = sdr["FirstName"].ToString();
                            obj.LastName = sdr["LastName"].ToString();
                            obj.Photo = sdr["Photo"].ToString();
                            obj.ComplaintID = Convert.ToInt32(sdr["ComplaintID"].ToString());
                            obj.getComplaintType = sdr["ComplaintType"].ToString();
                            if (Convert.ToInt32(obj.getComplaintType) == 1)
                            {
                                obj.getComplaintType = "Dutbins";
                            }
                            else
                            {
                                obj.getComplaintType = "Toilet";
                            }
                            obj.ComplaintDescription = sdr["ComplaintDescription"].ToString();
                            obj.ComplaintPhoto = sdr["ComplaintPhoto"].ToString();
                            objlist.Add(obj);

                        }

                    }
                }
            }
            return Task.FromResult<IEnumerable<ComplaintModel>>(objlist);


        }*/
    }
}