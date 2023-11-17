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
    /// Lógica de interacción para EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AccountServicesClient accountServicesClient;
        private Account account;
        private int code = 0;

        public EmailWindow()
        {
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
        }

        private void Button_Confirmations_Accept_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_EmailWindow_Email.Text != "")
            {
                try
                {
                    if (accountServicesClient.ExistAccount(TextBox_EmailWindow_Email.Text))
                    {
                        account = new Account()
                        {
                            email = TextBox_EmailWindow_Email.Text
                        };

                        SendCode();
                        MessageBox.Show("Se ha enviado un código a tu correo electrónico");

                        RecoverPassword recoverPassword = new RecoverPassword();
                        recoverPassword.ConfigurateWindow(this.account, code);
                        Close();
                        recoverPassword.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("El correo no corresponde a alguna cuenta registrada");
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
            else
            {
                MessageBox.Show("Por favor, ingrese su correo electrónico");
            }
        }

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
