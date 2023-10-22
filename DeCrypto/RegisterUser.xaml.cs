using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
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

namespace DeCrypto
{
    /// <summary>
    /// Lógica de interacción para RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window 
    {
        public RegisterUser()
        {
            InitializeComponent();
        }

        private void Button_RegisterUser_Enter_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                using ()
                {
                    
                }
            }
        }

        private bool ValidateData()
        {
            bool isValidData = false;
            if (IsValidEmail(TextBox_Email.Text))
            {
                if (TextBox_User.Text != "")
                {
                    if (TextBox_Password.Text != "")
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
                isValidEmail = false;
            }
            return isValidEmail;
        }
    }
}
