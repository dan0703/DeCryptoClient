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
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using log4net;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para GameRoom.xaml
    /// </summary>
    public partial class GameRoom : Window, IJoinToGameCallback, IChatMessageCallback
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly JoinToGameClient joinToGameClient;
        private readonly ChatMessageClient chatMessageClient;
        private int code;
        private Account account;
        private BlueTeam blueTeam;
        private RedTeam redTeam;
        private string profileImagePath = string.Empty;

        public GameRoom()
        {
            account = new Account();
            blueTeam = new BlueTeam();
            redTeam = new RedTeam();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            chatMessageClient = new ChatMessageClient(new InstanceContext(this));
            InitializeComponent();
            Closing += GameRoom_ClosingAsync;
        }

        private bool isNavegatingToGameBoardWindow = false;

        private  void GameRoom_ClosingAsync(object sender, CancelEventArgs e)
        {            
            if (!isNavegatingToGameBoardWindow)
            {
                joinToGameClient.LeaveGame(account.nickname);
                joinToGameClient.LeaveRoom(account.nickname, code, blueTeam, redTeam);                
                chatMessageClient.LeaveChat(account.nickname, code);                
            }            
        }

        public void GoToGameWindow()
        {
            isNavegatingToGameBoardWindow = true;
            InGame inGameWindow = new InGame();
            inGameWindow.ConfigurateWindow(this, account, code);
            Close();
            inGameWindow.Show();                       
        }

        public void ConfigurateWindow(Account account, int code)
        {
            try
            {
                if (code > 1)
                {
                    this.code = code;
                }
                else
                {
                    this.code = joinToGameClient.CreateRoom();
                }
                Label_GameRoom_Code.Content = this.code;
                this.account = account;
                ConfiurateProfilePicture();
                byte[] profileImageArrayBytes = ImageToByte(profileImagePath);
                joinToGameClient.JoinToGame(account.nickname, profileImageArrayBytes);
                joinToGameClient.JoinToRoom(this.code, account.nickname);
                chatMessageClient.JoinChat(account.nickname, code);
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

        private void ConfiurateProfilePicture()
        {
            this.profileImagePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../../Images/" + account.nickname + ".png";
            if (!File.Exists(profileImagePath))
            {
                this.profileImagePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../../Resources/defaultProfilePicture.png";
            }
        }

        public byte[] ImageToByte(string imagePath)
        {
            byte[] image = null;
            try
            {
                if (File.Exists(imagePath))
                {
                    using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            image = reader.ReadBytes((int)fileStream.Length);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("La imagen no existe en la ubicación especificada.");
                }
            }
            catch (FileNotFoundException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(Properties.Resources.MessageBox_Error_ErrorService, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return image;
        }

        private void Button_GameRoom_TeamRead_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBlock_GameRoom_Player1.Text != "Player1") && (TextBlock_GameRoom_Player2.Text != "Player2"))
            {
                MessageBox.Show(Properties.Resources.MessageBox_MenuGame_RoomIsFull, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
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
        }

        private void Button_GameRoom_TeamBlue_Click(object sender, RoutedEventArgs e)
        {
             if((TextBlock_GameRoom_Player3.Text != "Player3") && (TextBlock_GameRoom_Player4.Text != "Player4"))
             {
                MessageBox.Show(Properties.Resources.MessageBox_MenuGame_RoomIsFull, "DeCrypto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
             }
            else
             {
                try
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
        }

        private void Button_GameRoom_StartGame_Click(object sender, RoutedEventArgs e)
        {            
            joinToGameClient.StartGame(code);
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

        private void Image_GameRoom_SendMessage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            if (TextBox_GameRoom_WriteMessage.Text != "")
            {
                if (Label_GameRoom_BlankMessage.Visibility == Visibility.Visible)
                {
                    Label_GameRoom_BlankMessage.Visibility = Visibility.Collapsed;
                }

                try
                {
                    ChatMessage chatMessage = new ChatMessage();
                    chatMessage.nickname = account.nickname;
                    chatMessage.message = TextBox_GameRoom_WriteMessage.Text;
                    chatMessage.time = DateTime.Now.ToString("HH:mm");
                    chatMessageClient.SendMessage(chatMessage, this.code);
                    TextBox_GameRoom_WriteMessage.Text = "";
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
            else
            {
                Label_GameRoom_BlankMessage.Visibility = Visibility.Visible;
            }
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
                Grid playerGrid = new Grid();

                Label Label_Player = new Label();
                Label_Player.Content = profile.Key;
                Label_Player.Foreground = Brushes.White;
                Label_Player.FontSize = 30;

                Image playerImage = BytesToImage(profile.Value);

                ColumnDefinition column1 = new ColumnDefinition();
                ColumnDefinition column2 = new ColumnDefinition();
                column1.Width = new GridLength(200, GridUnitType.Pixel);
                column2.Width = new GridLength(1, GridUnitType.Star);

                playerGrid.ColumnDefinitions.Add(column1);
                playerGrid.ColumnDefinitions.Add(column2);

                playerImage.Width = 60;
                playerImage.Height = 60;

                Grid.SetColumn(Label_Player, 0);
                Grid.SetColumn(playerImage, 1);

                playerGrid.Children.Add(Label_Player);
                playerGrid.Children.Add(playerImage);

                StackPanel_GameRoom_PlayerList.Children.Add(playerGrid);
            }
        }

        private Image BytesToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.DecodePixelWidth = 60;
                bitmapImage.DecodePixelHeight = 60;
                bitmapImage.EndInit();
            }

            Image image = new Image();
            image.Source = bitmapImage;

            return image;
        }

        public void ReceiveChatMessages(ChatMessage[] messages)
        {
            ListBox_GameRoom_ChatMessages.Items.Clear();
            foreach (var message in messages) 
            {
                Label Label_Player = new Label();
                Label_Player.Content = message.nickname;
                Label_Player.Foreground = Brushes.Orange;
                Label_Player.FontSize = 15;
                ListBox_GameRoom_ChatMessages.Items.Add(Label_Player);

                Label Label_Message = new Label();
                Label_Message.Content = message.message;
                Label_Message.Foreground = Brushes.Black;
                Label_Message.FontSize = 25;
                ListBox_GameRoom_ChatMessages.Items.Add(Label_Message);

                Label Label_Time = new Label();
                Label_Time.Content = message.time;
                Label_Time.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#292B30"));
                Label_Time.FontSize = 10;
                ListBox_GameRoom_ChatMessages.Items.Add(Label_Time);
            }
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
