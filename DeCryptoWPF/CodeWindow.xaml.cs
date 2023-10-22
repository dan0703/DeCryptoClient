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
                GameRoom gameRoomWindow = new GameRoom();
                gameRoomWindow.ConfigurateWindow(account, code);
                Close();
                gameRoomWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No existe ninguna partida que coincida con el codigo ingresado \n Por favor, intenta con otro","Room",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        public void RecivePlayers(Dictionary<string, byte[]> profiles)
        {
            throw new System.NotImplementedException();
        }

        internal void ConfigurateWindow(Account account)
        {
            this.account = account;
        }
    }
}
