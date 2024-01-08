using DeCryptoWPF.DeCryptoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            ValidateName(TextBox_Register_Name.Text, validationErrors);
            ValidateEmail(TextBox_Register_Email.Text, validationErrors);
            ValidateBirthday(DatePicker_Register_Birthday.SelectedDate, validationErrors);
            ValidatePasswords(PasswordBox_Register_Password.Password, PasswordBox_Register_ConfirmPassword.Password, validationErrors);
            ValidateUser(TextBox_Register_User.Text, validationErrors);
            ValidatePasswordComplexity(PasswordBox_Register_Password.Password, validationErrors);

            return validationErrors.ToString();
        }

        public void ValidateName(string name, StringBuilder validationErrors)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+(?:\\s[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+)*$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_ErrorNameCharacters);
            }
        }

        public void ValidateEmail(string email, StringBuilder validationErrors)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, "^(?=.{8,45}$)[\\w%+-]+@[a-zA-Z\\d.-]+(?<!\\.)\\.[a-zA-Z]{2,}$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_EmailInvalid);
            }
        }

        public void ValidateBirthday(DateTime? selectedDate, StringBuilder validationErrors)
        {
            if (selectedDate == null || selectedDate.Value >= DateTime.Now.Date)
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_ErrorDateBirthday);
            }
        }

        public void ValidatePasswords(string password, string confirmPassword, StringBuilder validationErrors)
        {
            if (password != confirmPassword)
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords);
            }
        }

        public void ValidateUser(string user, StringBuilder validationErrors)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(user, "^[A-Za-z0-9]{1,20}$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_Register_ErrorUserCharacters);
            }
        }

        public void ValidatePasswordComplexity(string password, StringBuilder validationErrors)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, "^(?!.*\\s)(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,64}$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_PasswordLong);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneLowecase);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneUppercase);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneNumber);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneSpecialCharacter);
            }
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
