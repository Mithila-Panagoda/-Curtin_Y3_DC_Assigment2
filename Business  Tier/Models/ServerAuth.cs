using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankDBInterface;
using System.ServiceModel;
namespace BusinessTier.Models
{
    public class ServerAuth
    {
        public BankDBServerInterface serverIMPL()
        {
            BankDBServerInterface foob;
            var tcp = new NetTcpBinding();
            var url = "net.tcp://localhost:8100/BankServer";
            var chanFactory = new ChannelFactory<BankDBServerInterface>(tcp, url);
            foob = chanFactory.CreateChannel();
            return foob;
        }
    }
}