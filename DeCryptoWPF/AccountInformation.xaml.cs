using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string profilePicturePathCopy = string.Empty;
        public AccountInformation()
        {
            InitializeComponent();
            Closing += GameRoom_Closing;            
        }

        private void GameRoom_Closing(object sender, CancelEventArgs e)
        {
            Image_AccountInformation_ProfilePicture = null;
        }

        private Account account;

        public void ConfigurateWindow(Account account)
        {
            this.account = account;            
            ConfigurateData();            
        }

        private void ConfigurateData()
        {
            profilePicturePathCopy = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Images/", account.nickname + "_Copy" + ".png");            
            Label_AccountInformation_Email.Content = this.account.email;
            Label_AccountInformation_Nickname.Content = this.account.nickname;
            CopyProfilePicture();
        }
        private void CopyProfilePicture()
        {

            var profilePicturePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Images/", account.nickname + ".png");
            if (File.Exists(profilePicturePath))
            {
                try
                {
                    File.Copy(profilePicturePath, profilePicturePathCopy, true);
                    Image_AccountInformation_ProfilePicture.Source = new BitmapImage(new Uri(profilePicturePathCopy));
                }
                catch (IOException ioException)
                {
                    log.Error(ioException);
                    Image_AccountInformation_ProfilePicture.Source = new BitmapImage(new Uri(profilePicturePath));
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    log.Error(unauthorizedAccessException);
                    Image_AccountInformation_ProfilePicture.Source = new BitmapImage(new Uri(profilePicturePath));
                }
                catch (Exception exception)
                {
                    log.Error(exception);
                    Image_AccountInformation_ProfilePicture.Source = new BitmapImage(new Uri(profilePicturePath));
                }
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
            string playerProfilePath = Complements.UploadImage();
            if (!string.IsNullOrEmpty(playerProfilePath))
            {

                if (Complements.SaveImage(account.nickname, playerProfilePath))
                {
                    MessageBox.Show("La imagen de perfil ha sido guardada con exito \n La aplicación será reiniciada", "Account Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Application.Current.Shutdown();
                    RestartApplication();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guarda la imagen, intetelo de nuevo");
                }
            }            
        }
        private void RestartApplication()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
