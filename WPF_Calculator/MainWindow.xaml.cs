using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Calculator
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        List<Button> buttons_num;
        List<Button> buttons_operator;
        double? first_num;
        double? second_num;
        CalcAction? action;


        private string _result;
        public string result {
            get { return _result; }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindow()
        {

            this.DataContext = this;
            InitializeComponent();


            buttons_num = new List<Button>()
            {
                num_0, num_1, num_2, num_3, num_4, num_5, num_6, num_7, num_8, num_9
            };
            buttons_operator = new List<Button>()
            {
                /*action_reset, action_clear, action_negative, action_point, action_equal */action_divide, action_multiple, action_minus, action_add
            };

            // Number buttons
            buttons_num.ForEach(e =>
            {
                e.Click += (sender, args) =>
                {
                    result += e.Content;
                };
            });

            // Action buttons
            action_clear.Click += (sender, e) =>
            {
                result = "";
            };
            action_reset.Click += (sender, e) =>
            {
                result = "";
                first_num = null;
                second_num = null;
                action = null;
            };
            action_negative.Click += (sender, e) =>
            {
                try
                {
                    double temp_num = double.Parse(result);
                    result = (temp_num*-1).ToString();

                }
                catch (Exception en) { }
            };
            action_point.Click += (sender, e) =>
            {
                if (result.Contains(',')) return;
                if (result.Length == 0) return;
                result += ",";
                
            };
            // Operatorion buttons
            action_equal.Click += (sender, e) =>
            {
                try
                {
                    if (first_num == null || action == null) return;

                    second_num = double.Parse(result);
                    result = "";
                    switch (action)
                    {
                        case CalcAction.Add:
                            result = (first_num+second_num).ToString();
                            break;
                        case CalcAction.Minus:
                            result = (first_num-second_num).ToString();
                            break;
                        case CalcAction.Divide:
                            if(second_num == 0)
                            {
                                MessageBox.Show("Bład: Nie można dzielić przez zero");
                            }
                            result = (first_num/second_num).ToString();
                            break;
                        case CalcAction.Multiple:
                            result = (first_num*second_num).ToString();
                            break;
                    }
                } catch(Exception n)
                {
                    MessageBox.Show("Błąd: Parsowanie");
                }
            };

            buttons_operator.ForEach(e =>
            {
                e.Click += (sender, args) =>
                {
                    try
                    {
                        first_num = double.Parse(result);
                        result = "";
                        switch (e.Name)
                        {
                            case "action_add":
                                action = CalcAction.Add;
                                break;
                            case "action_minus":
                                action = CalcAction.Minus;
                                break;
                            case "action_divide":
                                action = CalcAction.Divide;
                                break;
                            case "action_multiple":
                                action = CalcAction.Multiple;
                                break;
                        }
                    } catch(Exception e)
                    {
                        MessageBox.Show("Błąd: Parsowanie");
                    }
                };
            });
            
            
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
