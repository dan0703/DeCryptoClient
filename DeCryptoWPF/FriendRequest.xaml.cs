using DeCryptoWPF.DeCryptoServices;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para FriendRequest.xaml
    /// </summary>
    public partial class FriendRequest : Window
    {
        private Account senderAccount;
        private Account addressee;
        public FriendRequest(Account senderAccount, Account addressee )
        {
            this.senderAccount = senderAccount;
            this.addressee = addressee;
            InitializeComponent();
        }

        private void configurateComponents()
        {
            Label_FriendRequest_Nickname.Content = senderAccount.nickname;
            Image_FriendRequest_ProfileImage= (BytesToImage(senderAccount.profileImage));
        }

        private Image BytesToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.DecodePixelWidth = 60;
                bitmapImage.DecodePixelHeight = 60;
                bitmapImage.EndInit();
            }

            Image image = new Image();
            image.Source = bitmapImage;
            return image;
        }

        private void Button_FriendRequest_Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_FriendRequest_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
