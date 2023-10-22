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
using DeCryptoWPF;
using DeCryptoWPF.Logic;
using System.Net.Mail;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly AccountServicesClient accountServicesClient;
        private readonly PlayerServicesClient playerServicesClient;
        public Register()
        {
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
            playerServicesClient = new PlayerServicesClient();

        }

        private void ComboBox_Register_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_Register_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Register_Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Register_User_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Register_Enter_Click(object sender, RoutedEventArgs e)
        {
            string passwordHashed = Complements.EncryptPassword(PasswordBox_Register_Password.Password);
            Account account = new Account()
            {
                email = TextBox_Register_Email.Text,
                emailVerify = false,
                nickname = TextBox_Register_User.Text,
                name = TextBox_Register_Name.Text,
                password = passwordHashed,
            };
            User user = new User()
            {
                name = TextBox_Register_Name.Text,
                birthDay = DatePicker_Register_Birthday.Text,
                accountNickname = TextBox_Register_User.Text,
            };
            if (ValidateData())
            {
                if (accountServicesClient.RegisterAccount(account)&& playerServicesClient.RegisterPlayer(user))
                {
                    MessageBox.Show("Registro exitoso");
                    OpenSignIn();
                }
                else
                {
                    MessageBox.Show("Error, intentalo mas tarde");
                }
            }
        }

        private bool ValidateData()
        {
            bool isValidData = false;
            if (IsValidEmail(TextBox_Register_Email.Text))
            {
                if (TextBox_Register_User.Text != "")
                {
                    if (PasswordBox_Register_Password.Password != "")
                    {
                        isValidData = true;
                    }
                    else
                    {
                        MessageBox.Show("La contraseña debe contener al menos 8 caracteres, por favor ingrese otro nickname");
                    }
                }
                else
                {
                    MessageBox.Show("El nickname contiene caracteres extraños, por favor ingrese otro nickname");
                }
            }
            else
            {
                MessageBox.Show("La direccion de correo no es válida, por favor ingrese otra");
            }

            return isValidData;
        }

        static bool IsValidEmail(string email)
        {
            bool isValidEmail = false;
            try
            {
                var mailAdress = new MailAddress(email);
                isValidEmail = true;

            }
            catch (FormatException ex)
            {
                isValidEmail = true;
            }
            return isValidEmail;
        }

        private void Label_Register_AlreadyAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenSignIn();
        }
        private void OpenSignIn()
        {
            var signInWindow = new SignIn();
            Close();
            signInWindow.ShowDialog();
        }
    }
}
