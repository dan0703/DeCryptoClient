using DeCrypto.Domain;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : Window
    {
        AccountServicesClient accountServicesClient;
        DeCryptoServices.Account account;
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

        private void TextBox_RecoverPassword_EnterCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_RecoverPassword_SendAgain_Click(object sender, RoutedEventArgs e)
        {
            SendCode();
            MessageBox.Show("Se ha enviado un código a tu correo electrónico");
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
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Error, inténtelo de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
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

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsEmpty()
        {
            bool isEmpty = false;
            if ((PasswordBox_ChangePassword_NewPassword.Password == "") || (TextBox_RecoverPassword_EnterCode.Text == "") ||
                (PasswordBox_ChangePassword_NewPasswordConfirmation.Password == ""))
            {
                MessageBox.Show("Por favor, llene todos los campos", "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
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
            accountServicesClient.SendToken(account.email, "Código de verificación", "Por favor, ingresa este código para" +
                "verificar tu cuenta", code);
        }

        private string ValidateData()
        {
            StringBuilder validationErrors = new StringBuilder();

            if (TextBox_RecoverPassword_EnterCode.Text != code.ToString())
            {
                validationErrors.AppendLine("El código ingresado es incorrecto");
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

    }
}
