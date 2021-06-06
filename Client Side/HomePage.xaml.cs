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

namespace Client_Side
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            User user = User.Instance;
            TBID.Text = user.ID.ToString();
            TBName.Text = user.fname;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginandReg loginandReg = new LoginandReg();
            this.NavigationService.Navigate(loginandReg);
        }

        private void btnAccmngt_Click(object sender, RoutedEventArgs e)
        {
            AccountPage accountPage = new AccountPage();
            this.NavigationService.Navigate(accountPage);
        }

        private void btnUsermngt_Click(object sender, RoutedEventArgs e)
        {
            ProfileMngt profile = new ProfileMngt();
            this.NavigationService.Navigate(profile);
        }

        private void btntransactionmngt_Click(object sender, RoutedEventArgs e)
        {
            TransactionPage transaction = new TransactionPage();
            this.NavigationService.Navigate(transaction);
        }
    }
}
