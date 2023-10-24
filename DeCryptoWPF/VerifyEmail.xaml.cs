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
    /// Lógica de interacción para VerifyEmail.xaml
    /// </summary>
    public partial class VerifyEmail : Window
    {
        AccountServicesClient accountServicesClient;
        Account account;
        int code = 0;
        public VerifyEmail()
        {
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
        }

        private void Button_ConfirmationButton_Accept_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_CodeWindow_Code.Text != code.ToString())
            {
                MessageBox.Show("Código incorrecto, inténtelo de nuevo");
            }
            else
            {
                account.emailVerify = true;
                if (accountServicesClient.VerifyEmail(account))
                {
                    MessageBox.Show("Email verificado correctamente");
                    MenuGame menuGame = new MenuGame();
                    menuGame.ConfigurateWindow(account);
                    Close();
                    menuGame.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error verificando su Email. Inténtelo más tarde");
                }
            }
        }

        private void Button_VerifyEmail_SendAgain_Click(object sender, RoutedEventArgs e)
        {
            SendCode();
        }

        private void TextBox_CodeWindow_Code_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_VerifyEmail_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ConfigurateWindow(Account newAccount)
        {
            this.account = newAccount;
            SendCode();
        }

        private void SendCode()
        {
            var random = new Random();
            var lowerBound = 1000;
            var upperBound = 9999;
            code = random.Next(lowerBound, upperBound);
            accountServicesClient.SendToken(account.email, "Código de verificación", "Por favor, ingresa este código para" +
                "verificar tu cuenta", code);
        }
    }
}
