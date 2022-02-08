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

namespace Lab_01
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        public string a;
        public string znak;
        private void B1_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "1";
        }

        private void B2_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "2";
        }

        private void B3_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "3";
        }

        private void B4_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "4";
        }

        private void B5_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "5";
        }

        private void B6_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "6";
        }

        private void B7_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "7";
        }

        private void B8_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "8";
        }

        private void B9_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "9";
        }

        private void B10_click(object sender, RoutedEventArgs e)
        {
            TB.Text += "0";
        }

        private void B11_click(object sender, RoutedEventArgs e)
        {
            if (TB.Text != "")
            {
                znak = "+";
                a = TB.Text;
                TB.Text = "";

            }
        }

        private void B12_click(object sender, RoutedEventArgs e)
        {
            if (TB.Text != "")
            {
                a = TB.Text;
                TB.Text = "";
                znak = "-";
            }
        }

        private void B13_click(object sender, RoutedEventArgs e)
        {
            if (TB.Text != "")
            {
                a = TB.Text;
                TB.Text = "";
                znak = "*";
            }
        }

        private void B14_click(object sender, RoutedEventArgs e)
        {
            if(TB.Text != "")
            {
                a = TB.Text;
                TB.Text = "";
                znak = "/";
            }
        }

        private void B15_click(object sender, RoutedEventArgs e)
        {
            TB.Text = "";
        }

        private void B16_click(object sender, RoutedEventArgs e)
        {
            double b = 0;
            switch (znak)
            {
                case "+":
                    b = Convert.ToDouble(a) + Convert.ToDouble(TB.Text);
                    break;
                case "-":
                    b = Convert.ToDouble(a) - Convert.ToDouble(TB.Text);
                    break;
                case "/":
                    b = Convert.ToDouble(a) * 1.0 / Convert.ToDouble(TB.Text);
                    break;
                case "*":
                    b = Convert.ToDouble(a) * Convert.ToDouble(TB.Text);
                    break;
            }
            TB.Text = b.ToString();
            znak = "";
        }

        private void B17_click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void B18_click(object sender, RoutedEventArgs e)
        {
            if(TB.Text != "")
            {
                TB.Text += ",";
            } else
            {
                TB.Text = "0,";
            }
        }

        private void B19_click(object sender, RoutedEventArgs e)
        {
            if(TB.Text[0] != '-')
            {
                TB.Text = "-" + TB.Text;
            } else
            {
                TB.Text = TB.Text.Remove(0, 1);
            }
        }
    }
}
