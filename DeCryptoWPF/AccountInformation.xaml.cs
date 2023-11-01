using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para AccountInformation.xaml
    /// </summary>
    public partial class AccountInformation : Window
    {
        public AccountInformation()
        {
            InitializeComponent();
        }
        private Account account;

        public void ConfigurateWindow(Account account)
        {
            this.account = account;
            ConfigurateData();
        }

        private void ConfigurateData()
        {
            Label_AccountInformation_Email.Content = this.account.email;
            Label_AccountInformation_Nickname.Content = this.account.nickname;
            var profilePicturePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Images/", account.nickname + ".png");
            if(File.Exists(profilePicturePath))
            {
                Image_AccountInformation_ProfilePicture.Source = new BitmapImage(new Uri(profilePicturePath));
            }
        }

        private void Button_AccountInformation_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.ConfigurateWindow(this.account);
            Close();
            changePassword.ShowDialog();
        }

        private void Button_AccountInformation_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_AcountInformationEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_AccountInformation_ProfilePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image_AccountInformation_ProfilePicture.Source = null;
            string playerProfilePath = Complements.UploadImage();
            if (!string.IsNullOrEmpty(playerProfilePath))
            {
                Image_AccountInformation_ProfilePicture.Source = new BitmapImage(new Uri(playerProfilePath));
                if(Complements.SaveImage(account.nickname, playerProfilePath))
                {
                    MessageBox.Show("La imagen de perfil ha sido guardada con exito.", "Account Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guarda la imagen, intetelo de nuevo");
                }
            }            
        }
    }
}
