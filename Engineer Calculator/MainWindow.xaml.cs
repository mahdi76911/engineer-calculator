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
using System.IO;
using Microsoft.Win32;  //SaveDialog
using System.Text.RegularExpressions;

/*To-do:
 * Kar ba file(zakhire marhale marhale amaliat ha too yek text mesle calculeter samsung)
 * GUI (Enable va disable shodan dokme ha ba change shodan text input)
 */

namespace Engineer_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Game Player1;
        GraphForm Graph1;
        Form2 MostafaFrom;
        string STRForFile = "", Mark;
        bool PlusPress = false, DotPress = false, IPress = false;
        ObjStat Stat = ObjStat.INT;
        enum  ObjStat{INT=0,DOUBLE,COMPLEX }

        public MainWindow()
        {
            InitializeComponent();
        }

        public bool BiggerThan(string Num1, string Num2)
        {
            BigDouble N1 = new BigDouble(Num1);
            BigDouble N2 = new BigDouble(Num2);
            if (N1.Sub(N2).ToString()[0] == '-')
                return false;
            else
                return true;
            /*if (Num1.Length == Num2.Length)
            {
                int length = Num1.Length;
                for (int i = 0; i < length; i++)
                {
                    if (int.Parse(Num1[i].ToString()) > int.Parse(Num2[i].ToString()))
                    {
                        return true;
                    }
                    else if (int.Parse(Num1[i].ToString()) < int.Parse(Num2[i].ToString()))
                    {
                        return false;
                    }
                }
            }
            else if (Num1.Length > Num2.Length)
            {
                return true;
            }
            else if (Num1.Length > Num2.Length)
            {
                return false;
            }
            return true;*/
        }

        public string Sort(string numbers)
        {
            string Res = "";


            string[] nums = numbers.Split('\n');
            int count = nums.Length;

            for (int i = 0; i < count - 1; i++)// hazfe \r ha az akhare nums
                nums[i] = nums[i].Substring(0, nums[i].Length - 1);

            int c;
            for (int i = 1; i < count; i++)
            {
                c = i;
                while (true)
                {
                    if (c > 0 && BiggerThan(nums[c - 1], nums[c]))
                    {
                        string mem = nums[c];
                        nums[c] = nums[c - 1];
                        nums[c - 1] = mem;
                        c--;
                    }
                    else
                        break;
                }
            }
            for (int i = 0; i < count; i++)
            {
                Res = Res + nums[i] + "\n";
            }
            return Res;
        }

        private void SumB_Click(object sender, RoutedEventArgs e)
        {
            Mark = "+";
            switch (Stat)
            {
                case ObjStat.INT:
                    BigInt IN1 = new BigInt(Input.Text);
                    BigInt IN2 = new BigInt(Input2.Text);
                    Result.Text = IN1.Sum(IN2).ToString();
                    break;
                case ObjStat.DOUBLE:
                    BigDouble DN1 = new BigDouble(Input.Text);
                    BigDouble DN2 = new BigDouble(Input2.Text);
                    Result.Text = DN1.Sum(DN2).ToString();
                    break;
                case ObjStat.COMPLEX:
                    char[] CH = { '+', '-' };
                    BigComplex CN1 = new BigComplex(new BigDouble(Input.Text.Split(CH)[0]), new BigDouble(Input.Text.Split(CH)[1].Substring(0, Input.Text.Split(CH)[1].Length - 1)));
                    BigComplex CN2 = new BigComplex(new BigDouble(Input2.Text.Split(CH)[0]), new BigDouble(Input2.Text.Split(CH)[1].Substring(0, Input2.Text.Split(CH)[1].Length - 1)));
                    Result.Text = CN1.Sum(CN2).ToString();
                    break;
                default:
                    break;
            }
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Player1 = new Game(this);
            Player1.Show();
            GameButton.IsEnabled = false;
        }

        private void Main_Closed(object sender, EventArgs e)
        {
            try
            {
                Player1.Close();
                Graph1.Close();
            }
            catch { }
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Standard Input : 5sin(6x)");
            Graph1 = new GraphForm(this);
            Graph1.Show();
            GraphButton.IsEnabled = false;
        }

        private void NumbersButton_Click(object sender, RoutedEventArgs e)
        {
            Button X = (Button)sender;
            Input.Text += X.Content;
        }

        private void Input2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Decimal|| e.Key == Key.I || e.Key == Key.Add|| e.Key == Key.Subtract))
                e.Handled = true;
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Decimal || e.Key == Key.X || e.Key == Key.S || e.Key == Key.I || e.Key == Key.Add|| e.Key == Key.Subtract)
            {
                switch (e.Key)
                {
                    case Key.I:
                        if (Stat != ObjStat.COMPLEX || IPress)
                            e.Handled = true;
                        else
                            IPress = true;
                        break;
                    case Key.Decimal:
                        if (Stat == ObjStat.INT || DotPress)
                            e.Handled = true;
                        else
                            DotPress = true;
                        break;
                    case Key.Add:
                        if (PlusPress)
                            e.Handled = true;
                        else
                            PlusPress = true;
                        break;
                    default:
                        break;
                }
            }
            else
                e.Handled = true;
        }
        
        private void CEB_Click(object sender, RoutedEventArgs e)
        {
            Input.Text = "";
            Input2.Text = "0";
        }

        private void Input2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Input2.Text == "")
                Input2.Text = "";
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Input.Text.Split('.').Length < 2)
                DotPress = false;
            if (Input.Text.Split('i').Length < 2)
                IPress = false;
            if (Input.Text.Split('+').Length < 2)
                PlusPress = false;

            if (Input.Text.Split('\n').Length > 1 && Input.Text.Split('\n')[1] != "")
                SortB.IsEnabled = true;
            else
                SortB.IsEnabled = false;
            if (Input.Text =="")
            {
                FactB.IsEnabled = false;
                CrossB.IsEnabled = false;
                SumB.IsEnabled = false;
                DivB.IsEnabled = false;
                SubB.IsEnabled = false;
            }
            else
            {
                FactB.IsEnabled = true;
                CrossB.IsEnabled = true;
                SumB.IsEnabled = true;
                DivB.IsEnabled = true;
                SubB.IsEnabled = true;
            }
        }

        private void SortB_Click(object sender, RoutedEventArgs e)
        {
            Mark = "Sort:";
            Result.Text = Sort(Input.Text);
        }

        private void DivB_Click(object sender, RoutedEventArgs e)
        {
            Mark = "/";
            try
            {
                switch (Stat)
                {
                    case ObjStat.INT:
                        BigInt IN1 = new BigInt(Input.Text);
                        BigInt IN2 = new BigInt(Input2.Text);
                        Result.Text = IN1.Div(IN2).ToString();
                        break;
                    case ObjStat.DOUBLE:
                        BigDouble DN1 = new BigDouble(Input.Text);
                        BigDouble DN2 = new BigDouble(Input2.Text);
                        Result.Text = DN1.Div(DN2,10).ToString();
                        break;
                    case ObjStat.COMPLEX:
                        char[] CH = { '+', '-' };
                        BigComplex CN1 = new BigComplex(new BigDouble(Input.Text.Split(CH)[0]), new BigDouble(Input.Text.Split(CH)[1].Substring(0, Input.Text.Split(CH)[1].Length - 1)));
                        BigComplex CN2 = new BigComplex(new BigDouble(Input2.Text.Split(CH)[0]), new BigDouble(Input2.Text.Split(CH)[1].Substring(0, Input2.Text.Split(CH)[1].Length - 1)));
                        Result.Text = CN1.Div(CN2).ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex) { MessageBox.Show(Ex.Message); }
        }

        private void CrossB_Click(object sender, RoutedEventArgs e)
        {
            Mark = "*";
            switch (Stat)
            {
                case ObjStat.INT:
                    BigInt IN1 = new BigInt(Input.Text);
                    BigInt IN2 = new BigInt(Input2.Text);
                    Result.Text = IN1.Cross(IN2).ToString();
                    break;
                case ObjStat.DOUBLE:
                    BigDouble DN1 = new BigDouble(Input.Text);
                    BigDouble DN2 = new BigDouble(Input2.Text);
                    Result.Text = DN1.Cross(DN2).ToString();
                    break;
                case ObjStat.COMPLEX:
                    char[] CH = { '+', '-' };
                    BigComplex CN1 = new BigComplex(new BigDouble(Input.Text.Split(CH)[0]), new BigDouble(Input.Text.Split(CH)[1].Substring(0, Input.Text.Split(CH)[1].Length - 1)));
                    BigComplex CN2 = new BigComplex(new BigDouble(Input2.Text.Split(CH)[0]), new BigDouble(Input2.Text.Split(CH)[1].Substring(0, Input2.Text.Split(CH)[1].Length - 1)));
                    Result.Text = CN1.Cross(CN2).ToString();
                    break;
                default:
                    break;
            }

        }

        private void SubB_Click(object sender, RoutedEventArgs e)
        {
            Mark = "-";
            switch (Stat)
            {
                case ObjStat.INT:
                    BigInt IN1 = new BigInt(Input.Text);
                    BigInt IN2 = new BigInt(Input2.Text);
                    Result.Text = IN1.Sub(IN2).ToString();
                    break;
                case ObjStat.DOUBLE:
                    BigDouble DN1 = new BigDouble(Input.Text);
                    BigDouble DN2 = new BigDouble(Input2.Text);
                    Result.Text = DN1.Sub(DN2).ToString();
                    break;
                case ObjStat.COMPLEX:
                    char[] CH = { '+', '-' };
                    BigComplex CN1 = new BigComplex(new BigDouble(Input.Text.Split(CH)[0]), new BigDouble(Input.Text.Split(CH)[1].Substring(0, Input.Text.Split(CH)[1].Length - 1)));
                    BigComplex CN2 = new BigComplex(new BigDouble(Input2.Text.Split(CH)[0]), new BigDouble(Input2.Text.Split(CH)[1].Substring(0, Input2.Text.Split(CH)[1].Length - 1)));
                    Result.Text = CN1.Sub(CN2).ToString();
                    break;
                default:
                    break;
            }

        }

        private void FactB_Click(object sender, RoutedEventArgs e)
        {
            if (Stat == ObjStat.INT)
            {
                Mark = "!";
                BigInt FN = new BigInt();
                Result.Text = FN.Factorial(Convert.ToUInt64(Input.Text)).Number;
            }
            else
                MessageBox.Show("Erorr!");
        }

        private void DevB_Click(object sender, RoutedEventArgs e)
        {
            MostafaFrom = new Form2(this);
            MostafaFrom.Show();
            DevB.IsEnabled = false;
        }

        private void PrintB_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog MySaveD = new SaveFileDialog();
            MySaveD.CheckFileExists = false;
            if (MySaveD.ShowDialog() == true)
            {
                string MyPath = MySaveD.FileName;
                StreamWriter MyFile = new StreamWriter(MyPath+".txt");
                try
                {
                    for (int i = 0; i < STRForFile.Split('\n').Length; i++)
                    {
                        MyFile.WriteLine(STRForFile.Split('\n')[i]);
                    }
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    MyFile.Close();
                }
            }

        }

        private void BIB_Checked(object sender, RoutedEventArgs e)
        {
            Stat = ObjStat.INT;
        }

        private void BDB_Checked(object sender, RoutedEventArgs e)
        {
            Stat = ObjStat.DOUBLE;
        }

        private void BCB_Checked(object sender, RoutedEventArgs e)
        {
            Stat = ObjStat.COMPLEX;
        }

        private void Result_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Mark == "Sort:")
                STRForFile += "\n" + Mark + ":\n" + Input.Text + "\n To \n" + Result.Text;
            else if (Mark == "!")
                STRForFile += "\n" + Input.Text + "! = " + Result.Text;
            else
                STRForFile += "\n" + Mark + ":\n" + Input.Text + " " + Input2.Text + " = " + Result.Text;
        }
    }
}
