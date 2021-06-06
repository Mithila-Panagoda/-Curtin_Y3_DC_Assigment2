using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDB;
using System.ServiceModel;
using BankDBInterface;
namespace Data_Teir
{
    class Program
    {
        static void Main(string[] args)
        {
            BankDBServer bankDBServer = new BankDBServer();
            Console.WriteLine("Starting up server");
            var tcp = new NetTcpBinding();
            var host = new ServiceHost(typeof(BankDBServer));
            host.AddServiceEndpoint(typeof(BankDBServerInterface), tcp, "net.tcp://0.0.0.0:8100/BankServer");
            host.Open();
            Console.WriteLine("Server is online");
            Console.ReadLine();
            
        }
    }
}
