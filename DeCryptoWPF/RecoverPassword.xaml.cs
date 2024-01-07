using DeCrypto.Domain;
using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;
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
    /// Lógica de interacción para RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AccountServicesClient accountServicesClient;
        private DeCryptoServices.Account account;
        int code = 0;

        public RecoverPassword()
        {
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
        }

        public void ConfigurateWindow(DeCryptoServices.Account account, int code)
        {
            this.account = account;
            this.code = code;
        }

        private void Button_RecoverPassword_SendAgain_Click(object sender, RoutedEventArgs e)
        {
            SendCode();
            MessageBox.Show(Properties.Resources.MessageBox_Success_CodeSent);
        }

        private void Button_Confirmations_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTextBoxEmpty())
            {
                string validationErrors = ValidateData();
                if (string.IsNullOrEmpty(validationErrors))
                {
                    string newPasswordEncrypted = Complements.EncryptPassword(PasswordBox_ChangePassword_NewPassword.Password.ToString());
                    try
                    {
                        if (accountServicesClient.ChangePassword(this.account, newPasswordEncrypted))
                        {
                            MessageBox.Show(Properties.Resources.MessageBox_Success_InformationSaved, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (CommunicationException ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (TimeoutException ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(validationErrors, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsTextBoxEmpty()
        {
            bool isEmpty = false;
            if ((PasswordBox_ChangePassword_NewPassword.Password == "") || (TextBox_RecoverPassword_EnterCode.Text == "") ||
                (PasswordBox_ChangePassword_NewPasswordConfirmation.Password == ""))
            {
                MessageBox.Show(Properties.Resources.MessageBox_Error_EmptyFields, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmpty = true;
            }
            return isEmpty;
        }

        private void SendCode()
        {
            var random = new Random();
            var lowerBound = 1000;
            var upperBound = 9999;
            code = random.Next(lowerBound, upperBound);
            accountServicesClient.SendToken(account.email, Properties.Resources.Label_Email_TittleEmailRecoverPassword, Properties.Resources.Label_Email_BodyEmailRecoverPassword, code);
        }

        public void ValidateCode(string codeText, StringBuilder validationErrors)
        {
            if (codeText != code.ToString())
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorCode_IncorrectCode);
            }
        }

        public void ValidateMatchingPasswords(string newPassword, string confirmPassword, StringBuilder validationErrors)
        {
            if (newPassword != confirmPassword)
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords);
            }
        }

        public void ValidatePasswordComplexity(string newPassword, StringBuilder validationErrors)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(newPassword, "^(?!.*\\s)(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,64}$"))
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_PasswordLong);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneLowecase);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneUppercase);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneNumber);
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_NeedOneSpecialCharacter);
            }
        }

        private string ValidateData()
        {
            StringBuilder validationErrors = new StringBuilder();

            ValidateCode(TextBox_RecoverPassword_EnterCode.Text, validationErrors);
            ValidateMatchingPasswords(PasswordBox_ChangePassword_NewPassword.Password, PasswordBox_ChangePassword_NewPasswordConfirmation.Password, validationErrors);
            ValidatePasswordComplexity(PasswordBox_ChangePassword_NewPassword.Password, validationErrors);
            
            return validationErrors.ToString();
        }
    }
}
