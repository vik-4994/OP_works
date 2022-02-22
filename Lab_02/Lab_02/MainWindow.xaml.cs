using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_02
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Window w;
        Grid g;
        public int[,] ticTacToes = new int[5, 5];
        public int turn = 1;
        public string a;
        public string znak;
        private void B1_click(object sender, RoutedEventArgs e)
        {
            w = new Window();
            Grid myGrid = new Grid();
            w.Width = 400;
            w.Height = 300;
            myGrid = addButtonToGrid(myGrid, new Element("Go to main", "B1", 75, 30, 0, -25), goToMain);
            myGrid = addButtonToGrid(myGrid, new Element("Write", "B2", 50, 30, 100, -200), Button_click);
            myGrid = addButtonToGrid(myGrid, new Element("Delete", "B2", 50, 30, 225, -200), Button1_click);
            myGrid = addLabelToGrid(myGrid, new Element("Write some data", "LB", 200, 30, -100, -100));
            myGrid = addTextBoxToGrid(myGrid, new Element("", "TB", 150, 30, -200, -200));
            g = myGrid;
            w.Content = myGrid;
            Hide();
            w.Show();
        }
        private void B2_click(object sender, RoutedEventArgs e)
        {
            w = new Window();
            Grid myGrid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    myGrid = addButtonToGrid(myGrid, new Element("", $"B{i * 5 + j + 1}", 25, 25, -150 + 50 * j, -150 + 50 * i), makeTurn);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    ticTacToes[i, j] = 0;
                }
            }
            myGrid = addLabelToGrid(myGrid, new Element("", "LB", 220, 30, 10, 255));
            myGrid = addButtonToGrid(myGrid, new Element("Go to main", "B26", 100, 31, 233, 255), goToMain);
            g = myGrid;
            w.Content = myGrid;
            Hide();
            w.Show();
        }
        private void B3_click(object sender, RoutedEventArgs e)
        {
            w = new Window();
            Grid myGrid = new Grid();
            myGrid = addTextBoxToGrid(myGrid, new Element("", "TB", 116, 26, 100, 8));
            List<UIElement> elements = myGrid.Children.Cast<UIElement>().ToList();
            elements[0].SetValue(TextBox.IsEnabledProperty, false);
            myGrid = addButtonToGrid(myGrid, new Element("1", "B1", 26, 26, 10, 68), B01_click);
            myGrid = addButtonToGrid(myGrid, new Element("2", "B2", 26, 26, 70, 68), B02_click);
            myGrid = addButtonToGrid(myGrid, new Element("3", "B3", 26, 26, 130, 68), B03_click);
            myGrid = addButtonToGrid(myGrid, new Element("4", "B4", 26, 26, 10, 128), B04_click);
            myGrid = addButtonToGrid(myGrid, new Element("5", "B5", 26, 26, 70, 128), B05_click);
            myGrid = addButtonToGrid(myGrid, new Element("6", "B6", 26, 26, 130, 128), B06_click);
            myGrid = addButtonToGrid(myGrid, new Element("7", "B7", 26, 26, 10, 188), B07_click);
            myGrid = addButtonToGrid(myGrid, new Element("8", "B8", 26, 26, 70, 188), B08_click);
            myGrid = addButtonToGrid(myGrid, new Element("9", "B9", 26, 26, 130, 188), B09_click);
            myGrid = addButtonToGrid(myGrid, new Element("0", "B10", 26, 26, 10, 248), B10_click);
            myGrid = addButtonToGrid(myGrid, new Element("+", "B11", 26, 26, 70, 248), B11_click);
            myGrid = addButtonToGrid(myGrid, new Element("-", "B12", 26, 26, 130, 248), B12_click);
            myGrid = addButtonToGrid(myGrid, new Element("x", "B13", 26, 26, 190, 248), B13_click);
            myGrid = addButtonToGrid(myGrid, new Element("/", "B14", 26, 26, 190, 188), B14_click);
            myGrid = addButtonToGrid(myGrid, new Element("C", "B15", 26, 26, 190, 128), B15_click);
            myGrid = addButtonToGrid(myGrid, new Element("=", "B16", 26, 26, 190, 68), B16_click);
            myGrid = addButtonToGrid(myGrid, new Element(".", "B17", 26, 26, 10, 308), B17_click);
            myGrid = addButtonToGrid(myGrid, new Element("+/-", "B18", 26, 26, 70, 308), B18_click);
            myGrid = addButtonToGrid(myGrid, new Element("Go to main", "B19", 70, 26, 173, 308), goToMain);
            g = myGrid;
            w.Content = myGrid;
            Hide();
            w.Show();
        }
        private void B4_click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void B5_click(object sender, RoutedEventArgs e)
        {
            w = new Window();
            Grid myGrid = new Grid();
            myGrid = addButtonToGrid(myGrid, new Element("Go to main", "B1", 75, 30, 0, 50), goToMain);
            myGrid = addLabelToGrid(myGrid, new Element("Виконав Гавриленко I. 2022p.", "LB1", 150, 30, 0, -100));
            g = myGrid;
            w.Content = myGrid;
            Hide();
            w.Show();
        }

        private void goToMain(object sender, RoutedEventArgs e)
        {
            w.Hide();
            w = new MainWindow();
            w.Show();
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {

            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            var text = elems[4].GetValue(TextBox.TextProperty);
            string filePath = @"\\vmware-host\Shared Folders\Рабочий стол\Data.txt";
            StreamWriter dataWrite = new StreamWriter(filePath, true);
            dataWrite.WriteLine(text);
            dataWrite.Close();
            elems[4].SetValue(TextBox.TextProperty, "");
            elems[3].SetValue(Label.ContentProperty, "Succesfully read TextBox");
        }

        private void Button1_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[4].GetValue(TextBox.TextProperty)); ;
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
            for (int i = 0; i < datas.Count(); i++)
            {
                dataWrite.WriteLine(datas[i]);
            }
            dataWrite.Close();

            elems[3].SetValue(Label.ContentProperty, "Succesfully deleted data");
        }

        private void makeTurn(object sender, RoutedEventArgs e)
        {
            //List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            Button BX = e.Source as Button;
            //BX.Content = BX.Name;
            int length = BX.Name.Length;
            string numberOfPos = BX.Name.Remove(0, 1);
            int row = Convert.ToInt32(Math.Floor((Convert.ToInt32(numberOfPos) - 1) / 5.0));
            int column = Convert.ToInt32(numberOfPos) - 5 * row - 1;
            //BX.Content = $"{row} {column}";
            ticTacToes[row, column] = 1;
            BX.IsEnabled = false;
            BX.Content = "X";
            turn = 0;
            isGameEnded();
        }

        private void makeTurnII()
        {
            Random rnd = new Random();
            List<UIElement> elements = g.Children.Cast<UIElement>().ToList();
            int x, y;
            while (true)
            {
                x = rnd.Next(0, 5);
                y = rnd.Next(0, 5);
                if (ticTacToes[x, y] == 0)
                {
                    ticTacToes[x, y] = -1;
                    elements[x * 5 + y].SetValue(Button.ContentProperty, "O");
                    elements[x * 5 + y].SetValue(Button.IsEnabledProperty, false);
                    //LB.Content = ticTacToes[x, y];
                    turn = 1;
                    isGameEnded();
                    break;
                }
            }
        }

        private void isGameEnded()
        {
            List<UIElement> elements = g.Children.Cast<UIElement>().ToList();
            bool isGameHasEmpty = false;
            bool hasFourTicks = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (ticTacToes[i, j] == 0)
                    {
                        isGameHasEmpty = true;
                        break;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (ticTacToes[i, j] + ticTacToes[i, j + 1] + ticTacToes[i, j + 2] + ticTacToes[i, j + 3] == 4 || ticTacToes[i, j] + ticTacToes[i, j + 1] + ticTacToes[i, j + 2] + ticTacToes[i, j + 3] == -4)
                    {
                        hasFourTicks = true;
                        break;
                    }

                    if (i >= 0 && i <= 1 && (ticTacToes[i, j] == 1 || ticTacToes[i, j] == -1))
                    {
                        if (ticTacToes[i, j] + ticTacToes[i + 1, j] + ticTacToes[i + 2, j] + ticTacToes[i + 3, j] == 4 || ticTacToes[i, j] + ticTacToes[i + 1, j] + ticTacToes[i + 2, j] + ticTacToes[i + 3, j] == -4)
                        {
                            hasFourTicks = true;
                            break;
                        }
                        if (ticTacToes[i, j] + ticTacToes[i + 1, j + 1] + ticTacToes[i + 2, j + 2] + ticTacToes[i + 3, j + 3] == 4 || ticTacToes[i, j] + ticTacToes[i + 1, j + 1] + ticTacToes[i + 2, j + 2] + ticTacToes[i + 3, j + 3] == -4)
                        {
                            hasFourTicks = true;
                            break;
                        }
                    }
                    else if (i >= 3 && i <= 4 && (ticTacToes[i, j] == 1 || ticTacToes[i, j] == -1))
                    {
                        if (ticTacToes[i, j] + ticTacToes[i - 1, j + 1] + ticTacToes[i - 2, j + 2] + ticTacToes[i - 3, j + 3] == 4 || ticTacToes[i, j] + ticTacToes[i - 1, j + 1] + ticTacToes[i - 2, j + 2] + ticTacToes[i - 3, j + 3] == -4)
                        {
                            hasFourTicks = true;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    if (ticTacToes[i, j] + ticTacToes[i + 1, j] + ticTacToes[i + 2, j] + ticTacToes[i + 3, j] == 4 || ticTacToes[i, j] + ticTacToes[i + 1, j] + ticTacToes[i + 2, j] + ticTacToes[i + 3, j] == -4)
                    {
                        hasFourTicks = true;
                        break;
                    }
                }
            }


            if (isGameHasEmpty == false || hasFourTicks == true)
            {
                elements[25].SetValue(Label.ContentProperty, "Game ended");
            }
            else if (turn == 0)
            {
                makeTurnII();
            }
        }

        private void B01_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}1");
        }

        private void B02_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}2");
        }

        private void B03_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}3");
        }

        private void B04_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}4");
        }

        private void B05_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}5");
        }

        private void B06_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}6");
        }

        private void B07_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}7");
        }

        private void B08_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}8");
        }

        private void B09_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}9");
        }

        private void B10_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            elems[0].SetValue(TextBox.TextProperty, $"{text}0");
        }

        private void B11_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            if (text != "")
            {
                znak = "+";
                a = text;
                elems[0].SetValue(TextBox.TextProperty, $"");
            }
        }

        private void B12_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            if (text != "")
            {
                znak = "-";
                a = text;
                elems[0].SetValue(TextBox.TextProperty, $"");
            }
        }

        private void B13_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            if (text != "")
            {
                znak = "*";
                a = text;
                elems[0].SetValue(TextBox.TextProperty, $"");
            }
        }

        private void B14_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            if (text != "")
            {
                znak = "/";
                a = text;
                elems[0].SetValue(TextBox.TextProperty, $"");
            }
        }

        private void B15_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            elems[0].SetValue(TextBox.TextProperty, $"");
        }

        private void B16_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            double b = 0;
            switch (znak)
            {
                case "+":
                    b = Convert.ToDouble(a) + Convert.ToDouble(text);
                    break;
                case "-":
                    b = Convert.ToDouble(a) - Convert.ToDouble(text);
                    break;
                case "/":
                    b = Convert.ToDouble(a) * 1.0 / Convert.ToDouble(text);
                    break;
                case "*":
                    b = Convert.ToDouble(a) * Convert.ToDouble(text);
                    break;
            }
            //TB.Text = b.ToString();
            elems[0].SetValue(TextBox.TextProperty, $"{b.ToString()}");
            znak = "";
        }

        

        private void B17_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            if (text != "")
            {
                //TB.Text += ",";
                elems[0].SetValue(TextBox.TextProperty, $"{text},");
            }
            else
            {
                elems[0].SetValue(TextBox.TextProperty, $"0,");
            }
        }

        private void B18_click(object sender, RoutedEventArgs e)
        {
            List<UIElement> elems = g.Children.Cast<UIElement>().ToList();
            string text = Convert.ToString(elems[0].GetValue(TextBox.TextProperty));
            if (text[0] != '-')
            {
                elems[0].SetValue(TextBox.TextProperty, $"-{text}");
            }
            else
            {
                //TB.Text = TB.Text.Remove(0, 1);
                elems[0].SetValue(TextBox.TextProperty, $"{text.Remove(0,1)}");
            }
        }

        ////////////
        public Grid addButtonToGrid(Grid grid, Element elem, RoutedEventHandler func)
        {
            Button el = new Button();
            el.Width = elem.width;
            el.Height = elem.height;
            Thickness m = el.Margin;
            m.Left = elem.ML;
            m.Top = elem.MT;
            el.Margin = m;
            el.Content = elem.text;
            el.Name = elem.name;
            el.Click += func;
            grid.Children.Add(el);
            return grid;
        }

        public Grid addLabelToGrid(Grid grid, Element elem)
        {
            Label el = new Label();
            el.Width = elem.width;
            el.Height = elem.height;
            Thickness m = el.Margin;
            m.Left = elem.ML;
            m.Top = elem.MT;
            el.Margin = m;
            el.Content = elem.text;
            el.Name = elem.name;
            grid.Children.Add(el);
            return grid;
        }

        public Grid addTextBoxToGrid(Grid grid, Element elem)
        {
            TextBox el = new TextBox();
            el.Width = elem.width;
            el.Height = elem.height;
            Thickness m = el.Margin;
            m.Left = elem.ML;
            m.Top = elem.MT;
            el.Margin = m;
            el.Name = elem.name;
            grid.Children.Add(el);
            return grid;
        }

        public class Element
        {
            public int width;
            public int height;
            public int ML;
            public int MT;
            public string name;
            public string text;
            public Element(string txt, string n, int w, int h, int ml, int mt)
            {
                name = n;
                width = w;
                height = h;
                ML = ml;
                MT = mt;
                text = txt;
            }
        }
    }
}
