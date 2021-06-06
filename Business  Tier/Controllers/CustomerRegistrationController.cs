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
    public class CustomerRegistrationController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/CustomerRegistration
        public uint Get(string fname,string lname)
        {
            BankDBServerInterface foob = server.serverIMPL();
            uint result = foob.registerUser(fname, lname);
            return result;
        }

    }
}
