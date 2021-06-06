using BankDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDBInterface;
using Newtonsoft.Json;
using System.ServiceModel;
namespace Data_Teir
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    class BankDBServer : BankDBServerInterface
    {
        BankDB.BankDB bankDB = new BankDB.BankDB();
        UserAccessInterface userAccess;
        TransactionAccessInterface transactionAccess;
        AccountAccessInterface accountAccess;
        public int DepositMoney(uint UID,uint accID, uint Amount)
        {
            /*
             * Return / ErrorCodes :-
             * return 0 : Withdrawal successfull
             * return -100 : Withdrawal failed
             * return -200 : Account does not exist
             */
            accountAccess = bankDB.GetAccountInterface();
            try
            {
                accountAccess.SelectAccount(accID);
            }
            catch
            {
                return -200;//Account does not exisit
            }
            uint userID = accountAccess.GetOwner();
            if (userID == UID)
            {
                accountAccess.Deposit(Amount);
                bankDB.SaveToDisk();
                return 0;
            }
            else
            {
                return -100;//Deposit has failed
            }
        }
        public int WithdrawMoney(uint UID, uint accID, uint Amount)
        {
            /*
             * Return / ErrorCodes :-
             * ----------------------------------------------------
             * return 0 : Withdrawal successfull
             * return -100 : account does not exisit
             * return -150 : Insufficent balance  after withdrwal
             * return -200 : Insufficent funds to make withdrawal
             * return -250 : User/owner ID mismatch
             */
            accountAccess = bankDB.GetAccountInterface();
            try
            {
                accountAccess.SelectAccount(accID);
            }
            catch
            {
                return -100; //account does not exisit
            }
            uint userID = accountAccess.GetOwner();
            if (userID == UID)
            {
                if (accountAccess.GetBalance() > 1000) //sufficent funds to do withdrawal
                {
                    uint balance = accountAccess.GetBalance();
                    if(balance - Amount < 1000) //Minimum balance of Rs1000/= should be maintained in account
                    {
                        return -150; // Insufficent balance  after withdrwal
                    }
                    else
                    {
                        try
                        {
                            accountAccess.Withdraw(Amount);
                        }
                        catch
                        {
                            return -200; //Insufficent funds to make withdrawal
                        }
                        bankDB.SaveToDisk();
                        return 0; //successfull withdrawal!!
                    }
                }
                else
                {
                    return -200; //Insufficent funds to make withdrawal
                }
            }
            else
            {
                return -250; // User/owner ID mismatch
            }
        }
        public string getUserAccountInfo(uint UID)
        {
            UserAccount userAccount;
            accountAccess = bankDB.GetAccountInterface();
            List<uint> records = accountAccess.GetAccountIDsByUser(UID);
            List<UserAccount> accountinfo = new List<UserAccount>();
            foreach(uint record in records) {
                accountAccess.SelectAccount(record);
                uint balance = accountAccess.GetBalance();
                userAccount = new UserAccount(record,UID,balance);
                accountinfo.Add(userAccount);
            }
            string response = JsonConvert.SerializeObject(accountinfo);
            return response;
        }

        public List<uint> getUsers()
        {
            throw new NotImplementedException();
        }

        public int login(uint ID, string fname)
        {
            string username,lname;
            try
            {
                userAccess = bankDB.GetUserAccess();
                userAccess.SelectUser(ID);
                userAccess.GetUserName(out username,out lname);
                Console.WriteLine("UserName: " + username);
            }
            catch
            {
                return -100;
            }
            if (fname.Equals(username))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int newAccount(uint Uid, uint amount)
        {
            uint AccID;
            accountAccess = bankDB.GetAccountInterface();
            try
            {
                AccID = accountAccess.CreateAccount(Uid);
            }
            catch
            {
                return -500;
            }
            accountAccess.SelectAccount(AccID);
            accountAccess.Deposit(amount);
            bankDB.SaveToDisk();
            return (int) AccID;
        }

        public uint registerUser(string fname, string lname)
        {
            userAccess = bankDB.GetUserAccess();
            uint id=userAccess.CreateUser();
            userAccess.SelectUser(id);
            userAccess.SetUserName(fname, lname);
            bankDB.SaveToDisk();
            return id;
        }

        public int UpdateProfile(uint UID, string fname, string lname)
        {
            userAccess = bankDB.GetUserAccess();
            try
            {
                userAccess.SelectUser(UID);
                userAccess.SetUserName(fname, lname);
            }
            catch
            {
                return -100;
            }
            
            bankDB.SaveToDisk();
            return 0;
        }
        public string loadProfile(uint Uid)
        {
            string fname, lname;
            userAccess = bankDB.GetUserAccess();
            try
            {
                userAccess.SelectUser(Uid);
            }
            catch
            {
                return "failed";
            }
            userAccess.GetUserName(out fname, out lname);
            User user = new User(Uid, fname, lname);
            string response = JsonConvert.SerializeObject(user);
            return response;
        }

        public List<uint> getUserAccountIDs(uint UID)
        {
            accountAccess = bankDB.GetAccountInterface();
            List<uint> data = new List<uint>();
            uint error = 0;
            try
            {
                data = accountAccess.GetAccountIDsByUser(UID);
            }
            catch
            {
                data.Add(error);
                return data;
            }
            return data;
        }
        public int NewTransaction(uint accID, uint reciverID, uint amount)
        {
            uint TID = 0;
            transactionAccess = bankDB.GetTransactionInterface();
            accountAccess = bankDB.GetAccountInterface();
            try
            {
                 TID = transactionAccess.CreateTransaction();

            }
            catch
            {
                return -100;
            }
            transactionAccess.SelectTransaction(TID);
            try
            {
                transactionAccess.SetSendr(accID);
                accountAccess.SelectAccount(reciverID);
                uint reciverUID = accountAccess.GetOwner();
                if (reciverUID > 0)
                {
                    accountAccess.SelectAccount(accID);
                    uint senderAcBalance=accountAccess.GetBalance();
                    if (senderAcBalance < amount)
                    {
                        return -150;
                    }
                    transactionAccess.SetRecvr(reciverID);
                    transactionAccess.SetAmount(amount);
                }
                else
                {
                    return -200;
                }
                
            }
            catch
            {
                return -200;
            }
            return (int) TID;
        }
        public string loadPendingUserTransactions(uint UID)
        {
            transactionAccess = bankDB.GetTransactionInterface();
            accountAccess = bankDB.GetAccountInterface();

            List<uint> transactions = transactionAccess.GetTransactions();
            List<uint> UserAccounts = accountAccess.GetAccountIDsByUser(UID);
            List<Transaction> transactionInfo = new List<Transaction>();
            Transaction info;
            foreach (uint transaction in transactions)
            {
                Console.WriteLine("transactions ID : ", transaction);
                transactionAccess.SelectTransaction(transaction);
                foreach(uint account in UserAccounts)
                {
                    if (account == transactionAccess.GetSendrAcct())
                    {
                        Console.WriteLine("account : " + account + "sender account : " + transaction);
                        info = new Transaction(transaction, transactionAccess.GetSendrAcct(), transactionAccess.GetRecvrAcct(), transactionAccess.GetAmount());
                        transactionInfo.Add(info);
                    }
                }
            }
            string data = JsonConvert.SerializeObject(transactionInfo);
            return data;
        }
    }
}