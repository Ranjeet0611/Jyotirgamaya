using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;
using JYOTIRGAMAYA.Repository.contract;
using JYOTIRGAMAYA.Models;

namespace JYOTIRGAMAYA.Repository
{
    public class UserRepository : IUserRepository
    {

        SqlConnection DB;
        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string validateUser(LoginViewModel model)
        {
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("validateuser", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    DB.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            int UserID= Convert.ToInt32(sdr["UserID"].ToString());
                            model.UserID = UserID;
                           
                        }

                    }
                    string result = cmd.ExecuteScalar().ToString();
                    DB.Close();
                    return result;

                }
            }
        }
        public int registerUser(RegisterViewModel model)
        {

            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RegisterUser", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@CreatedBy", model.UserID);
                    cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    cmd.Parameters.AddWithValue("@IP_Address", model.Ip);
                    cmd.Parameters.AddWithValue("@Active", 1);
                    cmd.Parameters.AddWithValue("@City",model.City);
                    cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                    cmd.Parameters.AddWithValue("@BrowserName", model.BrowserName);
                    cmd.Parameters.AddWithValue("@BrowserType", model.BrowserType);
                    cmd.Parameters.AddWithValue("@BrowserVersion", model.BrowserVersion);
                    cmd.Parameters.AddWithValue("@BrowserPlatform", model.BrowserPlatform);
                    cmd.Parameters.AddWithValue("@SecurityName", model.SecurityName);
                    DB.Open();
                    int result = cmd.ExecuteNonQuery();
                    DB.Close();
                    return result;
                }
            }


        }
        public Task<IEnumerable<UserViewModel>> getUserData(int UserID)
        {
            List<UserViewModel> objlist = new List<UserViewModel>();
            UserViewModel model = new UserViewModel();
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GETUSERDATA", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            model.FirstName = sdr["FirstName"].ToString();
                            model.LastName = sdr["LastName"].ToString();
                            model.Email = sdr["Email"].ToString();
                            model.MobileNumber = sdr["MobileNumber"].ToString();
                            model.Photo = sdr["Photo"].ToString();
                            model.City = sdr["City"].ToString();
                            model.Pincode = Convert.ToInt32(sdr["Pincode"].ToString());
                            model.Password = sdr["Password"].ToString();
                            objlist.Add(model);

                        }
                        DB.Close();
                    }

                }
            }
            return Task.FromResult<IEnumerable<UserViewModel>>(objlist.ToList());
        }
        public Task<IEnumerable<UserViewModel>> getAllUser()
        {
            IList<UserViewModel> objlist = new List<UserViewModel>();

            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GETALLUSERS", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            UserViewModel model = new UserViewModel();
                            model.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                            model.FirstName = sdr["FirstName"].ToString();
                            model.LastName = sdr["LastName"].ToString();
                            model.Email = sdr["Email"].ToString();
                            model.MobileNumber = sdr["MobileNumber"].ToString();
                            model.Point = sdr["Points"].ToString();
                            //model.Photo = sdr["Photo"].ToString();
                            model.Pincode = Convert.ToInt32(sdr["Pincode"].ToString());
                            model.City = sdr["City"].ToString();
                            
                            
                            objlist.Add(model);
                        }
                        DB.Close();

                    }

                }
            }
            return Task.FromResult<IEnumerable<UserViewModel>>(objlist);
        }
        public int registercomplaint(ComplaintModel model, string fullpath, int UserID)
        {
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("REGISTERCOMPLAINT", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@ComplaintType", model.ComplaintType);
                    cmd.Parameters.AddWithValue("@ComplaintDescription", model.ComplaintDescription);
                    cmd.Parameters.AddWithValue("@ComplaintPhoto", fullpath);
                    DB.Open();
                    int result = cmd.ExecuteNonQuery();
                    DB.Close();
                    return result;
                }
            }
        }
        public Task<IEnumerable<ComplaintModel>> getUsersComplaint()
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
                            obj.Email = sdr["Email"].ToString();
                            obj.MobileNumber = sdr["MobileNumber"].ToString();
                            //obj.ComplaintPhoto = sdr["ComplaintPhoto"].ToString();
                            //obj.Photo = sdr["Photo"].ToString();
                            obj.ComplaintID = Convert.ToInt32(sdr["ComplaintID"].ToString());
                            obj.getComplaintType = sdr["ComplaintType"].ToString();
                            obj.City = sdr["City"].ToString();
                            obj.Pincode = Convert.ToInt32(sdr["Pincode"].ToString());
                            obj.RegisteredDate = sdr["RegisterDate"].ToString();
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


        }
        public int registerEvent(EventViewModel model, int UserID)
        {
            int result;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("REGISTEREVENT",DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID",UserID);
                    cmd.Parameters.AddWithValue("@EventName", model.EventName);
                    cmd.Parameters.AddWithValue("@EventDescription", model.EventDescription);
                    cmd.Parameters.AddWithValue("@EventDate", model.EventDate);
                    DB.Open();
                    result = cmd.ExecuteNonQuery();
                    DB.Close();
                    
                }
            }
            return result;
        }
        public UserViewModel getProfileData(int UserID)
        {
            UserViewModel model = new UserViewModel();
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EDITPROFILEDATA",DB))
                {
                    DB.Open();
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            model.FirstName = sdr["FirstName"].ToString();
                            model.LastName = sdr["LastName"].ToString();
                            model.Password = sdr["Password"].ToString();
                            model.MobileNumber = sdr["MobileNumber"].ToString();
                            model.Pincode = Convert.ToInt32(sdr["Pincode"].ToString());
                            model.City = sdr["City"].ToString();
                            model.Email = sdr["Email"].ToString();
                            model.SecurityName = sdr["SecurityName"].ToString();
                        }
                    }
                }
            }
            return model;
        }
        public int editUserProfile(UserViewModel model, int UserID)
        {
            int result;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EDITPROFILE",DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                    cmd.Parameters.AddWithValue("@BrowserName", model.BrowserName);
                    cmd.Parameters.AddWithValue("@BrowserType", model.BrowserType);
                    cmd.Parameters.AddWithValue("@BrowserVersion", model.BrowserVersion);
                    cmd.Parameters.AddWithValue("@BrowserPlatform", model.BrowserPlatform);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@SecurityName", model.SecurityName);
                    DB.Open();
                    result = cmd.ExecuteNonQuery();
                }
                DB.Close();
            }
            return result;
        }
        public int recoverPassword(RegisterViewModel model)
        {
            int result;
            using (DB = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RECOVERPASSWORD", DB))
                {
                    cmd.Connection = DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    DB.Open();
                    cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    cmd.Parameters.AddWithValue("@SecurityName", model.SecurityName);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        
    }
}
    
