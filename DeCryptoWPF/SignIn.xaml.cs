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
using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;

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
            if (ValidateData())
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
                        menuGameWidow.configurateWindow(newAccount);
                        Close();
                        menuGameWidow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Credenciales incorrectas", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show("El servicio no se encuentra disponible, por favor intentelo mas tarde"); 
                }
                
            }
        }

        private bool ValidateData()
        {
            bool isValid = true;
            if (PasswordBox_SignIn_Password == null)
            {
                MessageBox.Show("Por favor, ingrese una contraseña", "Campo vacio", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;

            }
            else if (TextBox_SignIn_Email.Text == null)
            {
                MessageBox.Show("Por favor, ingresa tu correo electronico", "Campo vacio", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            return isValid;
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
