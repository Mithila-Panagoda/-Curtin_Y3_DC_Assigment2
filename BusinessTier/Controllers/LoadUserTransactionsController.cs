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
    public class LoadUserTransactionsController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/LoadUserTransactions
        public string Get(uint UID)
        {
            BankDBServerInterface foob = server.serverIMPL();
            string result = foob.loadPendingUserTransactions(UID);
            return result;
        }


    }
}
