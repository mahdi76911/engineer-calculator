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
using Microsoft.Win32;  //SaveDialog
//for serialize
//using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Engineer_Calculator
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private MainWindow WParent;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        static System.Windows.Threading.DispatcherTimer CheckTimer = new System.Windows.Threading.DispatcherTimer();

        TextBox[] Answer = new TextBox[3];
        List<BlockClass> MyBlocks = new List<BlockClass>(18);
        static int[] BlocksInCol = new int[3];  //tedade block haye feli dar yek sotun.
        static int[] WaitBlockInCol = new int[3];   //tedade block haye montazer dar yek sotun.
        static int CheckColumn; //columni ke montazere check shodane.

        //Settings:
        static int ColumnSize = 6;  //andazeye kolle sotun.
        static int BlockReleaseTime = 10, BlockReleaseTick = 8;  //har chand sanie yek bar yek block zaher shavad.
        static int ColumnNumbers = 1;   //Tedad Column ha ke dar bazi hastan.
        static int NumbersOfBlockInABox = 2;    //Chandta Chandta bahaman maslaan 2 ta 2 ta jam kone natijero bekhad.
        static char Operator = '+';
        int IntLenght = 2, DecLenght = 2;   //tedade argham ashari va gheyre asharie adad dakhele block.
        int Speed = 1;  //sorate harkat block.  nemishe ziad jabejash kard chon jabeja shodan block maloom mishe!!!
        int ScoreScope = 50;
        TimeSpan TickReleaseSpeed = new TimeSpan(0, 0, 0, 1);   //sorat timer baraye release kardane block.(sorate timere Timer).
        TimeSpan BlockGoSpeed = new TimeSpan(0, 0, 0, 0, 1);    //sorat timere harkat dahandeye block ha.
        
        //constructors:
        public Game()
        {
            InitializeComponent();
        }
        public Game(MainWindow parent)
        {
            WParent = parent;
            InitializeComponent();
            for (int i = 0; i < BlocksInCol.Length; i++)
            {
                BlocksInCol[i] = 0;
                WaitBlockInCol[i] = 0;
            }
        }

        private double DRandom(int IntLength = 2, int DecimalLength = 2)
        {
            Random R = new Random();
            double res = R.Next((int)Math.Pow(10, IntLength - 1), (int)Math.Pow(10, IntLength));
            if (DecimalLength != 0)
            {
                double x;
                do
                {
                    x = R.Next(0, (int)Math.Pow(10, DecimalLength)) / Math.Pow(10, DecimalLength);
                } while (x.ToString().Length - 2 != DecimalLength);
                res += x;
            }
            return res;
        }

        [Serializable]
        class BlockClass
        {
            System.Windows.Threading.DispatcherTimer BTimer = new System.Windows.Threading.DispatcherTimer();

            public int x, y = 0;
            public int stat = 0;
            public int RunSpeed;
            public Label GBlock = new Label();

            //constructor for Load

            public BlockClass(string Loads)
            {//col , y
                x = int.Parse(Loads[0].ToString());
                y = int.Parse(Loads.Substring(2));
                stat = 2;
                GBlock.Width = 84;
                GBlock.Height = 40;
                GBlock.VerticalAlignment = VerticalAlignment.Top;
                GBlock.HorizontalAlignment = HorizontalAlignment.Left;
                GBlock.BorderBrush = Brushes.Black;
                GBlock.Margin = new Thickness(x * GBlock.Width, y, 0, 0);
                GBlock.BorderThickness = new Thickness(1);
                GBlock.Content = "";
                GBlock.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Engineer Calculator;component/Files/Bricks.jpg")));
            }

            public BlockClass(int ColNumber, string DoubleNumber, TimeSpan BSpeed, int BRunSpeed)
            {
                x = ColNumber;
                RunSpeed = BRunSpeed;
                BTimer.Tick += BlockRun;
                BTimer.Interval = BSpeed;
                BTimer.IsEnabled = true;
                {//Set Block properties
                    GBlock.Content = DoubleNumber;
                    GBlock.Width = 84;
                    GBlock.Height = 40;
                    GBlock.HorizontalContentAlignment = HorizontalAlignment.Center;
                    GBlock.VerticalContentAlignment = VerticalAlignment.Center;
                    GBlock.FontSize = 20;
                    GBlock.Background = Brushes.LightSalmon;
                    GBlock.FontFamily = new FontFamily("Times New Roman");
                    GBlock.VerticalAlignment = VerticalAlignment.Top;
                    GBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    GBlock.Margin = new Thickness(x * GBlock.Width, 0, 0, 0);
                    GBlock.BorderBrush = Brushes.Black;
                    GBlock.BorderThickness = new Thickness(1);
                }
            }

            public void BlockRun(object sender, EventArgs e)
            {
                if (y + GBlock.Height + RunSpeed >= GBlock.Height * (ColumnSize - (BlocksInCol[x] - 1)))    //dalile tooye ham raftan in yekast vaghti 2 ta block zoodtar az in ke berese paeen miad too ye sotun.
                {
                    stat = 1;
                    WaitBlockInCol[x]++;
                    BTimer.IsEnabled = false;
                }
                else
                {
                    y += RunSpeed;
                    GBlock.Margin = new Thickness(x * GBlock.Width, y, 0, 0);
                }
                if (WaitBlockInCol[x] == NumbersOfBlockInABox)
                {
                    CheckColumn = x;
                    CheckTimer.IsEnabled = true;
                }
            }

            public override string ToString()
            {
                return x + " " + y;
            }
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            if (WaitBlockInCol[CheckColumn] == NumbersOfBlockInABox)
            {
                int[] index = new int[NumbersOfBlockInABox];
                double Res = 0;
                if (Operator == '*' || Operator == '/')
                    Res = 1;
                int Counter = 0;
                for (int i = 0; i < MyBlocks.Count; i++)
                {
                    if (MyBlocks[i].stat == 1 && MyBlocks[i].x == CheckColumn)
                    {
                        switch (Operator)
                        {
                            case '+':
                                Res += double.Parse(MyBlocks[i].GBlock.Content.ToString());
                                break;
                            case '-':
                                Res += double.Parse(MyBlocks[i].GBlock.Content.ToString()) * Math.Pow((-1), Counter);
                                break;
                            case '*':
                                Res *= double.Parse(MyBlocks[i].GBlock.Content.ToString());
                                break;
                            case '/':
                                Res *= Math.Pow(double.Parse(MyBlocks[i].GBlock.Content.ToString()), Math.Pow((-1), Counter));
                                break;
                            default:
                                Res += double.Parse(MyBlocks[i].GBlock.Content.ToString());
                                break;
                        }

                        index[Counter] = i;
                        Counter++;
                    }
                }

                WaitBlockInCol[CheckColumn] = 0;
                if (Res.ToString() == Answer[CheckColumn].Text)
                {
                    BlocksInCol[CheckColumn] -= NumbersOfBlockInABox;
                    for (int i = NumbersOfBlockInABox - 1; i >= 0; i--)
                    {
                        GameGrid.Children.Remove(MyBlocks[index[i]].GBlock);
                        MyBlocks.RemoveAt(index[i]);
                    }
                    ScoreL.Content = int.Parse(ScoreL.Content.ToString()) + ScoreScope * (DecLenght + IntLenght);
                }
                else
                {
                    for (int i = 0; i < NumbersOfBlockInABox; i++)
                    {
                        MyBlocks[index[i]].stat = 2;
                        MyBlocks[index[i]].GBlock.Content = "";
                        MyBlocks[index[i]].GBlock.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Engineer Calculator;component/Files/Bricks.jpg")));
                    }
                    ScoreL.Content = int.Parse(ScoreL.Content.ToString()) - ScoreScope;
                }
            }
            CheckTimer.IsEnabled = false;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            BlockReleaseTick++;
            if (BlockReleaseTick >= BlockReleaseTime)
            {
                BlockReleaseTick = 0;
                Random R = new Random();
                int C = R.Next(0, ColumnNumbers);
                BlocksInCol[C]++;
                MyBlocks.Add(new BlockClass(C, HintNext.Content.ToString(), BlockGoSpeed,Speed));
                GameGrid.Children.Add(MyBlocks[MyBlocks.Count - 1].GBlock);
                HintNext.Content = DRandom(IntLenght, DecLenght);
            }
            if(MyBlocks.Count > ColumnNumbers*6)
            {
                Timer.IsEnabled = false;
                MessageBox.Show("You Lose!!!\nYour Score: "+ ScoreL.Content);
                GameWindow.Close();
            }
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Make a timer in WPF
            //MyBlocks = new List<BlockClass>(18);
            /*MyBlocks.RemoveRange(0, MyBlocks.Count);
            MyBlocks.Capacity = 18;*/
            Timer.Tick += Timer_Tick;
            Timer.Interval = TickReleaseSpeed;
            CheckTimer.Tick += CheckTimer_Tick;
            CheckTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            OperatorL.Content = "Operator: " + Operator;
            for (int i = 0; i < 3; i++)
            {
                //baraye reset kardane bazi
                WaitBlockInCol[i] = 0;
                BlocksInCol[i] = 0;

                //set kardane vijhegi haye textbox answer
                Answer[i] = new TextBox();
                Answer[i].GotFocus += Answer_GotFocus;
                Answer[i].KeyDown += Answer_KeyDown;
                Answer[i].KeyUp += Answer_KeyUp;
                Answer[i].Text = "?";
                Answer[i].TextAlignment = TextAlignment.Center;
                Answer[i].HorizontalContentAlignment = HorizontalAlignment.Center;
                Answer[i].VerticalContentAlignment = VerticalAlignment.Center;
                Answer[i].FontFamily = new FontFamily("Times New Roman");
                Answer[i].VerticalAlignment = VerticalAlignment.Top;
                Answer[i].HorizontalAlignment = HorizontalAlignment.Left;
                Answer[i].Margin = new Thickness(84 * i, 6 * HintNext.Height, 0, 0);
                Answer[i].FontSize = 20;
                Answer[i].Width = HintNext.Width;
                Answer[i].Height = HintNext.Height;
                Answer[i].Background = Brushes.LightGreen;
                Answer[i].BorderThickness = new Thickness(1);
                Answer[i].BorderBrush = Brushes.Black;
                Answer[i].Visibility = Visibility.Hidden;
                GameGrid.Children.Add(Answer[i]);
            }
            for (int i = 0; i < ColumnNumbers; i++)
                Answer[i].Visibility = Visibility.Visible;
/*            Draw Table and add Labels
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    BlockS[j, i] = 0;

                    Rectangle R = new Rectangle();
                    R.Stroke = Brushes.Black;
                    Grid.SetRow(R, i);
                    Grid.SetColumn(R, j);
                    GameGrid.Children.Add(R);

                    GameBlock[j, i] = new Label();
                    GameBlock[j, i].Visibility = Visibility.Hidden;
                    GameBlock[j, i].HorizontalContentAlignment = HorizontalAlignment.Center;
                    GameBlock[j, i].VerticalContentAlignment = VerticalAlignment.Center;
                    GameBlock[j, i].FontSize = 20;
                    GameBlock[j, i].Content = "?";
                    GameBlock[j, i].Background = Brushes.LightSalmon;
                    Grid.SetRow(GameBlock[j, i], i);
                    Grid.SetColumn(GameBlock[j, i], j);
                    GameGrid.Children.Add(GameBlock[j, i]);

                }
            }
            GameBlock[0, 6].Background = Brushes.LightGreen;
            GameBlock[1, 6].Background = Brushes.LightGreen;
            GameBlock[2, 6].Background = Brushes.LightGreen;

            GameBlock[0, 6].Visibility = Visibility.Visible;*/
        }

        private void Answer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
                Answer[0].Focus();
            else if (e.Key == Key.S)
                Answer[1].Focus();
            else if (e.Key == Key.D)
                Answer[2].Focus();
        }

        private void Answer_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox X = (TextBox)sender;
            X.Text = "";
        }

        private void Answer_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Decimal))
                e.Handled = true;
        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {
            CheckTimer.IsEnabled = false;
            Timer.IsEnabled = false;
            WParent.GameButton.IsEnabled = true;
        }
        
        private void CNumberSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColumnNumbers = int.Parse(CNumberSlider.Value.ToString());
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (i < ColumnNumbers)
                        Answer[i].Visibility = Visibility.Visible;
                    else
                        Answer[i].Visibility = Visibility.Hidden;
                }
                catch { }
            }
        }

        private void LoadB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog MyLoadD = new OpenFileDialog();
            if (MyLoadD.ShowDialog() == true)
            {
                MyBlocks.RemoveRange(0, MyBlocks.Count);

                string MyPath = MyLoadD.FileName;
                StreamReader MyFile = new StreamReader(MyPath);
                try
                {
                    //FirstGrid.Children.Remove(GameGrid);
                    GameGrid.Children.RemoveRange(2, MyBlocks.Count);
                    ScoreL.Content = MyFile.ReadLine();
                    string CL = MyFile.ReadLine();
                    BlocksInCol[0] = int.Parse(CL[0].ToString());
                    BlocksInCol[1] = int.Parse(CL[2].ToString());
                    BlocksInCol[2] = int.Parse(CL[4].ToString());
                    WaitBlockInCol[0] = 0;
                    WaitBlockInCol[1] = 0;
                    WaitBlockInCol[2] = 0;
                    int AllBlock = BlocksInCol[0] + BlocksInCol[1] + BlocksInCol[2];
                    for (int i = 0; i < AllBlock; i++)
                    {
                        string x = MyFile.ReadLine();
                        MyBlocks.Add(new BlockClass(x));
                        GameGrid.Children.Add(MyBlocks[i].GBlock);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { MyFile.Close(); }

            }

        }

        private void SaveB_Click(object sender, RoutedEventArgs e)
        {
            /*
            score
            block mojood dar sotun ha
            {block haye dakhele list}
            */

            SaveFileDialog MySaveD = new SaveFileDialog();
            MySaveD.CheckFileExists = false;
            if (MySaveD.ShowDialog() == true)
            {
                string MyPath = MySaveD.FileName;
                StreamWriter MyFile = new StreamWriter(MyPath);
                try
                {
                    MyFile.WriteLine(ScoreL.Content.ToString());
                    MyFile.WriteLine(BlocksInCol[0] + " " + BlocksInCol[1] + " " + BlocksInCol[2]);
                    for (int i = 0; i < MyBlocks.Count; i++)
                    {
                        MyFile.WriteLine(MyBlocks[i].ToString());
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { MyFile.Close(); }
                /* be khatere in ke natoonestam timer va label haro serialize konam majboor shodam khodam dasti dorostesh konam.
                 * System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(TextBox));
                 * BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(MyFile, Answer[0]);
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show("Failed to serialize. Reason: " + ex.Message);
                }
                finally
                {
                    MyFile.Close();
                }*/
            }
        }

        private void ExitSB_Click(object sender, RoutedEventArgs e)
        {
            SettingBox.Visibility = Visibility.Hidden;
        }

        private void RSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlockReleaseTime = Convert.ToInt32(Convert.ToDouble(RSpeedSlider.Value.ToString()));
        }

        private void PlusR_Checked(object sender, RoutedEventArgs e)
        {
            Operator = '+';
            OperatorL.Content = "Operator: " + Operator;
        }

        private void MinusR_Checked(object sender, RoutedEventArgs e)
        {
            Operator = '-';
            OperatorL.Content = "Operator: " + Operator;
        }

        private void CrossR_Checked(object sender, RoutedEventArgs e)
        {
            Operator = '*';
            OperatorL.Content = "Operator: " + Operator;
        }

        private void DivR_Checked(object sender, RoutedEventArgs e)
        {
            Operator = '/';
            OperatorL.Content = "Operator: " + Operator;
        }

        private void ILComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IntLenght = ILComboBox.SelectedIndex + 1;
        }

        private void DLComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DecLenght = DLComboBox.SelectedIndex;
        }

        private void SettingB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Timer.IsEnabled)
                StatB_MouseLeftButtonDown(sender, e);
            SettingBox.Visibility = Visibility.Visible;
        }

        private void StatB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Timer.IsEnabled)
            {
                HintNext.Content = "?";
                Timer.IsEnabled = false;
                StatB.Source = new BitmapImage(new Uri(@"pack://application:,,,/Engineer Calculator;component/Files/Play Icon.ico"));
            }
            else
            {
                HintNext.Content = DRandom(IntLenght,DecLenght);
                Timer.IsEnabled = true;
                StatB.Source = new BitmapImage(new Uri(@"pack://application:,,,/Engineer Calculator;component/Files/Pause Icon.ico"));
            }
        }
    }
}
