using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        List<Button> buttonList;
        private bool click = true;
        public GameWindow()
        {
            InitializeComponent();
            Closing += this.OnWindowClosing;

            buttonList = new List<Button>()
            {
                pole_1, pole_2, pole_3, pole_4,pole_5,pole_6, pole_7, pole_8, pole_9
            };

            buttonList.ForEach(e =>
            {
                e.Content = "";
                e.Click += (sender, evnt) =>
                {
                    if (e.Content == "")
                    {
                        if(click)
                        {
                            e.Content = "O";
                        } else
                        {
                            e.Content = "X";
                        }
                        click = !click;
                        move.Content = click ? "Ruch: O" : "Ruch: X";
                    }
                    checkWin();
                };
            });
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            this.Owner.Show();
        }
        public void checkWin()
        {
            if (isWinner("O"))
            {
                EndGameWindow endGame = new EndGameWindow("O");
                endGame.Owner = this;
                endGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                endGame.Show();
            }
            if(isWinner("X"))
            {
                EndGameWindow endGame = new EndGameWindow("O");
                endGame.Owner = this;
                endGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                endGame.Show();
            }
        }
        public bool isWinner(string player)
        {
            if (pole_1.Content == player && pole_1.Content == pole_2.Content && pole_1.Content == pole_2.Content) return true;
            if (pole_4.Content == player && pole_4.Content == pole_5.Content && pole_4.Content == pole_6.Content) return true;
            if (pole_7.Content == player && pole_7.Content == pole_8.Content && pole_7.Content == pole_9.Content) return true;
            if (pole_1.Content == player && pole_1.Content == pole_5.Content && pole_1.Content == pole_9.Content) return true;
            if (pole_3.Content == player && pole_3.Content == pole_5.Content && pole_3.Content == pole_7.Content) return true;
            if (pole_1.Content == player && pole_1.Content == pole_4.Content && pole_1.Content == pole_7.Content) return true;
            if (pole_2.Content == player && pole_2.Content == pole_5.Content && pole_2.Content == pole_8.Content) return true;
            if (pole_3.Content == player && pole_3.Content == pole_6.Content && pole_3.Content == pole_9.Content) return true;
            return false;
        } 
    }
}
