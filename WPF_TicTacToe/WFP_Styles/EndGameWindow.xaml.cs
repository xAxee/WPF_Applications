using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WFP_Styles
{
    /// <summary>
    /// Logika interakcji dla klasy EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {
        public EndGameWindow(String whoWin)
        {
            InitializeComponent();

            win.Content = "Wygrywa: " + whoWin;
            Closing += this.OnWindowClosing;
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            this.Owner.Close();
        }
    }
}
