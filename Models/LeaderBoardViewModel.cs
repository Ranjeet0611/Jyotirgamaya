using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JYOTIRGAMAYA.Models
{
    public class LeaderBoardViewModel:AdminViewModel
    {
        public LeaderBoardViewModel()
        {
            obj = new UserViewModel();
            listuser = new List<UserViewModel>();
        }
        public UserViewModel obj { get; set; }
        public IEnumerable<UserViewModel> listuser { get; set; }

        
    }
}