using DeCryptoWPF.DeCryptoServices;
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
    /// Lógica de interacción para InGame.xaml
    /// </summary>
    public partial class InGame : Window, IJoinToGameCallback
    {
        public readonly JoinToGameClient joinToGameClient;
        public readonly ChatMessageClient chatMessageClient;
        public GameRoom gameRoomWindow;
        private Account account;

        public InGame()
        {
            InitializeComponent();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            chatMessageClient = new ChatMessageClient(new InstanceContext(this));
        }
        public void ConfigurateWindow(GameRoom gameRoomWindow, Account account, int code)
        {
            this.gameRoomWindow = gameRoomWindow;
            this.account = account;
            this.joinToGameClient.JoinToGame(account.nickname, null);
        }

        private void Button_InGame_GiveClues_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel_InGame_GiveClues.Visibility == Visibility.Collapsed) {
                StackPanel_InGame_GiveClues.Visibility = Visibility.Visible;
                Button_InGame_GiveClues.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_InGame_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_InGame_SaveClues_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel_InGame_GiveClues.Visibility == Visibility.Visible)
            {
                StackPanel_InGame_GiveClues.Visibility = Visibility.Collapsed;
            }

            if (StackPanel_InGame_DecryptClues.Visibility == Visibility.Collapsed)
            {
                StackPanel_InGame_DecryptClues.Visibility= Visibility.Visible;
            }
        }

        private void Image_InGame_ReportPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportPlayer reportPlayer = new ReportPlayer();
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            reportPlayer.ShowDialog();
            this.Effect = null;
        }

        public void RecivePlayers(Dictionary<string, byte[]> profiles)
        {
            throw new NotImplementedException();
        }

        public void ReciveBlueTeam(BlueTeam blueTeam)
        {
            throw new NotImplementedException();
        }

        public void ReciveRedTeam(RedTeam redTeam)
        {
            throw new NotImplementedException();
        }

        public void ReciveFriendRequest(string senderNickname, string[] friendRequestList)
        {
            throw new NotImplementedException();
        }

        public void SetFriendList(string[] friendList)
        {
            throw new NotImplementedException();
        }

        public void GoToGameWindow()
        {
            throw new NotImplementedException();
        }
    }
}
