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
    public class NewTransactionController : ApiController
    {
        ServerAuth server = new ServerAuth();
        // GET: api/NewTransaction
        public string Get(uint accID, uint reciverID, uint amount)
        {
            BankDBServerInterface foob = server.serverIMPL();
            int result = foob.NewTransaction(accID, reciverID, amount);
            if(result == -100)
            {
                return "Unable To create Transaction";
            }
            else if(result == -200)
            {
                return "Reciver Does not Exist please try again";
            }
            else if(result == -150)
            {
                return "Cannot make fund transfer Insufficent funds in account";
            }
            else
            {
                return "Successfull" ;
            }
        }
    }
}
