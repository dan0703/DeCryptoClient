using DeCryptoWPF.DeCryptoServices;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel;
using System.Collections.Generic;

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
        public GameRoom()
        {
            account = new Account();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            InitializeComponent();
            code= joinToGameClient.CreateRoom();
            Label_GameRoom_Code.Content = code;
        }
        public void ConfigurateWindow(Account account)
        {
            this.account = account;
            joinToGameClient.JoinToRoom(code, account.nickname);
        }

        private void Button_GameRoom_TeamRead_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_GameRoom_TeamBlue_Click(object sender, RoutedEventArgs e)
        {

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

        public void RecivePlayers(Dictionary<string, byte[]> profiles)
        {
            MessageBox.Show("Hola");
            foreach (var profile in profiles)
            {
                MessageBox.Show(profile.Key);
            }
        }
    }
}
