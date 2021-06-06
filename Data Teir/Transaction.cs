using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Teir
{
    class Transaction
    {
        public Transaction(uint tID, uint senderID, uint reciverID, uint amount)
        {
            TID = tID;
            SenderID = senderID;
            ReciverID = reciverID;
            Amount = amount;
        }

        public uint TID { get; set; }
        public uint SenderID { get; set; }
        public uint ReciverID { get; set; }
        public uint Amount { get; set; }
    }
}
