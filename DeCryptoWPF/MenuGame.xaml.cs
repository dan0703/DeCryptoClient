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

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para MenuGame.xaml
    /// </summary>
    public partial class MenuGame : Window
    {
        public MenuGame()
        {
            InitializeComponent();
        }
        private Account account;
        public void configurateWindow(Account account)
        {
            this.account = account;
        }

        private void Button_MenuGame_NewGame_Click(object sender, RoutedEventArgs e)
        {
            GameRoom gameRoomWindow= new GameRoom();
            gameRoomWindow.ConfigurateWindow(account);
            Close();
            gameRoomWindow.ShowDialog();
        }

        private void Button_MenuGame_FindGame_Click(object sender, RoutedEventArgs e)
        {
            CodeWindow codeWindow= new CodeWindow();
            Close();
            codeWindow.ShowDialog();
        }

        private void ComboBox_MenuGame_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_MenuGame_AccountConfiguration_Click(object sender, RoutedEventArgs e)
        {
            AccountInformation accountInformation= new AccountInformation();
            accountInformation.ConfigurateWindow(account);
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            accountInformation.ShowDialog();
            this.Effect = null;
        }

        private void Button_MenuGame_FriendList_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
