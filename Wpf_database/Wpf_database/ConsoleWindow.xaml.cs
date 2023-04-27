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

namespace Wpf_database {
    /// <summary>
    /// Logika interakcji dla klasy ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window {
        public ConsoleWindow() {
            InitializeComponent();
            this.WriteLine("Create console instance");
        }

        public void WriteLine(string text) {
            Console.Text += $"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {text} \n";
        }
        public void Write(string text) {
            Console.Text += text;
        }
    }
}
