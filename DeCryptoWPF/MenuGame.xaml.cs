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
using log4net;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para MenuGame.xaml
    /// </summary>
    public partial class MenuGame : Window, IJoinToGameCallback, IFriendsServicesCallback
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private JoinToGameClient joinToGameClient;
        private DeCryptoServices.Account account;
        private FriendsServicesClient friendsServicesClient;

        public MenuGame()
        {
            InitializeComponent();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            friendsServicesClient = new FriendsServicesClient(new InstanceContext(this));

        }
        public void ConfigurateWindow(Account account)
        {            
            try
            {
                this.account = account;
                joinToGameClient.JoinToGame(account.nickname, null);
                friendsServicesClient.jointToFriendRequestService(account.nickname);

            }
            catch (CommunicationException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException + ex.Message);
            }
            catch (TimeoutException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException + ex.Message);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException + ex.Message);
            }
        }

        private void Button_MenuGame_NewGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GameRoom gameRoomWindow = new GameRoom();
                joinToGameClient.LeaveGame(account.nickname);
                gameRoomWindow.ConfigurateWindow(account, 0);
                Close();
                gameRoomWindow.ShowDialog();
            }
            catch (CommunicationException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException);
            }
            catch (TimeoutException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException);
            }
        }

        private void Button_MenuGame_FindGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CodeWindow codeWindow = new CodeWindow();
                codeWindow.ConfigurateWindow(this.account);
                joinToGameClient.LeaveGame(account.nickname);
                Close();
                codeWindow.ShowDialog();
            }
            catch (CommunicationException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException);
            }
            catch (TimeoutException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ServiceException);
            }
        }

        private void ComboBox_MenuGame_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_MenuGame_AccountConfiguration_Click(object sender, RoutedEventArgs e)
        {
            AccountInformation accountInformation = new AccountInformation();
            accountInformation.ConfigurateWindow(account);
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            accountInformation.ShowDialog();
            this.Effect = null;
        }

        private void Button_MenuGame_FriendList_Click(object sender, RoutedEventArgs e)
        {
            Expander_MenuGame_Configurations.IsExpanded = false;
            StackPanel_MenuGame_FriendList.Visibility = Visibility.Visible;
        }

        private void Image_MenuGame_CloseFriendList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Expander_MenuGame_Configurations.IsExpanded = false;
            StackPanel_MenuGame_FriendList.Visibility= Visibility.Hidden;
        }

        private void Button_MenuGame_AddFriend_Click(object sender, RoutedEventArgs e)
        {
            SendFriendRequest sendFriendRequest = new SendFriendRequest ();
            sendFriendRequest.ConfigurateWindow(account);
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            sendFriendRequest.ShowDialog();
            this.Effect = null;
        }

        public void ReciveFriendRequest(string senderNickname)
        {
           /* MessageBoxResult result = MessageBox.Show(senderNickname + " Te ha enviado una solicitud de amistad \n ¿Deseas aceptarla?", "FriendRequest", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                if (!joinToGameClient.AcceptFriendRequest(senderNickname, account.nickname))
                {
                    MessageBox.Show("Ha ocurrido un error, vuelve a intentarlo");
                }
            }*/
        }

        public void ReciveFriendRequest(string senderNickname, string[] friendRequestList)
        {
            throw new NotImplementedException();
        }

        public void SetFriendList(string[] friendList)
        {
            throw new NotImplementedException();
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

        public void GoToGameWindow()
        {
            throw new NotImplementedException();
        }
    }
}
