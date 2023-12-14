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
    public partial class CodeWindow : Window, IFriendsServicesCallback
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private RoomServicesClient roomServicesClient;
        private Account account;

        public CodeWindow()
        {
            roomServicesClient = new RoomServicesClient();
            account = new Account();
            InitializeComponent();
        }

        private void Button_CodeWindow_Join_Click(object sender, RoutedEventArgs e)
        {
            int code = int.Parse(TextBox_CodeWindow_Code.Text);
            try
            {
                if (roomServicesClient.AllreadyExistRoom(code))
                {
                    if (roomServicesClient.IsFullRoom(code))
                    {
                        MessageBox.Show(Properties.Resources.MessageBox_MenuGame_RoomIsFull, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                    MessageBox.Show("la room no existe");
                    MessageBox.Show(Properties.Resources.MessageBox_MenuGame_RoomNotFound, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try { 
                MenuGame menuGameWindow = new MenuGame();
                menuGameWindow.ConfigurateWindow(account);
                Close();
                menuGameWindow.ShowDialog();
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

        internal void ConfigurateCodeWindow(Account account)
        {
            this.account = account;
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
    }
}
