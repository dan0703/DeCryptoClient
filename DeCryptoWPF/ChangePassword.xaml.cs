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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AccountServicesClient accountServicesClient;
        private Account account;
        public ChangePassword()
        {
            InitializeComponent();
            accountServicesClient = new AccountServicesClient();
        }

        public void ConfigurateWindow(Account account)
        {
            this.account = account;
        }

        private void Button_Confirmations_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEmpty())
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
                            AccountInformation accountInformation = new AccountInformation();
                            accountInformation.ConfigurateWindow(account);
                            Close();
                            accountInformation.ShowDialog();
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

        private string ValidateData()
        {
            StringBuilder validationErrors = new StringBuilder();

            try
            {
                if (!accountServicesClient.CurrentPassword(this.account, Complements.EncryptPassword(PasswordBox_ChangePassword_CurrentPassword.Password)))
                {
                    validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_IncorrectPassword);
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

            if (PasswordBox_ChangePassword_NewPassword.Password != PasswordBox_ChangePassword_NewPasswordConfirmation.Password)
            {
                validationErrors.AppendLine(Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords);
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordBox_ChangePassword_NewPassword.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
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
            if ((PasswordBox_ChangePassword_CurrentPassword.Password == "") || (PasswordBox_ChangePassword_NewPassword.Password == "") ||
                (PasswordBox_ChangePassword_NewPasswordConfirmation.Password == ""))
            {
                MessageBox.Show(Properties.Resources.MessageBox_Error_EmptyFields, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmpty = true;
            }
            return isEmpty;
        }

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
