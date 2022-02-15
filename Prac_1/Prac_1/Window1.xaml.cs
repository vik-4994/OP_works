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
using System.IO;

namespace Prac_1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        public List<long> timeStamps = new List<long>();

        public void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        public void TextBox_PreviewKeyUp(object sender, RoutedEventArgs e)
        {
            //string filePath = @"\\vmware-host\Shared Folders\Рабочий стол\Data.txt";
            List<double> intervals = new List<double>();
            DateTimeOffset dto = DateTimeOffset.Now;
            long timeStamp = dto.ToUnixTimeMilliseconds();
            if(InputField.Text != "")
            {
                if (VerifField.Text == InputField.Text)
                {
                    SymbolCount.Content = 0;
                    timeStamps.Add(timeStamp);
                    Counter_LB.Content = Convert.ToInt32(Counter_LB.Content) + 1;
                    ComboBoxItem item = (ComboBoxItem)CountProtection.SelectedValue;

                    if (Counter_LB.Content.ToString() == item.Content.ToString())
                    {
                        InputField.IsEnabled = false;
                    }
                    InputField.Text = "";

                    for(int i = 0; i < timeStamps.Count() - 1; i++)
                    {
                        intervals.Add(Convert.ToDouble((timeStamps[i+1] - timeStamps[i] * 1.0)/1000));
                    }
                    LB.Content = intervals[0];
                    timeStamps.Clear();
                    Calc(intervals);
                } else
                {
                    if (VerifField.Text.Contains(InputField.Text))
                    {
                        SymbolCount.Content = Convert.ToString(Convert.ToDouble(SymbolCount.Content) + 1);
                        timeStamps.Add(timeStamp);
                    } else
                    {
                        timeStamps.Clear();
                        InputField.Text = "";
                        SymbolCount.Content = 0;
                    }
                }
            }
        }

        public void Calc(List<double> a)
        {
            List<double> matSpod = new List<double>();
            string filePath = @"\\vmware-host\Shared Folders\Рабочий стол\Data.txt";
            double b = 0;
            double c;
            double d = 0;
            double e;
            double g;
            double tt = 2.26;
            double tp;
            bool bol = false;
            for(int i = 0; i < a.Count(); i++)
            {
                for(int j = 0; j < a.Count(); j++)
                {
                    if(j != i)
                    {
                        b += a[j];
                    }
                }
                c = b / (a.Count() - 1);
                matSpod.Add(c);
                for(int j = 0; j < a.Count; j++)
                {
                    if(j != i)
                    {
                        d += Math.Pow(a[j] - c, 2);
                    }
                }
                e = d / (a.Count() - 1);
                g = Math.Sqrt(e);
                tp = Math.Abs((a[i] - c) / g);
                b = 0;
                d = 0;
                if(tp > tt)
                {
                    LB.Content = $"{a[i]} {i} {e} {tp}";
                    break;
                }
                if (i == a.Count() - 1)
                {
                    bol = true;
                }
            }
            if (bol)
            {
                LB.Content = "OK";
                for(int i = 0; i < a.Count(); i++)
                {
                    b += a[i];
                }
                c = b / a.Count();
                for (int j = 0; j < a.Count; j++)
                {
                    d += Math.Pow(a[j] - c, 2); 
                }
                e = d / a.Count();
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{c}-{e}");
                sw.Close();
            }
        }
    }
}
