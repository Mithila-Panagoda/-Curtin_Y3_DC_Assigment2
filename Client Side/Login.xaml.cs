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
namespace Client_Side
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        User user = User.Instance;
        public Login()
        { 
            InitializeComponent();
            User user = User.Instance;
            if (user.ID > 0)
            {
                txtID.Text = user.ID.ToString();
            }
            else
            {
                txtID.Text = "";
            }
        }

        private async void btnLogincust_Click(object sender, RoutedEventArgs e)
        {
            uint ID = uint.Parse(txtID.Text);
            string fname = txtfname.Text;
            string url = @"https://localhost:44339/api/CustomerLogin/?ID=" + ID + "&fname=" + fname;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response =await Task.Run(() => client.Get(request));

            string result =response.Content.ToString();
            if (result.Equals("\"success\""))
            {
                MessageBox.Show("User Logged in successfully");
                user.fname = fname;
                user.ID = ID;
                HomePage homePage = new HomePage();
                this.NavigationService.Navigate(homePage);
            }
            else
            {
                MessageBox.Show("User ID or name incorrect Please try again");
                txtfname.Text="";
                txtID.Text = "";
            }
        }
    }
}
