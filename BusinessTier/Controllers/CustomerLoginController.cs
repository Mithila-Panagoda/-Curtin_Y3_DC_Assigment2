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
    public class CustomerLoginController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/CustomerLogin
        public string Get(uint ID,string fname)
        {
            BankDBServerInterface foob = server.serverIMPL();
            int result = foob.login(ID, fname);
            /*
                -100 User does not exist
                  0 Login Successfull
                 -1 Name or ID incorrect!!
             */
            if (result == 0)
            {
                return "success";
            }
            else
            {
                return "fail";
            }
            
        }
    }
}
