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
using System.ServiceModel;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para MenuGame.xaml
    /// </summary>
    public partial class MenuGame : Window, IJoinToGameCallback
    {
        private JoinToGameClient joinToGameClient;
        private Account account;


        public MenuGame()
        {
            InitializeComponent();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
        }
        public void ConfigurateWindow(Account account)
        {
            this.account = account;
            joinToGameClient.JoinToGame(account.nickname, null);

        }

        private void Button_MenuGame_NewGame_Click(object sender, RoutedEventArgs e)
        {
            GameRoom gameRoomWindow= new GameRoom();
            joinToGameClient.LeaveGame(account.nickname);
            gameRoomWindow.ConfigurateWindow(account, 0);
            Close();
            gameRoomWindow.ShowDialog();
        }

        private void Button_MenuGame_FindGame_Click(object sender, RoutedEventArgs e)
        {
            CodeWindow codeWindow= new CodeWindow();
            codeWindow.ConfigurateWindow(this.account);
            joinToGameClient.LeaveGame(account.nickname);
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

        public void ReciveBlueTeam(BlueTeam blueTeam)
        {
            throw new NotImplementedException();
        }

        public void ReciveRedTeam(RedTeam redTeam)
        {
            throw new NotImplementedException();
        }

        public void RecivePlayers(Dictionary<string, byte[]> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
