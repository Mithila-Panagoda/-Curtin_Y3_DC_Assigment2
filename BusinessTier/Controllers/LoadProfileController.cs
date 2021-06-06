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
    public class LoadProfileController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/LoadProfile
        public List<string> Get(uint UID)
        {
            BankDBServerInterface foob = server.serverIMPL();
            List<string> data = new List<string>();
            data.Add(foob.loadProfile(UID));
            return data;
        }
    }
}
