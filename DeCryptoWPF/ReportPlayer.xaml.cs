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
    /// Lógica de interacción para ReportPlayer.xaml
    /// </summary>
    public partial class ReportPlayer : Window
    {
        private Dictionary<string, byte[]> profiles;

        public ReportPlayer(Dictionary<string, byte[]> profiles)
        {
            InitializeComponent();
            this.profiles = profiles;
        }

        private void Button_Confirmations_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Confirmations_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
