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
    public class UpdateProfileController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/UpdateProfile
        public int Get(uint UID,string fname,string lname)
        {
            BankDBServerInterface foob = server.serverIMPL();
            int result = foob.UpdateProfile(UID, fname, lname);
            return result;
        }
    }
}
