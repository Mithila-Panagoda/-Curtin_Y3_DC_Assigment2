using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Teir
{
    class UserAccount
    {
        public UserAccount(uint accountID, uint userID, uint balance)
        {
            AccountID = accountID;
            UserID = userID;
            Balance = balance;
        }

        public uint AccountID { get; set; }
        public uint UserID { get; set; }
        public uint Balance { get; set; }
    }
}
