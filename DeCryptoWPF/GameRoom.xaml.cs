using DeCryptoWPF.DeCryptoServices;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System;
using System.Windows.Media.Imaging;
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
        private readonly Image[] images;
        private readonly BitmapImage ImageDefault = new BitmapImage(new Uri("/Resources/imageUUser", UriKind.Relative));
        public GameRoom()
        {
            account = new Account();
            blueTeam = new BlueTeam();
            redTeam = new RedTeam();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            InitializeComponent();
            images = new Image[] { Image_GameRoom_Player1, Image_GameRoom_Player2, Image_GameRoom_Player3, Image_GameRoom_Player4 };
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
            joinToGameClient.JoinToRoom(this.code, account.nickname, new byte[1]);
            joinToGameClient.JoinToGame(account.nickname);
        }

        private void Button_GameRoom_TeamRead_Click(object sender, RoutedEventArgs e)
        {
            if((TextBlock_GameRoom_Player1.Text !="Player1") && (TextBlock_GameRoom_Player2.Text != "Player2"))
            {
                MessageBox.Show("El equipo ya se encuentra lleno, intenta con otro");                
            }
            else
            {
                if (TextBlock_GameRoom_Player3.Text == account.nickname)
                {
                    this.blueTeam.nicknamePlayer1 = "Player3";
                    joinToGameClient.JoinToBlueTeam(blueTeam, code);
                }
                else if (TextBlock_GameRoom_Player4.Text == account.nickname)
                {
                    this.blueTeam.nicknamePlayer2 = "Player4";
                    joinToGameClient.JoinToBlueTeam(blueTeam, code);
                }
                if (TextBlock_GameRoom_Player1.Text == "Player1")
                {
                    this.redTeam.nicknamePlayer1 = account.nickname;
                    this.redTeam.nicknamePlayer2 = TextBlock_GameRoom_Player2.Text;
                }
                else if (TextBlock_GameRoom_Player2.Text == "Player2")
                {
                    this.redTeam.nicknamePlayer1 = TextBlock_GameRoom_Player1.Text;
                    this.redTeam.nicknamePlayer2 = account.nickname;
                }
                joinToGameClient.JoinToRedTeam(this.redTeam, code);
                Button_GameRoom_TeamRead.IsEnabled = false;
                Button_GameRoom_TeamBlue.IsEnabled = true;
            }
        }

        private void Button_GameRoom_TeamBlue_Click(object sender, RoutedEventArgs e)
        {
             if((TextBlock_GameRoom_Player3.Text != "Player3") && (TextBlock_GameRoom_Player4.Text != "Player4"))
             {
                MessageBox.Show("El equipo ya se encuentra lleno, intenta con otro");                
             }
             else
             {
                if (TextBlock_GameRoom_Player1.Text == account.nickname)
                {
                    redTeam.nicknamePlayer1 = "Player1";
                    joinToGameClient.JoinToRedTeam(redTeam, code);
                }
                else if (TextBlock_GameRoom_Player2.Text == account.nickname)
                {
                    redTeam.nicknamePlayer2 = "Player2";
                    joinToGameClient.JoinToRedTeam(redTeam, code);
                }
                if (TextBlock_GameRoom_Player3.Text == "Player3")
                {
                    this.blueTeam.nicknamePlayer1 = account.nickname;
                    this.blueTeam.nicknamePlayer2 = TextBlock_GameRoom_Player4.Text;
                }
                else if (TextBlock_GameRoom_Player4.Text == "Player4")
                {
                    this.blueTeam.nicknamePlayer1 = TextBlock_GameRoom_Player3.Text;
                    this.blueTeam.nicknamePlayer2 = account.nickname;
                }
                joinToGameClient.JoinToBlueTeam(this.blueTeam, code);
                Button_GameRoom_TeamBlue.IsEnabled = false;
                Button_GameRoom_TeamRead.IsEnabled = true;
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

        public void ReciveBlueTeam(BlueTeam blueTeam)
        {
            this.blueTeam = blueTeam;
            TextBlock_GameRoom_Player3.Text = blueTeam.nicknamePlayer1;
            TextBlock_GameRoom_Player4.Text = blueTeam.nicknamePlayer2;
        }

        public void ReciveRedTeam(RedTeam redTeam)
        {
            this.redTeam = redTeam;
            TextBlock_GameRoom_Player1.Text = redTeam.nicknamePlayer1;
            TextBlock_GameRoom_Player2.Text = redTeam.nicknamePlayer2;
        }

        public void RecivePlayers(Dictionary<string, byte[]> profiles)
        {
            StackPanel_GameRoom_PlayerList.Children.Clear();
            foreach (var profile in profiles)
            {
                Label Label_Player = new Label();
                Label_Player.Content = profile.Key;
                Label_Player.Margin = new Thickness(0, 0, 0, 50);
                Label_Player.Foreground = Brushes.White;
                Label_Player.FontSize = 30;
                StackPanel_GameRoom_PlayerList.Children.Add(Label_Player);
            }
        }
    }
}
