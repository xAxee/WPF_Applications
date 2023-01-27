using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point point = new Point();
        bool draw_state = false;

        TextBlock text_result;
        bool text_state = false;
        public MainWindow()
        {
            InitializeComponent();

            paint.MouseDown += (s, e) =>
            {
                if (text_state)
                {
                    drawText(e);
                } else
                {
                    point = e.GetPosition(paint);
                    draw_state = true;
                }
            };
            paint.MouseUp += (s, e) =>
            {
                draw_state = false;
            };
            paint.MouseMove += (s, e) =>
            {
                if (draw_state)
                {
                    drawLine(e);
                }
            };
            text.Click += (s, e) =>
            {
                draw_state = false;
                text_state = true;
                TextWindow textWindow = new TextWindow();
                textWindow.Owner = this;
                textWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (textWindow.ShowDialog() == true)
                {
                    text_result = textWindow.Answer();
                }
            };
        }
        public void drawText(MouseEventArgs e)
        {
            TextBlock txt1 = text_result;
            Canvas.SetTop(txt1, e.GetPosition(paint).Y);
            Canvas.SetLeft(txt1, e.GetPosition(paint).X);
            paint.Children.Add(txt1);
            text_state = false;
            point = e.GetPosition(paint);
            draw_state = true;
        }
        public void drawLine(MouseEventArgs e)
        {
            Line line = new Line();

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = getColor();
            
            line.StrokeThickness = getThickness();
            line.Stroke = brush;

            line.X1 = point.X;
            line.Y1 = point.Y;
            line.X2 = e.GetPosition(paint).X;
            line.Y2 = e.GetPosition(paint).Y;

            point = e.GetPosition(paint);

            paint.Children.Add(line);
        }
        public Color getColor()
        {
            return color.SelectedColor;
        }
        public int getThickness()
        {
            try
            {
                int t = Int32.Parse(thickness.Text);
                if (t > 0 && t <= 5)
                {
                    return t;
                }
            }
            catch
            {
                MessageBox.Show("Blad parsowania");
                return 1;
            }
            return 1;
        }
    }
}
