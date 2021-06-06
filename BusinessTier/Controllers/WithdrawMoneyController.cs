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
    public class WithdrawMoneyController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/WithdrawMoney
        public int Get(uint UID, uint accID, uint amount)
        {
            BankDBServerInterface foob = server.serverIMPL();
            int result = foob.WithdrawMoney(UID, accID, amount);
            /*
             * Return / ErrorCodes :-
             * ----------------------------------------------------
             * return 0 : Withdrawal successfull
             * return -100 : account does not exisit
             * return -150 : Insufficent balance  after withdrwal
             * return -200 : Insufficent funds to make withdrawal
             * return -250 : User/owner ID mismatch
             */
            return result;
        }
    }
}
