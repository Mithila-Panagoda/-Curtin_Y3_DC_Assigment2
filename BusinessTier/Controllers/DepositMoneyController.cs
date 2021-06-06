using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessTier.Models;
using BankDBInterface;

namespace BusinessTier.Controllers
{
    public class DepositMoneyController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/DepositMoney
        public int Get(uint UID,uint accID,uint amount)
        {
            BankDBServerInterface foob = server.serverIMPL();
            int result = foob.DepositMoney(UID, accID, amount);
            /*
             * Return / ErrorCodes :-
             * --------------------------------------
             * return 0 : Deposit successfull
             * return -100 : Deposit failed
             * return -200 : Account does not exist
             */
            return result;
        }
    }
}
