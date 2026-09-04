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
using System.Text.RegularExpressions;

namespace Engineer_Calculator
{
    /// <summary>
    /// Interaction logic for GraphForm.xaml
    /// </summary>
    public partial class GraphForm : Window
    {
        private MainWindow WParent;
        private MessageBoxImage ok;

        public GraphForm()
        {
            InitializeComponent();
        }

        public GraphForm(MainWindow parent)
        {
            WParent = parent;
            InitializeComponent();

            Line line0 = new Line();
            line0.Stroke = System.Windows.Media.Brushes.LightGray;
            line0.X1 = 0;
            line0.Y1 = -500;
            line0.X2 = 0;
            line0.Y2 = 500;
            line0.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            line0.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            line0.StrokeThickness = 5;
            MyGrid.Children.Add(line0);
            Line line1 = new Line();
            line1.Stroke = System.Windows.Media.Brushes.LightGray;
            line1.X1 = -500;
            line1.Y1 = 0;
            line1.X2 = 500;
            line1.Y2 = 0;
            line1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            line1.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            line1.StrokeThickness = 5;
            MyGrid.Children.Add(line1);
        }

        private void GraphWin_Closed(object sender, EventArgs e)
        {
            WParent.GraphButton.IsEnabled = true;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            double zarib;
            double zaribx;
            int counter = 0;
            string zaribs = "";
            string zaribxs = "";
            string s = textBox.Text;
            if (s == "")
                MessageBox.Show("Enter function", "Error");
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsNumber(s[i]))
                    {
                        zaribs += s[i];
                        counter++;
                    }
                    else if (counter == 0)
                    {
                        zaribs = "1";
                        counter += 3;
                        break;
                    }
                    else
                    {
                        counter += 4;
                        break;
                    }
                }

                int acount = counter;
                for (int i = counter; i < s.Length; i++)
                {
                    if (char.IsNumber(s[i]))
                    {
                        zaribxs += s[i];
                        counter++;
                    }
                    else if (counter == acount)
                    {
                        zaribxs = "1";
                        break;
                    }
                    else
                        break;
                }
                zarib = Int32.Parse(zaribs);
                zaribx = Int32.Parse(zaribxs);
                double y1;
                double x1;
                double y2;
                double x2;
                for (int i = int.Parse(textBox_Copy.Text); i < int.Parse(textBox_Copy1.Text); i++)
                {
                    x1 = i;
                    y1 = 20 * zarib * (Math.Sin(zaribx * i / 20));
                    x2 = (i + 1);
                    y2 = 20 * zarib * (Math.Sin(zaribx * (i + 1) / 20));
                    Line line = new Line();
                    line.Stroke = System.Windows.Media.Brushes.LightCoral;
                    line.X1 = x1;
                    line.Y1 = y1;
                    line.X2 = x2;
                    line.Y2 = y2;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    line.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    line.StrokeThickness = 1;
                    MyGrid.Children.Add(line);
                }
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            MyGrid.Children.Clear();
            Line line0 = new Line();
            line0.Stroke = System.Windows.Media.Brushes.LightGray;
            line0.X1 = 0;
            line0.Y1 = -500;
            line0.X2 = 0;
            line0.Y2 = 500;
            line0.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            line0.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            line0.StrokeThickness = 5;
            MyGrid.Children.Add(line0);
            Line line1 = new Line();
            line1.Stroke = System.Windows.Media.Brushes.LightGray;
            line1.X1 = -500;
            line1.Y1 = 0;
            line1.X2 = 500;
            line1.Y2 = 0;
            line1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            line1.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            line1.StrokeThickness = 5;
            MyGrid.Children.Add(line1);
        }
    }
}
