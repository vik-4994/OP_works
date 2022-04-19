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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace prac_02
{
    public partial class MainWindow : Window
    {
        static DispatcherTimer dT;
        static int Radius = 15;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();
        static int pc = 0;
        int[,] ways;
        public MainWindow()
        {
            dT = new DispatcherTimer();
            Random rnd = new Random();
            
            InitializeComponent();
            InitPoints();
            InitPolygon();
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }
        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();
            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();
                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) -
                3 * Radius);

                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height -
                3 * Radius));

                pC.Add(p);
            }
            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();
                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }
        }
        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }
        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }

        private void PlotWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();
            for (int i = 0; i < BestWayIndex.Length; i++)
                Points.Add(pC[BestWayIndex[i]]);
            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }
        private void VelCB_SelectionChanged(object sender,
        SelectionChangedEventArgs e)

        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
            dT.Interval = new

            TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));

        }
        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                dT.Stop();
                NumElemCB.IsEnabled = true;
            }
            else
            {
                NumElemCB.IsEnabled = false;
                Random rnd = new Random();
                if (pc == 0 || PointCount != pc)
                {
                    pc = PointCount;
                    ways = new int[10, PointCount];
                    for (int j = 0; j < 5; j++)
                    {
                        for (int i = 0; i < PointCount; i++)
                            ways[j, i] = i;
                        for (int s = 0; s < 2 * PointCount; s++)
                        {
                            int i1, i2, tmp;
                            i1 = rnd.Next(PointCount);
                            i2 = rnd.Next(PointCount);
                            tmp = ways[j, i1];
                            ways[j, i1] = ways[j, i2];
                            ways[j, i2] = tmp;
                        }
                    }
                }
                dT.Start();
            }
        }
        private void NumElemCB_SelectionChanged(object sender,

        SelectionChangedEventArgs e)

        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
        }
        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();
            //InitPoints();
            PlotPoints();
            PlotWay(GetBestWay());
        }
        private int[] GetBestWay()
        {
            List<double> distanses = new List<double>();
            int[] way = new int[PointCount];
            
            Random rnd = new Random();
            for (int i = 5; i < 10; i++)
            {
                int p1 = rnd.Next(0, 5);
                int p2 = rnd.Next(0, 5);
                while (p1 == p2)
                {
                    p2 = rnd.Next(0, 5);
                }
                int cr = rnd.Next(1, PointCount - 1);
                int[] tmp1 = new int[PointCount];
                int[] tmp2 = new int[PointCount];
                for (int j = 0; j < PointCount; j++)
                {
                    if(j < cr)
                    {
                        tmp1[j] = ways[p1, j];
                        tmp2[j] = ways[p2, j];
                    } else
                    {
                        tmp1[j] = ways[p2, j];
                        tmp2[j] = ways[p1, j];
                    }
                }
                List<int> ch1 = new List<int>();
                List<int> ch2 = new List<int>();
                ch1.AddRange(tmp1);
                ch1.AddRange(tmp2);
                ch2.AddRange(tmp2);
                ch2.AddRange(tmp1);
                for(int j = 0; j < PointCount; j++)
                {
                    ch1.RemoveAt(ch1.LastIndexOf(j));
                    ch2.RemoveAt(ch2.LastIndexOf(j));
                }
                List<int> ch = new List<int>();
                if(rnd.NextDouble() < 0)
                {
                    ch.AddRange(ch1);
                } else
                {
                    ch.AddRange(ch2);
                }
                if(rnd.NextDouble() < 0.5)
                {
                    int i1 = rnd.Next(0, PointCount - 1);
                    int i2 = rnd.Next(0, PointCount - 1);
                    while(i1 == i2)
                    {
                        i2 = rnd.Next(0, PointCount - 1);
                    }
                    int tmpp;
                    if (i1 > i2)
                    {
                        tmpp = i1;
                        i1 = i2;
                        i2 = tmpp;
                    }
                    for (int j = 0; j < Math.Floor((i2 - i1) / 2.0); j++)
                    {
                        tmpp = ch[i1 + j];
                        ch[i1 + j] = ch[i2 - j];
                        ch[i2 - j] = tmpp;
                    }
                    
                }
                for (int j = 0; j < PointCount; j++)
                {
                    ways[i, j] = ch[j];
                }
            }
            double length = 0;
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < PointCount; j++)
                {
                    if(j == PointCount - 1)
                    {
                        length += Math.Sqrt(Math.Pow(pC[ways[i, j]].X - pC[ways[i, 0]].X, 2) + Math.Pow(pC[ways[i, j]].Y - pC[ways[i, 0]].Y, 2));
                    } else
                    {
                        length += Math.Sqrt(Math.Pow(pC[ways[i, j]].X - pC[ways[i, j + 1]].X, 2) + Math.Pow(pC[ways[i, j]].Y - pC[ways[i, j + 1]].Y, 2));
                    }
                }
                distanses.Add(Math.Round(length, 2));
                length = 0;
            }

            List<int> indexes = new List<int>();
            int[,] minWays = new int[5, PointCount];
            int ind = 0;
            double min = distanses[0];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < distanses.Count; j++)
                {
                    if(distanses[j] < min && indexes.IndexOf(j) == -1)
                    {
                        min = distanses[j];
                        ind = j;
                    }
                }
                indexes.Add(ind);
                for (int j = 0; j < distanses.Count; j++)
                {
                    if(indexes.IndexOf(j) == -1)
                    {
                        min = distanses[j];
                        ind = j;
                    }
                }
                for (int j = 0; j < PointCount; j++)
                {
                    minWays[i, j] = ways[indexes[i], j];
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < PointCount; j++)
                {
                    ways[i, j] = minWays[i, j];
                }
            }
            for (int i = 0; i < PointCount; i++)
            {
                way[i] = ways[0, i];
            }
            return way;
        }
    }






















    
}