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
using DeCryptoWPF;
using System.ServiceModel;
using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;
using System.ServiceModel.Channels;
using log4net;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        private AccountServicesClient accountServicesClient;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public SignIn()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
        }

        private void Button_SignIn_SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTextBoxEmpty())
            {
                string passwordHashed = Complements.EncryptPassword(PasswordBox_SignIn_Password.Password);
                Account account = new Account()
                {
                    password = passwordHashed,
                    email = TextBox_SignIn_Email.Text
                };
                try
                {
                    var newAccount = accountServicesClient.Login(account);
                    if (newAccount != null)
                    {
                        MessageBox.Show(newAccount.nickname);
                        MenuGame menuGameWidow = new MenuGame();
                        menuGameWidow.ConfigurateWindow(newAccount);
                        Close();
                        menuGameWidow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.MessageBox_SignIn_IncorrectCredentials, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (CommunicationException ex)
                {
                    log.Error(ex);
                    MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException, "DeCrypto1", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeoutException ex)
                {
                    log.Error(ex);
                    MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException, "DeCrypto2", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException, "DeCrypto3", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsTextBoxEmpty()
        {
            bool isEmpty = false;
            if ((TextBox_SignIn_Email.Text == "") || (PasswordBox_SignIn_Password.Password == ""))
            {
                MessageBox.Show(Properties.Resources.MessageBox_Error_EmptyFields, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmpty = true;
            }
            return isEmpty;
        }

        private void Button_SignIn_SignInAsGuest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Label_SignIn_SignUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var registerWindow = new Register();
            Close();
            registerWindow.ShowDialog();
        }

        private void Label_SignIn_ForgotPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var  emailWindow= new EmailWindow();
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            emailWindow.ShowDialog();
            this.Effect = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(TextBox_SignIn_Password.Visibility == Visibility.Collapsed)
            {
                TextBox_SignIn_Password.Visibility = Visibility.Visible;
                TextBox_SignIn_Password.Text = PasswordBox_SignIn_Password.Password;
                PasswordBox_SignIn_Password.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordBox_SignIn_Password.Password = TextBox_SignIn_Password.Text;
                TextBox_SignIn_Password.Visibility = Visibility.Collapsed;
                PasswordBox_SignIn_Password.Visibility = Visibility.Visible;
            }
        }

        private void TextBox_SignIn_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordBox_SignIn_Password.Password = TextBox_SignIn_Password.Text;
        }
    }
}
