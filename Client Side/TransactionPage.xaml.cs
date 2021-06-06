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
    /// Interaction logic for TransactionPage.xaml
    /// </summary>
    public partial class TransactionPage : Page
    {
        public  TransactionPage()
        {
            InitializeComponent();
            loadACID();
           // loadTransactions();


        }
        private async void loadACID()
        {
            User user = User.Instance;
            string url = @"https://localhost:44339/api/GetUserAccountIDs/?UID=" + user.ID;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = await Task.Run(()=> client.Get(request));
            List<uint> data = JsonConvert.DeserializeObject<List<uint>>(response.Content.ToString());
            if (data.Count == 0)
            {
                MessageBox.Show("User Does Not have any account Please Make an account with the Bank");
                userIDs.IsEnabled = false;
                txtReciverID.IsEnabled = false;
                txtamount.IsEnabled = false;
                btnSubmit.IsEnabled = false;
                btnSearch.IsEnabled = false;
            }
            else
            {
                userIDs.ItemsSource = data;
                userIDs.IsEnabled = true;
                txtReciverID.IsEnabled = true;
                txtamount.IsEnabled = true;
                btnSubmit.IsEnabled = true;
                btnSearch.IsEnabled = true;
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string accID = userIDs.SelectedItem.ToString();
            string reciver = txtReciverID.Text;
            string amount = txtamount.Text;
            string url = @"https://localhost:44339/api/NewTransaction/?accID=" + accID+ "&reciverID="+reciver+ "&amount="+amount;
            var client = new RestClient(url);
            var request = new RestRequest();
           // var response = await Task.Run(()=>  client.Get(request));
            var response = client.Get(request);
            string result = JsonConvert.DeserializeObject<string>(response.Content.ToString());
            if (result.Equals("Successfull"))
            {
                MessageBox.Show("Transaction Has Been Submiited for processing.");
                loadTransactions();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void loadTransactions()
        {
            User user = User.Instance;
            string url = @"https://localhost:44339/api/LoadUserTransactions/?UID=" + user.ID ;
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);
            string result = JsonConvert.DeserializeObject<string>(response.Content.ToString());
            MessageBox.Show("Response recived : " + result);
            if (result.Equals("[]"))
            {
                MessageBox.Show("No Pending Transactions for User");
            }
            else
            {
                List<Transaction> data = JsonConvert.DeserializeObject<List<Transaction>>(response.Content.ToString());
                PendingTransActions.ItemsSource = data;
            }
            
        }
    }
}
