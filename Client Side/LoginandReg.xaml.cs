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
    /// Interaction logic for LoginandReg.xaml
    /// </summary>
    public partial class LoginandReg : Page
    {
        public LoginandReg()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            Login login = new Login();
            this.NavigationService.Navigate(login);
            user.ID = 0;
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            string fname = txtfname.Text;
            string lname = txtlname.Text;
            string url = @"https://localhost:44339/api/CustomerRegistration/" + fname + "/" + lname;

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);

            uint ID = uint.Parse(response.Content.ToString());
            if (ID > 0)
            {
                MessageBox.Show("User Regiserted Successfully ID genrated :" + response.Content.ToString());
                user.fname = fname;
                user.ID = ID;
                Login login = new Login();
                this.NavigationService.Navigate(login);
            }
            else
            {
                MessageBox.Show("Registrtion failed.");
                user.ID = 0;
            }
            
        }
    }
}
