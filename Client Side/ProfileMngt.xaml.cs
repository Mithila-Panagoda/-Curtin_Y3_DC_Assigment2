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
using Newtonsoft.Json;
using RestSharp;

namespace Client_Side
{
    /// <summary>
    /// Interaction logic for ProfileMngt.xaml
    /// </summary>
    public partial class ProfileMngt : Page
    {
        public ProfileMngt()
        {
            InitializeComponent();
            User user = User.Instance;
            string url = @"https://localhost:44339/api/LoadProfile/?UID=" + user.ID;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);
            string result = response.Content.ToString();
            List<string> data = JsonConvert.DeserializeObject<List<string>>(result);
            foreach(string info in data)
            {
                user = JsonConvert.DeserializeObject<User>(info);
            }
            txtUID.Text = user.ID.ToString();
            txtFname.Text = user.fname;
            txtLname.Text = user.lname;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage();
            this.NavigationService.Navigate(home);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            string fname = txtFname.Text;
            string lname = txtLname.Text;
            string url = @"https://localhost:44339/api/UpdateProfile/?UID=" + user.ID+"&fname="+fname+"&lname="+lname;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);
            
            if(txtFname.Text.Length==0 || txtLname.Text.Length == 0)
            {
                MessageBox.Show("Felids cannot be Empty!!");
            }
            else
            {
                int result = int.Parse(response.Content.ToString());
                if (result == 0)
                { 
                    MessageBox.Show("User Information Updated Successfully You will be logged out now Please relogin");
                    Login login = new Login();
                    this.NavigationService.Navigate(login);
                }
                else
                {
                    MessageBox.Show("Failed to update please try again");
                }
            }
        }
    }
}
