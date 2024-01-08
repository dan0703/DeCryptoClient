using DeCryptoWPF.DeCryptoServices;
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
        private bool isWaitingForGuess = false;
        private string[][] blueTeamClues;
        private string[][] redTeamClues;
        private int roundNumber = 0;
        private string myTeam = null;
        private readonly int INTERCEPTION_ROUNDS_NUMBER = 3;
        private bool isInterceptionRound=false;
        private List<string> clueList;

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
            if (playerNumber == 0 && redTeam != null)
            {
                if (redTeam.nicknamePlayer1 == account.nickname)
                {
                    playerNumber = 1;
                    myTeam = "red";
                    var words = gameServiceClient.GetRedTeamWords(code);
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
                    myTeam = "red";
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
            if (playerNumber == 0 && blueTeam != null)
            {
                if (blueTeam.nicknamePlayer1 == account.nickname)
                {
                    playerNumber = 3;
                    myTeam = "blue";
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
                    myTeam = "blue";
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

        private void StartNewRound()
        {
            MessageBox.Show("startNewRound");
            Button_InGame_GiveClues.Visibility = Visibility.Visible;
            StackPanel_InGame_GiveClues.Visibility = Visibility.Collapsed;
            StackPanel_InGame_DecryptClues.Visibility = Visibility.Collapsed;
            Label_InGame_Note.Visibility = Visibility.Collapsed;
            ComboBox_InGame_Clue1.SelectedIndex = 0;
            ComboBox_InGame_Clue2.SelectedIndex = 0;
            ComboBox_InGame_Clue3.SelectedIndex = 0;
            TextBox_InGame_ClueForWord1.Text = "";
            TextBox_InGame_ClueForWord2.Text = "";
            TextBox_InGame_ClueForWord3.Text = "";
            if (isInterceptionRound)
            {
                Button_InGame_GiveClues.Content = "Interceptar Pistas";
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
                if (!wordsToGiveClues.Contains(aux[randomIndex]))
                {
                    wordsToGiveClues.Add(aux[randomIndex]);
                    index[i] = randomIndex;
                }
                else
                {
                    i--;
                }
            }
            TextBlock_InGame_Word1.Text = wordsToGiveClues[0];
            TextBlock_InGame_Word2.Text = wordsToGiveClues[1];
            TextBlock_InGame_Word3.Text = wordsToGiveClues[2];
        }

        private void ConfigurateTeamColor()
        {
            if (playerNumber > 2)
            {
                Button_InGame_GiveClues.Background = new SolidColorBrush(Colors.DeepSkyBlue);
            }
        }

        private void Button_InGame_GiveClues_Click(object sender, RoutedEventArgs e)
        {
            gameServiceClient.MakeWaitForClues(teamMateNickname, code);
            if (isInterceptionRound)
            {
                if (StackPanel_InGame_InterceptClues.Visibility == Visibility.Collapsed)
                {
                    StackPanel_InGame_InterceptClues.Visibility = Visibility.Visible;
                    Button_InGame_GiveClues.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (StackPanel_InGame_GiveClues.Visibility == Visibility.Collapsed)
                {
                    StackPanel_InGame_GiveClues.Visibility = Visibility.Visible;
                    Button_InGame_GiveClues.Visibility = Visibility.Collapsed;
                }
            }
           
        }

        

        private void Button_InGame_SaveClues_Click(object sender, RoutedEventArgs e)
        {
            string[] clues = new string[4];
            clues[wordList.IndexOf(TextBlock_InGame_Word1.Text)] = TextBox_InGame_ClueForWord1.Text;
            clues[wordList.IndexOf(TextBlock_InGame_Word2.Text)] = TextBox_InGame_ClueForWord2.Text;
            clues[wordList.IndexOf(TextBlock_InGame_Word3.Text)] = TextBox_InGame_ClueForWord3.Text;
            if (myTeam.Equals("red"))
            {
                gameServiceClient.GiveRedTeamClues(clues, code, account.nickname);
            }
            else
            {
                gameServiceClient.GiveBlueTeamClues(clues, code, account.nickname);
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

        public void SetBlueTeamScore(int blueTeamInterception, int blueTeamMisComunications)
        {
            if(blueTeam.missComunicationsPoints == blueTeamMisComunications)
            {
                MessageBox.Show("Decifrado exitoso");
            }
            else
            {
                MessageBox.Show("Decifrado fallido");
            }
            blueTeam.interceptionsPoints = blueTeamInterception;
            blueTeam.missComunicationsPoints = blueTeamMisComunications;
            TextBlock_InGame_NumberComunicationsBlue.Text = blueTeamMisComunications.ToString();
            TextBlock_InGame_NumberInterceptionsBlue.Text = blueTeamInterception.ToString();
            WaitingOtherTeam();
        }

        public void SetRedTeamScore(int redTeamInterception, int redTeamMisComunications)
        {
            if (redTeam.missComunicationsPoints == redTeamMisComunications)
            {
                MessageBox.Show("Decifrado exitoso");
            }
            else
            {
                MessageBox.Show("Decifrado fallido");
            }
            redTeam.interceptionsPoints = redTeamInterception;
            redTeam.missComunicationsPoints = redTeamMisComunications;
            TextBlock_InGame_NumberComunicationsRed.Text = redTeam.missComunicationsPoints.ToString();
            TextBlock_InGame_NumberInterceptionsRed.Text = redTeam.interceptionsPoints.ToString();
            WaitingOtherTeam();
        }

        private void WaitingOtherTeam()
        {
            StackPanel_InGame_DecryptClues.Visibility = Visibility.Collapsed;
            StackPanel_InGame_GiveClues.Visibility = Visibility.Collapsed;
            Label_InGame_Note.Visibility = Visibility.Visible;
            Label_InGame_Note.Content = "Esperando al otro equipo...";
        }
        public void WaitForGuesses()
        {
            if (isInterceptionRound)
            {
                Label_InGame_Note.Content = "Espera a que tu compañero de equipo intercepte las pistas...";
            }
            else
            {
                isWaitingForGuess = true;
                Label_InGame_Note.Content = "Esperando a que tu compañero de equipo de las pistas...";
            }
            Label_InGame_Note.Visibility = Visibility.Visible;
            Button_InGame_GiveClues.Visibility = Visibility.Collapsed;

        }
        public void WaitForTeamToGuess()
        {
            Label_InGame_Note.Content = "Esperando a que tu compañero de equipo descifre las pistas...";
            Label_InGame_Note.Visibility = Visibility.Visible;
            Button_InGame_GiveClues.Visibility = Visibility.Collapsed;
            StackPanel_InGame_GiveClues.Visibility = Visibility.Collapsed;
        }

        public void GuessRedTeamWords()
        {
            string[] auxiliarClues = new string[3];
            if (redTeamClues != null)
            {
                foreach (var clues in redTeamClues)
                {
                    int count = 0;
                    if (clues != null)
                    {
                        clueList = clues.ToList();
                        foreach (var clue in clues)
                        {
                            if (clue != null)
                            {
                                auxiliarClues[count] = clue;
                                count++;
                            }
                        }
                        TextBlock_InGame_SelectWord1.Text = auxiliarClues[0];
                        TextBlock_InGame_SelectWord2.Text = auxiliarClues[1];
                        TextBlock_InGame_SelectWord3.Text = auxiliarClues[2];
                    }
                }
            }
        }

        public void DecryptRedTeamWords()
        {
            string[] auxiliarClues = new string[3];
            if (blueTeamClues != null)
            {
                foreach (var clues in blueTeamClues)
                {
                    int count = 0;
                    if (clues != null)
                    {
                        clueList = clues.ToList();
                        foreach (var clue in clues)
                        {
                            if (clue != null)
                            {
                                auxiliarClues[count] = clue;
                                count++;
                            }
                        }
                        TextBlock_InGame_SelectWord1_Intercept.Text = auxiliarClues[0];
                        TextBlock_InGame_SelectWord2_Intercept.Text = auxiliarClues[1];
                        TextBlock_InGame_SelectWord3_Intercept.Text = auxiliarClues[2];
                    }
                }
            }



        }

        public void DecryptBlueTeamWords()
        {
            if (redTeamClues != null)
            {
                string[] auxiliarClues = new string[3];
                if (redTeamClues != null)
                {
                    foreach (var clues in redTeamClues)
                    {
                        int count = 0;
                        if (clues != null)
                        {
                            clueList = clues.ToList();
                            foreach (var clue in clues)
                            {
                                if (clue != null)
                                {
                                    auxiliarClues[count] = clue;
                                    count++;
                                }
                            }
                            TextBlock_InGame_SelectWord1_Intercept.Text = auxiliarClues[0];
                            TextBlock_InGame_SelectWord2_Intercept.Text = auxiliarClues[1];
                            TextBlock_InGame_SelectWord3_Intercept.Text = auxiliarClues[2];
                        }
                    }
                }
            }
        }


        public void GuessBlueTeamWords()
        {
            if (blueTeamClues != null)
            {
                string[] auxiliarClues = new string[3];
                if (blueTeamClues != null)
                {
                    foreach (var clues in blueTeamClues)
                    {
                        int count = 0;
                        if (clues != null)
                        {
                            clueList = clues.ToList();
                            foreach (var clue in clues)
                            {
                                if (clue != null)
                                {
                                    auxiliarClues[count] = clue;
                                    count++;
                                }
                            }
                            TextBlock_InGame_SelectWord1.Text = auxiliarClues[0];
                            TextBlock_InGame_SelectWord2.Text = auxiliarClues[1];
                            TextBlock_InGame_SelectWord3.Text = auxiliarClues[2];
                        }
                    }
                }
            }
        }


        public void SetBoard(GameConfiguration gameConfiguration)
        {
            roundNumber = gameConfiguration.roundNumber;            
            TextBlock_InGame_NumberRound.Text = roundNumber.ToString();
            SetCluesInBoard();
            if (gameConfiguration.roundNumber > 1)
            {
                int result = roundNumber / INTERCEPTION_ROUNDS_NUMBER;
                if (gameConfiguration.roundNumber > 8)
                {
                    MessageBox.Show("El juego ha finalizado");
                    //mandar a llamar a un metodo para finalizar el juego
                }
                else
                {
                    if (result == 1 || result == 2)
                    {
                        isInterceptionRound = true;
                        blueTeamClues = gameConfiguration.blueTeam.clues;
                        redTeamClues = gameConfiguration.redTeam.clues;
                        MessageBox.Show("is interception round");
                        DecryptRedTeamWords();
                        DecryptBlueTeamWords();
                    }
                    else
                    {
                        MessageBox.Show("Next Round");
                        isInterceptionRound = false;
                    }
                    StartNewRound();
                }
            }                        
        }

        private void StartInterceptionRound()
        {
            StackPanel_InGame_InterceptClues.Visibility = Visibility.Visible;
        }

        public void SetCluesInBoard()
        {
            if (blueTeamClues != null)
            {
                foreach (var clues in blueTeamClues)
                {
                    if (clues != null)
                    {
                        TextBlock_InGame_Word1Blue.Text = TextBlock_InGame_Word1Blue.Text + "\n" + clues[0];
                        TextBlock_InGame_Word2Blue.Text = TextBlock_InGame_Word2Blue.Text + "\n" + clues[1];
                        TextBlock_InGame_Word3Blue.Text = TextBlock_InGame_Word3Blue.Text + "\n" + clues[2];
                        TextBlock_InGame_Word4Blue.Text = TextBlock_InGame_Word4Blue.Text + "\n" + clues[3];
                    }
                }
            }
            if (redTeamClues != null)
            {
                foreach (var clues in redTeamClues)
                {
                    if (clues != null)
                    {
                        TextBlock_InGame_Word1Red.Text = TextBlock_InGame_Word1Red.Text + "\n" + clues[0];
                        TextBlock_InGame_Word2Red.Text = TextBlock_InGame_Word2Red.Text + "\n" + clues[1];
                        TextBlock_InGame_Word3Red.Text = TextBlock_InGame_Word3Red.Text + "\n" + clues[2];
                        TextBlock_InGame_Word4Red.Text = TextBlock_InGame_Word4Red.Text + "\n" + clues[3];
                    }
                }
            }
        }

        public void SetBlueTeamClues(string[][] blueTeamClues)
        {
            this.blueTeamClues = blueTeamClues;
            GuessBlueTeamWords();

            if (isWaitingForGuess)
            {
                isWaitingForGuess = false;
                StackPanel_InGame_DecryptClues.Visibility = Visibility.Visible;
                Label_InGame_Note.Visibility = Visibility.Collapsed;
            }
            else
            {
                WaitForTeamToGuess();
            }
        }

        public void SetRedTeamClues(string[][] redTeamClues)
        {
            this.redTeamClues = redTeamClues;
            GuessRedTeamWords();

            if (isWaitingForGuess)
            {
                isWaitingForGuess = false;
                StackPanel_InGame_DecryptClues.Visibility = Visibility.Visible;
                Label_InGame_Note.Visibility= Visibility.Collapsed;
            }
            else
            {
                WaitForTeamToGuess();
            }
        }

        private void Button_InGame_Save_Click(object sender, RoutedEventArgs e)
        {
            int clueIndex1 = clueList.IndexOf(TextBlock_InGame_SelectWord1.Text);
            int clueIndex2 = clueList.ToList().IndexOf(TextBlock_InGame_SelectWord2.Text);
            int clueIndex3 = clueList.ToList().IndexOf(TextBlock_InGame_SelectWord3.Text);
            bool isCorrectDecrypt = false;
            if (clueIndex1 == ComboBox_InGame_Clue1.SelectedIndex)
            {
                if (clueIndex2 == ComboBox_InGame_Clue2.SelectedIndex)
                {
                    if (clueIndex3 == ComboBox_InGame_Clue3.SelectedIndex)
                    {
                        isCorrectDecrypt = true;
                    }
                }
            }
            if (myTeam.Equals("red"))
            {               
                gameServiceClient.SubmitRedTeamDecryptResult(isCorrectDecrypt, code);
            }
            else
            {
                gameServiceClient.SubmitBlueTeamDecryptResult(isCorrectDecrypt, code);
            }
        }

        private void Button_InGame_Save_Intercept_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
