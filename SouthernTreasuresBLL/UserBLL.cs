using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SouthernTreasuresDAL;

namespace SouthernTreasuresBLL
{
    public class UserBLL
    {
        public UserBLL(UserDAL userDAL)
        {
            UserID = userDAL.UserID;
            Name = userDAL.Name;
            Password = userDAL.Password;
            Role = userDAL.Role;
            EmailAddress = userDAL.EmailAddress;
            CreditInfo = userDAL.CreditInfo;
        }
        public UserBLL()
        {

        }

        public override string ToString()
        {
            return $"UserID:{UserID} Name:{Name} Password:{Password} Role:{Role} EmailAddress:{EmailAddress} CreditInfo:{CreditInfo}";
        }
        
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public string CreditInfo { get; set; }

        private List<UserBLL> _user=null;
        public List<UserBLL> User
        {
            get
            {
                if (_user == null)
                {
                    throw new Exception("No User here");
                }
                return _user;
            }
        }
    }
}
