﻿using DeCryptoWPF.DeCryptoServices;
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
        private Account account;
        private JoinToGameClient joinToGameClient;
        private PlayerServicesClient playerServicesClient;

        public SendFriendRequest()
        {
            InitializeComponent();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            playerServicesClient = new PlayerServicesClient();
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
                    joinToGameClient.SendFriendRequest(account.nickname, TextBox_SendFriendRequest_NickName.Text);
                }
                else
                {
                    MessageBox.Show("No se ha encontrado el nickname proporcionado, intenta con otro");
                }

            }
            catch(Exception )
            {
                MessageBox.Show("El servicion no se encuentra disponible, intentelo mas tarde");
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
            MessageBoxResult result = MessageBox.Show(senderNickname + " Te ha enviado una solicitud de amistad \n ¿Deseas aceptarla?", "FriendRequest", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                if(!joinToGameClient.AcceptFriendRequest(senderNickname, account.nickname))
                {
                    MessageBox.Show("Ha ocurrido un error, vuelve a intentarlo");
                }
            }
        }
    }
}