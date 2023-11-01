using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;
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
                            MessageBox.Show("Se ha actualizado correctamente la contraseña");
                            AccountInformation accountInformation = new AccountInformation();
                            accountInformation.ConfigurateWindow(account);
                            Close();
                            accountInformation.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Error, inténtelo de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    } catch (Exception ex)
                    {
                        MessageBox.Show("El servicio no se encuentra disponible");
                    }
                }
                else
                {
                    MessageBox.Show(validationErrors, "Error en datos", MessageBoxButton.OK);
                }
            }
        }

        private string ValidateData()
        {
            StringBuilder validationErrors = new StringBuilder();

            if (!accountServicesClient.CurrentPassword(this.account, Complements.EncryptPassword(PasswordBox_ChangePassword_CurrentPassword.Password)))
            {
                validationErrors.AppendLine("La contraseña ingresada no corresponde con la contraseña actual");
            }

            if (PasswordBox_ChangePassword_NewPassword.Password != PasswordBox_ChangePassword_NewPasswordConfirmation.Password)
            {
                validationErrors.AppendLine("Las contraseñas no coinciden");
            }    
            if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordBox_ChangePassword_NewPassword.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
            {

                validationErrors.AppendLine("La contraseña debe contener al menos 8 caracteres.");
                validationErrors.AppendLine("Debe contener al menos una letra minúscula.");
                validationErrors.AppendLine("Debe contener al menos una letra mayúscula.");
                validationErrors.AppendLine("Debe contener al menos un número.");
                validationErrors.AppendLine("Debe contener al menos un carácter especial.");
            }
            return validationErrors.ToString();
        }

        private bool IsEmpty()
        {
            bool isEmpty = false;
            if ((PasswordBox_ChangePassword_CurrentPassword.Password == "") || (PasswordBox_ChangePassword_NewPassword.Password == "") ||
                (PasswordBox_ChangePassword_NewPasswordConfirmation.Password == ""))
            {
                MessageBox.Show("Por favor, llene todos los campos", "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
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
