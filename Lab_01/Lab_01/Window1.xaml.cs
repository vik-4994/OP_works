using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            string text = TB.Text;
            string filePath = @"\\vmware-host\Shared Folders\Рабочий стол\Data.txt";
            StreamWriter dataWrite = new StreamWriter(filePath, true);
            dataWrite.WriteLine(text);
            dataWrite.Close();
            TB.Text = "";
            LB.Content = "Succesfully read TextBox";
        }

        private void Button1_click(object sender, RoutedEventArgs e)
        {
            string text = TB.Text;
            string filePath = @"\\vmware-host\Shared Folders\Рабочий стол\Data.txt";
            StreamReader dataRead = new StreamReader(filePath);
            string line;
            List<string> datas = new List<string>();
            while ((line = dataRead.ReadLine()) != null)
            {
                if (line.IndexOf(text) == -1)
                {
                    datas.Add(line);
                }
            }
            dataRead.Close();
            StreamWriter dataWrite = new StreamWriter(filePath, false);
            for(int i = 0; i < datas.Count(); i++)
            {
                dataWrite.WriteLine(datas[i]);
            }
            dataWrite.Close();
           
            LB.Content = "Succesfully deleted Data";
        }

        private void Exit_btn_click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
