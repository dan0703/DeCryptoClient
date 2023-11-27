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
using log4net;
using System.ServiceModel;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

            if (!IsEmpty())
            {
                string validationErrors = ValidateData();
                if (string.IsNullOrEmpty(validationErrors))
                {
                    try
                    {
                        if (accountServicesClient.RegisterAccount(account) && playerServicesClient.RegisterPlayer(user))
                        {
                            MessageBox.Show(Properties.Resources.MessageBox_Success_RegisterSucessfull, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Information);
                            OpenSignInWindow();
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (CommunicationException ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (TimeoutException ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(validationErrors, "DeCrypto", MessageBoxButton.OK);
                }
            } 
        }

        private string ValidateData()
        {
            StringBuilder validationErrors = new StringBuilder();

            if (!System.Text.RegularExpressions.Regex.IsMatch(TextBox_Register_Name.Text, "^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\\s]*$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_ErrorNameCharacters);
            }

            if (!IsValidEmail(TextBox_Register_Email.Text))
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_EmailInvalid);
            }

            if (DatePicker_Register_Birthday.SelectedDate == null || DatePicker_Register_Birthday.SelectedDate.Value >= DateTime.Now)
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_ErrorDateBirthday);
            }

            if (PasswordBox_Register_Password.Password != PasswordBox_Register_ConfirmPassword.Password)
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords);
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(TextBox_Register_User.Text, "^[A-Za-zÀ-ÖØ-öø-ÿ]{1,20}$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_ErrorUserCharacters);
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordBox_Register_Password.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_PasswordLong);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneLowecase);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneUppercase);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneNumber);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneSpecialCharacter);
            }
            return validationErrors.ToString();
        }

        private bool IsEmpty()
        {
            bool isEmpty = false;

            if ((TextBox_Register_Name.Text == "") || (TextBox_Register_Email.Text == "") || (PasswordBox_Register_Password.Password == "") ||
                (TextBox_Register_User.Text == "") || (TextBox_Register_Email.Text == "") || (DatePicker_Register_Birthday == null))
            {
                MessageBox.Show(Properties.Resources.MessageBox_Error_EmptyFields, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmpty = true;
            }
            return isEmpty;
        }

        static bool IsValidEmail(string email)
        {
            bool isValidEmail = false;
            try
            {
                var mailAdress = new MailAddress(email);
                isValidEmail = true;
            }
            catch (FormatException)
            {
                isValidEmail = false;
            }
            return isValidEmail;
        }

        private void Label_Register_AlreadyAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenSignInWindow();
        }

        private void OpenSignInWindow()
        {
            var signInWindow = new SignIn();
            Close();
            signInWindow.ShowDialog();
        }
    }
}
