using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthernTreasuresDAL
{
    public class UserDAL
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public string CreditInfo { get; set; }

        //ToString converts an object to its string representation so that it is suitable for display
        public override string ToString()
        {
            return $"UserID:{UserID} Name:{Name} Password:{Password} Role:{Role} EmailAddress:{EmailAddress} CreditInfo:{CreditInfo}";
        }
    }
}
