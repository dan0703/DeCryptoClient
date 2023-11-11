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

namespace DeCryptoWPF
{
    /// <summary>
    /// Lógica de interacción para InGame.xaml
    /// </summary>
    public partial class InGame : Window
    {
        public InGame()
        {
            InitializeComponent();
        }

        private void Button_InGame_GiveClues_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel_InGame_GiveClues.Visibility == Visibility.Collapsed) {
                StackPanel_InGame_GiveClues.Visibility = Visibility.Visible;
                Button_InGame_GiveClues.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_InGame_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_InGame_SaveClues_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel_InGame_GiveClues.Visibility == Visibility.Visible)
            {
                StackPanel_InGame_GiveClues.Visibility = Visibility.Collapsed;
            }

            if (StackPanel_InGame_DecryptClues.Visibility == Visibility.Collapsed)
            {
                StackPanel_InGame_DecryptClues.Visibility= Visibility.Visible;
            }
        }

        private void Image_InGame_ReportPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportPlayer reportPlayer = new ReportPlayer();
            this.Effect = new System.Windows.Media.Effects.BlurEffect();
            reportPlayer.ShowDialog();
            this.Effect = null;
        }
    }
}
