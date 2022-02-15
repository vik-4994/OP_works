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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        public List<long> timeStamps = new List<long>();
        public double r = 0;
        public double w = 0;
        public void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        public void InputField_PreviewKeyUp(object sender, RoutedEventArgs e)
        {
            List<double> intervals = new List<double>();
            DateTimeOffset dto = DateTimeOffset.Now;
            long timeStamp = dto.ToUnixTimeMilliseconds();
            if (InputField.Text != "")
            {
                if (VerifField.Text == InputField.Text)
                {
                    timeStamps.Add(timeStamp);
                    Counter_LB.Content = Convert.ToInt32(Counter_LB.Content) + 1;
                    ComboBoxItem item = (ComboBoxItem)CountProtection.SelectedValue;

                    if (Counter_LB.Content.ToString() == item.Content.ToString())
                    {
                        InputField.IsEnabled = false;
                    }

                    for (int i = 0; i < timeStamps.Count() - 1; i++)
                    {
                        intervals.Add(Convert.ToDouble((timeStamps[i + 1] - timeStamps[i] * 1.0) / 1000));
                    }
                    LB.Content = intervals[0];
                    timeStamps.Clear();
                    Calc(intervals);
                    InputField.Text = "";
                }
                else
                {
                    if (VerifField.Text.Contains(InputField.Text))
                    {
                        timeStamps.Add(timeStamp);
                    }
                    else
                    {
                        timeStamps.Clear();
                        InputField.Text = "";
                    }
                }
            }
        }

        public void Calc(List<double> a)
        {
            string filePath = @"\\vmware-host\Shared Folders\Рабочий стол\Data.txt";
            int num = new Random().Next(0, 4);
            int cntr = 0;
            double tt = 2.26;
            double b = 0;
            double c;
            double d = 0;
            double e;
            double g;
            double tp;
            double dis = 0;
            double matSpod = 0;
            StreamReader sr = new StreamReader(filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if(cntr == num)
                {
                    string[] jh = line.Split('-');
                    dis = Convert.ToDouble(jh[1]);
                    matSpod = Convert.ToDouble(jh[0]);
                    break;
                } else
                {
                    cntr++;
                }
            }
            sr.Close();
            
                for (int i = 0; i < a.Count(); i++)
                {
                    b += a[i];
                }
                c = b / a.Count();
                for (int j = 0; j < a.Count; j++)
                {
                    d += Math.Pow(a[j] - c, 2);
                }
                e = d / a.Count();
            double phisher;
            double phisherStatic = 3.18;
            if(dis < e)
            {
                phisher = e / dis;
            } else
            {
                phisher = dis / e;
            }

            if(phisher > phisherStatic)
            {
                DispField.Content = "неоднородны";
            } else
            {
                DispField.Content = "однородны";
            }

            g = Math.Sqrt((dis + e) * (InputField.Text.Length - 1) / (2 * InputField.Text.Length - 1));

            tp = Math.Abs(matSpod - c) / (g * Math.Sqrt(2.0 / InputField.Text.Length));
            if (tp < tt)
            {
                r++;
            }

            w++;

            PField.Content = $"{r/w}";

            P1Field.Content = $"{(w - r) / w * 0.05}";
            P2Field.Content = $"{r / w * 0.05}";
        }
    }
}
