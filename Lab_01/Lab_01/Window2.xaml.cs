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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public int[,] ticTacToes = new int[5, 5];
        public int turn = 1;
        public Window2()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    ticTacToes[i, j] = 0;
                }
            }
        }
        private void Exit_btn_click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void isGameEnded()
        {
            bool isGameHasEmpty = false;
            bool hasFourTicks = false;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(ticTacToes[i, j] == 0)
                    {
                        isGameHasEmpty = true;
                        break;
                    }
                }
            }
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    if (ticTacToes[i, j] + ticTacToes[i, j + 1] + ticTacToes[i, j + 2] + ticTacToes[i, j + 3] == 4 || ticTacToes[i, j] + ticTacToes[i, j + 1] + ticTacToes[i, j + 2] + ticTacToes[i, j + 3] == -4)
                    {
                        hasFourTicks = true;
                        break;
                    }

                    if(i >= 0 && i <= 1 && (ticTacToes[i,j] == 1 || ticTacToes[i,j] == -1))
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
                        if(ticTacToes[i, j] + ticTacToes[i - 1, j + 1] + ticTacToes[i - 2, j + 2] + ticTacToes[i - 3, j + 3] == 4 || ticTacToes[i, j] + ticTacToes[i - 1, j + 1] + ticTacToes[i - 2, j + 2] + ticTacToes[i - 3, j + 3] == -4)
                        {
                            hasFourTicks = true;
                            break;
                        }
                    }
                }
            }
            for(int i = 0; i < 2; i++)
            {
                for(int j = 2; j < 5; j++)
                {
                    if (ticTacToes[i, j] + ticTacToes[i + 1, j] + ticTacToes[i + 2, j] + ticTacToes[i + 3, j] == 4 || ticTacToes[i, j] + ticTacToes[i + 1, j] + ticTacToes[i + 2, j] + ticTacToes[i + 3, j] == -4)
                    {
                        hasFourTicks = true;
                        break;
                    }
                }
            }
            

            if(isGameHasEmpty == false || hasFourTicks == true)
            {
                LB.Content = "Game Ended";
            } else if(turn == 0)
            {
                makeTurnII();
            }
        }

        private void makeTurn(object sender, RoutedEventArgs e)
        {
            Button BX = e.Source as Button;
            int length = BX.Name.Length;
            string numberOfPos = BX.Name.Remove(0, 1);
            int row = Convert.ToInt32(Math.Floor((Convert.ToInt32(numberOfPos) - 1) / 5.0));
            int column = Convert.ToInt32(numberOfPos) - 5 * row - 1;
            LB.Content = $"{row} {column}";
            ticTacToes[row, column] = 1;
            BX.IsEnabled = false;
            BX.Content = "X";
            turn = 0;
            isGameEnded();
        }

        private void makeTurnII()
        {
            Random rnd = new Random();
            List<UIElement> elements = Grid.Children.Cast<UIElement>().ToList();
            int x, y;
            while (true)
            {
                x = rnd.Next(0, 5);
                y = rnd.Next(0, 5);
                if(ticTacToes[x, y] == 0)
                {
                    ticTacToes[x, y] = -1;
                    elements[x * 5 + y].SetValue(Button.ContentProperty, "O");
                    elements[x * 5 + y].SetValue(Button.IsEnabledProperty, false);
                    LB.Content = ticTacToes[x, y];
                    turn = 1;
                    isGameEnded();
                    break;
                }
            }
        }
    }
}
