using DeCryptoWPF.DeCryptoServices;
using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;
using System.Collections.Generic;
using System;
using log4net;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window, IJoinToGameCallback
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private JoinToGameClient joinToGameClient;
        private Account account;

        public CodeWindow()
        {
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            account = new Account();
            InitializeComponent();
        }

        private void Button_CodeWindow_Join_Click(object sender, RoutedEventArgs e)
        {
            int code = int.Parse(TextBox_CodeWindow_Code.Text);
            try
            {
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

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try { 
                MenuGame menuGameWindow = new MenuGame();
                menuGameWindow.ConfigurateWindow(account);
                joinToGameClient.LeaveGame(account.nickname);
                Close();
                menuGameWindow.ShowDialog();
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
