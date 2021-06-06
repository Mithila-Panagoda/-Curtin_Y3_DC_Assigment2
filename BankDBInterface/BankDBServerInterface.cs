using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BankDBInterface
{
    [ServiceContract]
    public interface BankDBServerInterface
    {
        [OperationContract]
        uint registerUser(string fname, string lname);

        [OperationContract]
        int login(uint ID, string fname);

        [OperationContract]
        List<uint> getUsers();

        [OperationContract]
        int newAccount(uint Uid, uint amount);

        [OperationContract]
        int UpdateProfile(uint Uid, string fname, string lname);

        [OperationContract]
        string loadProfile(uint Uid);

        [OperationContract]
        string getUserAccountInfo(uint UID);

        [OperationContract]
        List<uint> getUserAccountIDs(uint UID);

        [OperationContract]
        int WithdrawMoney(uint UID,uint accID, uint Amount);

        [OperationContract]
        int DepositMoney(uint UID,uint accID, uint Amount);

        [OperationContract]
        int NewTransaction(uint accID, uint reciverID, uint amount);

        [OperationContract]
        string loadPendingUserTransactions(uint UID);
    }
}
