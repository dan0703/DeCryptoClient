﻿using DeCryptoWPF.DeCryptoServices;
using log4net;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para InGame.xaml
    /// </summary>
    public partial class InGame : Window, IJoinToGameCallback, IFriendsServicesCallback, IGameServicesCallback
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public readonly JoinToGameClient joinToGameClient;
        public readonly FriendsServicesClient friendsServicesClient;
        public readonly GameServicesClient gameServiceClient;
        private Dictionary<string, byte[]> profiles;
        public GameRoom gameRoomWindow;
        private Account account;
        private int code;
        private BlueTeam blueTeam;
        private RedTeam redTeam;
        private int playerNumber;
        private List<string> wordList;
        private string teamMateNickname;
        private bool isWaitingForGuesses = false;

        public InGame()
        {
            InitializeComponent();
            joinToGameClient = new JoinToGameClient(new InstanceContext(this));
            friendsServicesClient = new FriendsServicesClient(new InstanceContext(this));
            gameServiceClient = new GameServicesClient(new InstanceContext(this));
        }
        public void ConfigurateInGameWindow(GameRoom gameRoomWindow, Account account, int code)
        {
            this.code = code;
            this.gameRoomWindow = gameRoomWindow;
            this.account = account;
            joinToGameClient.JoinToGame(account.nickname, null);
            friendsServicesClient.jointToFriendRequestService(account.nickname);
            joinToGameClient.JoinToRoom(this.code, account.nickname);
        }
        private void SetPlayerNumber()
        {
            if (playerNumber == 0 && redTeam!=null)
            {
                if (redTeam.nicknamePlayer1 == account.nickname)
                {
                    playerNumber = 1;
                    var words=gameServiceClient.GetRedTeamWords(code);
                    teamMateNickname = redTeam.nicknamePlayer2;
                    if (words != null)
                    {
                        this.wordList = words.ToList();
                        SetRedTeamWords();
                        gameServiceClient.JoinToGameBoard(profiles[account.nickname], account.nickname, code);
                    }
                }
                else if (redTeam.nicknamePlayer2 == account.nickname)
                {
                    playerNumber = 2;
                    var words = gameServiceClient.GetRedTeamWords(code);
                    teamMateNickname = redTeam.nicknamePlayer1;
                    if (words != null)
                    {
                        this.wordList = words.ToList();
                        SetRedTeamWords();
                        gameServiceClient.JoinToGameBoard(profiles[account.nickname], account.nickname, code);
                    }
                }
            }
            if(playerNumber == 0 && blueTeam != null)
            {
                if (blueTeam.nicknamePlayer1 == account.nickname)
                {
                    playerNumber = 3;
                    var words = gameServiceClient.GetBlueTeamWords(code);
                    teamMateNickname = blueTeam.nicknamePlayer2;
                    if (words != null)
                    {
                        this.wordList = words.ToList();
                        SetBlueTeamWords();
                        gameServiceClient.JoinToGameBoard(profiles[account.nickname], account.nickname, code);

                    }
                }
                else if (blueTeam.nicknamePlayer2 == account.nickname)
                {
                    playerNumber = 4;
                    var words = gameServiceClient.GetBlueTeamWords(code);
                    teamMateNickname = blueTeam.nicknamePlayer1;
                    if (words != null)
                    {
                        this.wordList = words.ToList();
                        SetBlueTeamWords();
                        gameServiceClient.JoinToGameBoard(profiles[account.nickname], account.nickname, code);

                    }
                }
            }
            
        }

        private void SetBlueTeamWords()
        {
            if (this.wordList == null)
            {
                MessageBox.Show("Error");
            }
            else
            {
                TextBlock_InGame_Word1Blue.Text = wordList[0];
                TextBlock_InGame_Word2Blue.Text = wordList[1];
                TextBlock_InGame_Word3Blue.Text = wordList[2];
                TextBlock_InGame_Word4Blue.Text = wordList[3];
                SetWordsToGiveClues(wordList);
            }
        }

        private void SetRedTeamWords()
        {
            if (this.wordList == null)
            {
                MessageBox.Show("Error");
            }
            else
            {
                TextBlock_InGame_Word1Red.Text = wordList[0];
                TextBlock_InGame_Word2Red.Text = wordList[1];
                TextBlock_InGame_Word3Red.Text = wordList[2];
                TextBlock_InGame_Word4Red.Text = wordList[3];
                SetWordsToGiveClues(wordList);
            }
        }
        private List<string> wordsToGiveClues = new List<string>();
        private int[] index = new int[4];
        private void SetWordsToGiveClues(List<string> words)
        {
            Random random = new Random();
            var aux = new List<string>(words);
            for (int i = 0; i < 4; i++)
            {
                var aux2 = new List<string>(aux);
                int randomIndex = random.Next(aux2.Count);
                wordsToGiveClues.Add(aux[randomIndex]);
                aux.RemoveAt(randomIndex);
                index[i] = randomIndex;
            }
            TextBlock_InGame_Word1.Text = wordsToGiveClues[0];
            TextBlock_InGame_Word2.Text = wordsToGiveClues[1];
            TextBlock_InGame_Word3.Text = wordsToGiveClues[2];
        }

        private void ConfigurateTeamColor()
        {
            if(playerNumber > 2)
            {
                Button_InGame_GiveClues.Background = new SolidColorBrush(Colors.DeepSkyBlue);
            }
        }

        private void Button_InGame_GiveClues_Click(object sender, RoutedEventArgs e)
        {
            gameServiceClient.MakeWaitForClues(teamMateNickname, code);
            if (StackPanel_InGame_GiveClues.Visibility == Visibility.Collapsed) 
            {
                StackPanel_InGame_GiveClues.Visibility = Visibility.Visible;
                Button_InGame_GiveClues.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_InGame_Save_Click(object sender, RoutedEventArgs e)
        {

            string[] clues = new string[4];
            clues[index[0]] = TextBox_InGame_ClueForWord1.Text;
            clues[index[1]] = TextBox_InGame_ClueForWord2.Text;
            clues[index[2]] = TextBox_InGame_ClueForWord3.Text;            
            
            gameServiceClient.GiveBlueTeamClues(clues, code, account.nickname);
        }

        private void Button_InGame_SaveClues_Click(object sender, RoutedEventArgs e)
        {
            if (!isWaitingForGuesses)
            {
                if (StackPanel_InGame_GiveClues.Visibility == Visibility.Visible)
                {
                    StackPanel_InGame_GiveClues.Visibility = Visibility.Collapsed;
                }

                if (StackPanel_InGame_DecryptClues.Visibility == Visibility.Collapsed)
                {
                    StackPanel_InGame_DecryptClues.Visibility = Visibility.Visible;
                }
            }            
        }

        private void Image_InGame_ReportPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportPlayer reportPlayer = new ReportPlayer(profiles);
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            reportPlayer.ShowDialog();
            this.Effect = null;
        }

        public void RecivePlayers(Dictionary<string, byte[]> profiles)
        {
            this.profiles = profiles;
        }

        public void ReciveBlueTeam(BlueTeam blueTeam)
        {
            this.blueTeam = blueTeam;
            TextBlock_InGame_Player3.Text = blueTeam.nicknamePlayer1;
            TextBlock_InGame_Player4.Text = blueTeam.nicknamePlayer2;
            SetPlayerNumber();
            ConfigurateTeamColor();
        }

        public void ReciveRedTeam(RedTeam redTeam)
        {
            this.redTeam = redTeam;
            TextBlock_InGame_Player1.Text = redTeam.nicknamePlayer1;
            TextBlock_InGame_Player2.Text = redTeam.nicknamePlayer2;
            SetPlayerNumber();
            ConfigurateTeamColor();
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

        public void SetBlueTeamClues(string[] blueTeamClues)
        {
            throw new NotImplementedException();
        }

        public void SetRedTeamClues(string[] redTeamClues)
        {
            throw new NotImplementedException();
        }

        public void SetBlueTeamScore(int blueTeamInterception, int blueTeamMisComunications)
        {
            throw new NotImplementedException();
        }

        public void SetRedTeamScore(int redTeamInterception, int redTeamMisComunications)
        {
            throw new NotImplementedException();
        }
        public void WaitForGuesses()
        {
            isWaitingForGuesses = true;
            Button_InGame_GiveClues.Content = "Esperando a que tu compañero de equipo de las pistas...";
        }

        public void SetBoard(GameConfiguration gameConfiguration)
        {
            TextBlock_InGame_NumberRound.Text = gameConfiguration.roundNumber.ToString();            
        }
    }
}
