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
    public class NewAccountController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/NewAccount
        public int Get(uint Uid,uint amount)
        {
            BankDBServerInterface foob = server.serverIMPL();
            int result = foob.newAccount(Uid, amount);
            if (result > 0)
            {
                return result;
            }
            else
            {
                return -500;
            }
        }

    }
}
