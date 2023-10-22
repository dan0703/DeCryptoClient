using DeCryptoWPF.DeCryptoServices;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para GameRoom.xaml
    /// </summary>
    public partial class GameRoom : Window, IJoinToGameCallback
    {
        private readonly JoinToGameClient joinToGameClient;
        private int code;
        private Account account;
        private BlueTeam blueTeam;
        private RedTeam redTeam;
        public GameRoom()
        {
            account = new Account();
            blueTeam = new BlueTeam();
            redTeam = new RedTeam();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            InitializeComponent();
            Closing += GameRoom_Closing;
        }

        private void GameRoom_Closing(object sender, CancelEventArgs e)
        {
            joinToGameClient.LeaveRoom(account.nickname, code, blueTeam, redTeam);
        }

        public void ConfigurateWindow(Account account, int code)
        {
            if (code >1)
            {
                this.code = code;
            }
            else
            {
               this.code = joinToGameClient.CreateRoom();
            }
            Label_GameRoom_Code.Content = this.code;
            this.account = account;
            joinToGameClient.JoinToRoom(this.code, account.nickname);
        }

        private void Button_GameRoom_TeamRead_Click(object sender, RoutedEventArgs e)
        {
            if(TextBlock_GameRoom_Player1.Text == "Player1")
            {
                this.redTeam.nicknamePlayer1 = account.nickname;
                this.redTeam.nicknamePlayer2 = TextBlock_GameRoom_Player2.Text;
                joinToGameClient.joinToRedTeam(this.redTeam, code);

            }
            else if (TextBlock_GameRoom_Player2.Text == "Player2")
            {
                this.redTeam.nicknamePlayer1 = TextBlock_GameRoom_Player1.Text;
                this.redTeam.nicknamePlayer2 = account.nickname;
                joinToGameClient.joinToRedTeam(this.redTeam, code);

            }
            else
            {
                MessageBox.Show("El equipo ya se encuentra lleno, intenta con otro");
            }
        }

        private void Button_GameRoom_TeamBlue_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlock_GameRoom_Player3.Text == "Player3")
            {
                this.blueTeam.nicknamePlayer1 = account.nickname;
                this.blueTeam.nicknamePlayer2 = TextBlock_GameRoom_Player4.Text;
                joinToGameClient.joinToBlueTeam(this.blueTeam, code);

            }
            else if (TextBlock_GameRoom_Player4.Text == "Player4")
            {
                this.blueTeam.nicknamePlayer1 = TextBlock_GameRoom_Player3.Text;
                this.blueTeam.nicknamePlayer2 = account.nickname;
                joinToGameClient.joinToBlueTeam(this.blueTeam, code);
            }
            else
            {
                MessageBox.Show("El equipo ya se encuentra lleno, intenta con otro");
            }
        }

        private void Button_GameRoom_StartGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_GameRoom_SendCode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_GameRoom_Chat_Click(object sender, RoutedEventArgs e)
        {
            CloseOpenChat();
        }

        private void Image_GameRoom_CloseChat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CloseOpenChat();
        }

        private void CloseOpenChat()
        {
            if (Expander_GameRoom_Chat.IsExpanded)
            {
                Expander_GameRoom_Chat.IsExpanded = false;
            }
            else
            {
                Expander_GameRoom_Chat.IsExpanded = true;
            }
        }

        public void RecivePlayers(string[] playerList)
        {
            StackPanel_GameRoom_PlayerList.Children.Clear();
            foreach (var profile in playerList)
            {
                Label Label_Player = new Label();
                Label_Player.Content = profile;
                Label_Player.Margin = new Thickness(0, 0, 0, 50);
                Label_Player.Foreground = Brushes.White;
                Label_Player.FontSize = 30;
                StackPanel_GameRoom_PlayerList.Children.Add(Label_Player);
            }
        }

        public void ReciveBlueTeam(BlueTeam blueTeam)
        {
            TextBlock_GameRoom_Player3.Text = blueTeam.nicknamePlayer1;
            TextBlock_GameRoom_Player4.Text = blueTeam.nicknamePlayer2;
        }

        public void ReciveRedTeam(RedTeam redTeam)
        {
            TextBlock_GameRoom_Player1.Text = redTeam.nicknamePlayer1;
            TextBlock_GameRoom_Player2.Text = redTeam.nicknamePlayer2;
        }
    }
}
