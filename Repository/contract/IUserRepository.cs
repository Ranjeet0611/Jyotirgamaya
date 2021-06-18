using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using JYOTIRGAMAYA.Models;
namespace JYOTIRGAMAYA.Repository.contract
{
    public interface IUserRepository
    {
         string validateUser(LoginViewModel model);
         int registerUser(RegisterViewModel model);
         Task<IEnumerable<UserViewModel>> getUserData(int UserID);
         Task<IEnumerable<UserViewModel>> getAllUser();
         Task<IEnumerable<ComplaintModel>> getUsersComplaint();
         int registercomplaint(ComplaintModel model, string fullpath,int UserID);
         int registerEvent(EventViewModel model, int UserID);
         int editUserProfile(UserViewModel model,int UserID);
         UserViewModel getProfileData(int UserID);
         int recoverPassword(RegisterViewModel model);
    }
}