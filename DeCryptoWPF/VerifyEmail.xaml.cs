using DeCryptoWPF.DeCryptoServices;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AccountServicesClient accountServicesClient;
        private Account account;
        private int code = 0;

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
                try
                {
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
                catch (CommunicationException ex)
                {
                    log.Error(ex);
                    MessageBox.Show("El servicio no se encuentra disponible");
                }
                catch (TimeoutException ex)
                {
                    log.Error(ex);
                    MessageBox.Show("El servicio no se encuentra disponible");
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    MessageBox.Show("El servicio no se encuentra disponible");
                }
            }
        }

        private void Button_VerifyEmail_SendAgain_Click(object sender, RoutedEventArgs e)
        {
            SendCode();
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
