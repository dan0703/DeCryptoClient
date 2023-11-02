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

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        AccountServicesClient accountServicesClient;
        public SignIn()
        {
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
        }

        private void TextBox_SignIn_Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_SignIn_SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEmpty())
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
                        MenuGame menuGameWidow = new MenuGame();
                        menuGameWidow.ConfigurateWindow(newAccount);
                        Close();
                        menuGameWidow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Credenciales incorrectas", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("El servicio no se encuentra disponible, por favor intentelo mas tarde");
                }

            }
        }

        private bool IsEmpty()
        {
            bool isEmpty = false;
            if ((TextBox_SignIn_Email.Text == "") && (PasswordBox_SignIn_Password.Password == ""))
            {
                MessageBox.Show("Por favor, ingresa un correo y una contraseña", "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmpty = true;
            }
            else if ((TextBox_SignIn_Email.Text == ""))
            {
                MessageBox.Show("Por favor, ingresa un correo", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmpty = true;
            }
            else if ((PasswordBox_SignIn_Password.Password == ""))
            {
                MessageBox.Show("Por favor, ingresa un una contraseña", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Error);
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
