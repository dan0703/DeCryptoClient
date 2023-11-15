using DeCryptoWPF.DeCryptoServices;
using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;
using System.Collections.Generic;
using System;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window, IJoinToGameCallback
    {
        JoinToGameClient joinToGameClient;
        Account account;
        public CodeWindow()
        {
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            account = new Account();
            InitializeComponent();
        }

        private void TextBox_CodeWindow_Code_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_CodeWindow_Join_Click(object sender, RoutedEventArgs e)
        {
            int code = int.Parse(TextBox_CodeWindow_Code.Text);
            if (joinToGameClient.AllreadyExistRoom(code))
            {
                if (joinToGameClient.IsFullRoom(code))
                {
                    MessageBox.Show("La sala se encientra llena. Intenta con otra");
                }
                else
                {
                    GameRoom gameRoomWindow = new GameRoom();
                    gameRoomWindow.ConfigurateWindow(account, code);
                    Close();
                    gameRoomWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No existe ninguna partida que coincida con el codigo ingresado \n Por favor, intenta con otro", "Room", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MenuGame menuGameWindow = new MenuGame();
            menuGameWindow.ConfigurateWindow(account);
            joinToGameClient.LeaveGame(account.nickname);
            Close();
            menuGameWindow.ShowDialog();
        }

        internal void ConfigurateWindow(Account account)
        {
            this.account = account;
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

        public void ReciveFriendRequest(string senderNickname)
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
