using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JYOTIRGAMAYA.Models;
using System.Threading.Tasks;
namespace JYOTIRGAMAYA.Repository.contract
{
    public interface IAdminRepository
    {
        string validateAdmin(AdminViewModel model);
        int deleteCompliant(ComplaintModel model);
        Task<IEnumerable<UserViewModel>> getRegisteredUsers();
        int deleteUser(UserViewModel model);
        IEnumerable<EventViewModel> getRegisteredEvents();
        int deleteevent(EventViewModel model);
        int submitDustbinData(MapViewModel model);
        int submitToiletData(MapViewModel model);
        //Task<IEnumerable<ComplaintModel>> getUsersComplaint();
    }
}