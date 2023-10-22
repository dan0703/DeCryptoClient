using DeCryptoWPF.DeCryptoServices;
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
using System.Windows.Shapes;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para AccountInformation.xaml
    /// </summary>
    public partial class AccountInformation : Window
    {
        public AccountInformation()
        {
            InitializeComponent();
        }
        private Account account;

        public void ConfigurateWindow(Account account)
        {
            this.account = account;
            configurateData();
        }

        private void configurateData()
        {
            Label_AccountInformation_Email.Content = this.account.email;
            Label_AccountInformation_Nickname.Content = this.account.nickname;
        }


        private void Button_AccountInformation_ChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AccountInformation_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_AcountInformationEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
