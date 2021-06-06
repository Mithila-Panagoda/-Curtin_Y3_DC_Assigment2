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
    public class GetUserAccountIDsController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/GetUserAccountIDs
        public List<uint> Get(uint UID)
        {
            BankDBServerInterface foob = server.serverIMPL();
            List<uint> data = foob.getUserAccountIDs(UID);
            return data;
        }

    }
}
