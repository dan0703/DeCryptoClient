using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para ReceiveFriendRequest.xaml
    /// </summary>
    public partial class ReceiveFriendRequest : Window
    {
        private DispatcherTimer timer;
        private int currentSecond;
        private string senderNickname;
        public ReceiveFriendRequest(string senderNickname)
        {
            InitializeComponent();
            this.senderNickname = senderNickname;
            Label_ReceiveFriendRequest_SenderNickname.Content = senderNickname + "te ha enviado una solicitud de amistad";
            CloseWindow();
        }

        private void Button_ReceiveFriendRequest_Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ReceiveFriendRequest_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseWindow()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;
            ProgressBar_ReceiverFriendRequest.Maximum = 100;
            timer.Start();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            currentSecond++;
            UpdateProgressBar();
            if (currentSecond >= 10)
            {
                timer.Stop();
                ProgressBar_ReceiverFriendRequest.Value = ProgressBar_ReceiverFriendRequest.Maximum;
            }
        }

        private void UpdateProgressBar()
        {
            double currentValue = (double)currentSecond / 10 * 100;
            currentValue = Math.Min(currentValue, ProgressBar_ReceiverFriendRequest.Maximum);
            ProgressBar_ReceiverFriendRequest.Value = currentValue;
        }
    }
}
