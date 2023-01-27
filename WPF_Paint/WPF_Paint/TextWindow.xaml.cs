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

namespace WPF_Paint
{
    /// <summary>
    /// Logika interakcji dla klasy TextWindow.xaml
    /// </summary>
    public partial class TextWindow : Window
    {
        TextBlock textBlock = new TextBlock();
        public TextWindow()
        {
            InitializeComponent();

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = input.Text;
            try
            {
                int fontSize = Convert.ToInt32(size.Text);
                textBlock.FontSize = fontSize;
            }
            catch { }
            this.DialogResult = true;
        }
        private void bold_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBlock.FontWeight == FontWeights.Bold)
            {
                textBlock.FontWeight = FontWeights.Normal;
            }
            else
            {
                textBlock.FontWeight = FontWeights.Bold;
            }
            input.FontWeight = textBlock.FontWeight;
        }

        private void italic_Click(object sender, RoutedEventArgs e)
        {
            if (textBlock.FontStyle == FontStyles.Italic)
            {
                textBlock.FontStyle = FontStyles.Normal;
            }
            else
            {
                textBlock.FontStyle = FontStyles.Italic;
            }
            input.FontStyle = textBlock.FontStyle;
        }
        public TextBlock Answer()
        {
            return textBlock;
        }
    }
}
