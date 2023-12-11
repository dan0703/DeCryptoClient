using DeCryptoWPF.DeCryptoServices;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para SendFriendRequest.xaml
    /// </summary>
    public partial class SendFriendRequest : Window, IJoinToGameCallback
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Account account;
        private readonly JoinToGameClient joinToGameClient;
        private readonly PlayerServicesClient playerServicesClient;
        private readonly FriendsServicesClient friendsServicesClient;

        public SendFriendRequest()
        {
            InitializeComponent();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            playerServicesClient = new PlayerServicesClient();
            friendsServicesClient = new FriendsServicesClient(new InstanceContext(this));
        }
        public void ConfigurateWindow(Account account)
        {
            this.account = account;
        }
        
        private void Button_SendFriendRequest_SendRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (playerServicesClient.ExistNickname(TextBox_SendFriendRequest_NickName.Text))
                {
                    if(TextBox_SendFriendRequest_NickName.Text != account.nickname)
                    {
                        friendsServicesClient.SendFriendRequest(account.nickname, TextBox_SendFriendRequest_NickName.Text);
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    MessageBox.Show("No se ha encontrado el nickname proporcionado, intenta con otro");
                }

            }
            catch (CommunicationException ex)
            {
                log.Error(ex);
                MessageBox.Show("El servicio no se encuentra disponible");
            }
            catch (TimeoutException ex)
            {
                log.Error(ex);
                MessageBox.Show("El servicio no se encuentra disponible");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show("El servicio no se encuentra disponible");
            }
        }

        private void Button_SendFriendRequest_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        public void ReciveFriendRequest(string senderNickname)
        {
            /*MessageBoxResult result = MessageBox.Show(senderNickname + " Te ha enviado una solicitud de amistad \n ¿Deseas aceptarla?", "FriendRequest", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                if(!joinToGameClient.AcceptFriendRequest(senderNickname, account.nickname))
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

        public void GoToGameWindow()
        {
            throw new NotImplementedException();
        }
    }
}
