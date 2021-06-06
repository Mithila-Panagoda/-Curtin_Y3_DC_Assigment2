using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;
using Newtonsoft.Json;
namespace Client_Side
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();
            loadDataGrid();
        }

        private async void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            int amount = int.Parse(txtInitAmt.Text);
            if(amount <= 1000)
            {
                MessageBox.Show("Please Deposit an amount greater than Rs1000/=");
            }
            else
            {
                User user = User.Instance;
                uint ID = user.ID;
                uint depositamt = (uint)amount;
                string url = @"https://localhost:44339/api/NewAccount/?Uid=" + ID + "&amount=" + depositamt;

                var client = new RestClient(url);
                var request = new RestRequest();
                var response =await Task.Run(() => client.Get(request));

                int result = int.Parse(response.Content.ToString());

                if (txtInitAmt.Text.Length == 0)
                {
                    MessageBox.Show("Please Enter deposit amount greater than Rs1000/= to make an account!!");
                }
                else
                {
                    if (result > 0)
                    {
                        MessageBox.Show("new Account Created Your account ID is : " + result);
                        loadDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Failed To create account please try again");
                        txtInitAmt.Text = "";
                    }
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtInitAmt.Text = "";
        }

        private async void loadDataGrid()
        {
            User user = User.Instance;
            string url = @"https://localhost:44339/api/LoadUserAccounts/?UID=" + user.ID;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = await Task.Run(()=> client.Get(request));
            string result = response.Content.ToString();
            List<string> data = JsonConvert.DeserializeObject<List<string>>(result);
            List<UserAccount> entity = new List<UserAccount>();
            if (data.Count == 0)
            {
                MessageBox.Show("User Dosnt have any accounts still.");
            }
            else
            {
                foreach (string record in data)
                {
                    entity = JsonConvert.DeserializeObject<List<UserAccount>>(record);
                }
                accountInfo.ItemsSource = entity;
            }
        }

        private void accountInfo_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserAccount userAccount = accountInfo.SelectedItem as UserAccount;
            txtAccID.Text = userAccount.AccountID.ToString();
            txtBalance.Text = userAccount.Balance.ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAccID.Text = "";
            txtBalance.Text = "";
            txtAmt.Text = "";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage();
            this.NavigationService.Navigate(home);
        }

        private async void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            string url = @"https://localhost:44339/api/DepositMoney/"+user.ID+"/"+txtAccID.Text+"/"+txtAmt.Text ;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = await Task.Run(()=> client.Get(request));
            /*
             * Return / ErrorCodes :-
             * --------------------------------------
             * return 0 : Deposit successfull
             * return -100 : Deposit failed
             * return -200 : Account does not exist
             */
            
            if (txtAccID.Text.Length == 0)
            {
                MessageBox.Show("Please Select an account from the list to perform Withdrawal or Deposit");
            }
            else
            {
                int result = int.Parse(response.Content.ToString());
                if (result == 0)
                {
                    MessageBox.Show("Money has been Deposited Successfully");
                    loadDataGrid();
                    txtAccID.Clear();
                    txtAmt.Clear();
                    txtBalance.Clear();
                }
                else if (result == -100)
                {
                    MessageBox.Show("Deposit Failed Please try again!!");
                }
                else if (result == -200)
                {
                    MessageBox.Show("Account does not exist please change account number");
                }
            }
        }

        private async void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            string url = @"https://localhost:44339/api/WithdrawMoney/" + user.ID + "/" + txtAccID.Text + "/" + txtAmt.Text;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = await Task.Run(()=> client.Get(request));
            /*
             * Return / ErrorCodes :-
             * ----------------------------------------------------
             * return 0 : Withdrawal successfull
             * return -100 : account does not exisit
             * return -150 : Insufficent balance  after withdrwal
             * return -200 : Insufficent funds to make withdrawal
             * return -250 : User/owner ID mismatch
             */
            

            if (txtAccID.Text.Length == 0)
            {
                MessageBox.Show("Please Select an account from the list to perform Withdrawal or Deposit");
            }
            else
            {
                int result = int.Parse(response.Content.ToString());
                if (result == 0)
                {
                    MessageBox.Show("Withdrawal Successfull.");
                    loadDataGrid();
                    txtAccID.Clear();
                    txtAmt.Clear();
                    txtBalance.Clear();
                }
                else if (result == -100)
                {
                    MessageBox.Show("Account Does Exist Please check account number or change account");
                }
                else if (result == -150)
                {
                    MessageBox.Show("Cannot make withdrwal Account should have minimum balance of Rs1000/=");
                }
                else if (result == -200)
                {
                    MessageBox.Show("Insufficent Funds in account cannot make withdrawal");
                }
                else if (result == -250)
                {
                    MessageBox.Show("Cannot perform withdrawal User ID mismatch!!");
                }
            }
        }
    }
}
