using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankDBInterface;
using BusinessTier.Models;

namespace BusinessTier.Controllers
{
    public class LoadUserAccountsController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/LoadUserAccounts
        public List<string> Get(uint UID)
        {
            BankDBServerInterface foob = server.serverIMPL();
            string result = foob.getUserAccountInfo(UID);
            List<string> data = new List<string>();
            data.Add(result);
            if (result.Length > 0)
            {
                return data;
            }
            else
            {
                return data;
            }
        }

    }
}
