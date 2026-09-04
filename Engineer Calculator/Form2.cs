using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engineer_Calculator
{
    public partial class Form2 : Form
    {
        private MainWindow WParent;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(MainWindow parent)
        {
            WParent = parent;
            InitializeComponent();
        }

        public void funbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void X_Click(object sender, EventArgs e)
        {

        }

        private void funresult_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstVar_TextChanged(object sender, EventArgs e)
        {

        }

        private void SecondVar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("3x^2y^3+5xy :جملات باید بصورت روبرو باشد" + "\n (y) و (x) فقط با متغیر های کوچک" + "\n بدون فاصله(اسپیس) میان جملات" + "\n اگر متغیر ها مقدار دهی شده باشند این مقادیر در ضریب جملات مشتق اعمال خواهد شد ");
        }

        private void result_Click(object sender, EventArgs e)
        {
            if (funbox.Text != "")
            {
                double result = 0;
                int[] alamat;
                char[] spliter = { '+', '-' };
                string[] stm; //stm = statment . jomle haye moadele
                stm = funbox.Text.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
                alamat = new int[stm.Length];

                if (funbox.Text[0] == '-')
                    alamat[0] = -1;
                else
                    alamat[0] = 1;
                for (int i = 1, cc = 1; i < funbox.Text.Length; i++)
                {
                    if (funbox.Text[i] == '-')
                    {
                        alamat[cc] = -1;
                        cc++;
                    }
                    if (funbox.Text[i] == '+')
                    {
                        alamat[cc] = 1;
                        cc++;
                    }
                }
                double[] pow = new double[2];
                int zarib;
                for (int i = 0; i < stm.Length; i++)
                {
                    switch (stm[i].Length)
                    {
                        case 1:
                            if (stm[i] == "x")  //x
                            {
                                //stm[i] = Xvalue.Text;
                                result += int.Parse(Xvalue.Text.ToString()) * alamat[i];
                            }
                            else if (stm[i] == "y") //y
                            {
                                //stm[i] = Yvalue.Text;
                                result += int.Parse(Yvalue.Text.ToString()) * alamat[i];
                            }
                            else    //5
                            {
                                result += int.Parse(stm[i].ToString()) * alamat[i];
                            }
                            break;
                        case 2:
                            if (stm[i][0] == 'x' || stm[i][0] == 'y') //xy
                            {
                                result += int.Parse(Xvalue.Text.ToString()) * int.Parse(Yvalue.Text.ToString()) * alamat[i];
                            }
                            else    //6x || 7y
                            {
                                zarib = int.Parse(stm[i][0].ToString());
                                if (stm[i][1] == 'x')
                                {
                                    result += zarib * int.Parse(Xvalue.Text.ToString()) * alamat[i];
                                }
                                else
                                {
                                    result += zarib * int.Parse(Yvalue.Text.ToString()) * alamat[i];
                                }

                            }

                            break;
                        case 3:
                            if (stm[i][0] == 'x')   //x^3
                            {
                                pow[0] = int.Parse(stm[i][2].ToString());
                                result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * alamat[i];
                            }
                            else if (stm[i][0] == 'y')  //y^4
                            {
                                pow[1] = int.Parse(stm[i][2].ToString());
                                result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * alamat[i];
                            }
                            else    //6xy
                            {
                                zarib = int.Parse(stm[i][0].ToString());
                                result += zarib * int.Parse(Xvalue.Text.ToString()) * int.Parse(Yvalue.Text.ToString()) * alamat[i];
                            }
                            break;
                        case 4:
                            if (stm[i][0] == 'x' || stm[i][0] == 'y')
                            {
                                if (stm[i][1] == '^')
                                {
                                    pow[0] = int.Parse(stm[i][2].ToString());
                                    if (stm[i][0] == 'x')   //x^2y
                                    {
                                        result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * int.Parse(Yvalue.Text.ToString()) * alamat[i];
                                    }
                                    else    //y^2x
                                    {
                                        result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * int.Parse(Xvalue.Text.ToString()) * alamat[i];
                                    }
                                }
                                else
                                {
                                    pow[0] = int.Parse(stm[i][3].ToString());
                                    if (stm[i][0] == 'x')   //xy^2
                                    {
                                        result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * int.Parse(Xvalue.Text.ToString()) * alamat[i];
                                    }
                                    else    //yx^3
                                    {
                                        result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * int.Parse(Yvalue.Text.ToString()) * alamat[i];
                                    }
                                }
                            }
                            else
                            {
                                pow[0] = int.Parse(stm[i][3].ToString());
                                zarib = int.Parse(stm[i][0].ToString());
                                if (stm[i][1] == 'x')   //6x^4
                                {
                                    result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * zarib * alamat[i];
                                }
                                else    //2y^5
                                {
                                    result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * zarib * alamat[i];
                                }
                            }
                            break;
                        case 5:
                            zarib = int.Parse(stm[i][0].ToString());
                            if (stm[i][2] == '^')
                            {
                                pow[0] = int.Parse(stm[i][3].ToString());
                                if (stm[i][1] == 'x')   //6x^2y
                                {
                                    result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * int.Parse(Yvalue.Text.ToString()) * zarib * alamat[i];
                                }
                                else    //6y^2x
                                {
                                    result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * int.Parse(Xvalue.Text.ToString()) * zarib * alamat[i];
                                }
                            }
                            else
                            {
                                pow[0] = int.Parse(stm[i][4].ToString());
                                if (stm[i][1] == 'x')   //6xy^2
                                {
                                    result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * int.Parse(Xvalue.Text.ToString()) * zarib * alamat[i];
                                }
                                else    //5yx^2
                                {
                                    result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * int.Parse(Yvalue.Text.ToString()) * zarib * alamat[i];
                                }
                            }

                            break;
                        case 6:
                            pow[0] = int.Parse(stm[i][2].ToString());
                            pow[1] = int.Parse(stm[i][5].ToString());
                            if (stm[i][0] == 'x')   //x^2y^6
                            {
                                result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * alamat[i];
                            }
                            else    //y^2x^6
                            {
                                result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[1]) * alamat[i];
                            }
                            break;
                        case 7:
                            zarib = int.Parse(stm[i][0].ToString());
                            pow[0] = int.Parse(stm[i][3].ToString());
                            pow[1] = int.Parse(stm[i][6].ToString());
                            if (stm[i][0] == 'x')   //6x^2y^2
                            {
                                result += Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * zarib * alamat[i];
                            }
                            else    //5y^2x^3
                            {
                                result += Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[1]) * zarib * alamat[i];
                            }
                            break;
                        default:
                            ;
                            break;

                    }
                }
                label1.Text = result.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void xdif_Click(object sender, EventArgs e)
        {
            if (funbox.Text != "")
            {
                double result = 0;
                int[] alamat;
                char[] spliter = { '+', '-' };
                string[] stm; //stm = statment . jomle haye moadele
                stm = funbox.Text.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
                alamat = new int[stm.Length];

                if (funbox.Text[0] == '-')
                    alamat[0] = -1;
                else
                    alamat[0] = 1;
                for (int i = 1, cc = 1; i < funbox.Text.Length; i++)
                {
                    if (funbox.Text[i] == '-')
                    {
                        alamat[cc] = -1;
                        cc++;
                    }
                    if (funbox.Text[i] == '+')
                    {
                        alamat[cc] = 1;
                        cc++;
                    }
                }
                double[] pow = new double[2];
                double zarib;
                string[] dif = new string[stm.Length];
                for (int i = 0; i < stm.Length; i++)
                {
                    switch (stm[i].Length)
                    {
                        case 1:
                            if (stm[i] == "x")  //x
                            {
                                dif[i] = "1";
                            }
                            else              //y || 6
                            {
                                dif[i] = "";
                            }
                            break;
                        case 2:
                            if (stm[i][0] == 'x') //xy
                            {
                                if (Yvalue.Text != "")
                                {
                                    dif[i] = Yvalue.Text;
                                }
                                else
                                {
                                    dif[i] = "y";
                                }
                            }
                            else if (stm[i][0] == 'y') //yx
                            {
                                if (Yvalue.Text != "")
                                {
                                    dif[i] = Yvalue.Text;
                                }
                                else
                                {
                                    dif[i] = "y";
                                }
                            }
                            else     //6x
                            {
                                if (stm[i][1] == 'x')
                                {
                                    dif[i] = stm[i][0].ToString();
                                }
                                else //7y
                                {
                                    dif[i] = "";
                                }

                            }

                            break;
                        case 3:
                            if (stm[i][0] == 'x')   //x^3
                            {
                                zarib = int.Parse(stm[i][2].ToString());
                                pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                            }
                            else if (stm[i][0] == 'y')  //y^4
                            {
                                dif[i] = "";
                            }
                            else    //6xy
                            {
                                if (Yvalue.Text != "")
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * int.Parse(Yvalue.Text.ToString());
                                    dif[i] = zarib.ToString();
                                }
                                else
                                {
                                    dif[i] = stm[i][0].ToString() + 'y';
                                }
                            }
                            break;
                        case 4:
                            if (stm[i][0] == 'x' || stm[i][0] == 'y')
                            {
                                if (stm[i][1] == '^')
                                {
                                    pow[0] = int.Parse(stm[i][2].ToString());
                                    if (stm[i][0] == 'x')   //x^2y
                                    {
                                        if (Yvalue.Text != "")
                                        {
                                            zarib = int.Parse(stm[i][2].ToString()) * int.Parse(Yvalue.Text.ToString());
                                            pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                        }
                                        else
                                        {
                                            zarib = int.Parse(stm[i][2].ToString());
                                            pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString() + 'y';
                                        }

                                    }
                                    else    //y^2x
                                    {
                                        if (Yvalue.Text != "")
                                        {
                                            zarib = Math.Pow(double.Parse(Yvalue.Text.ToString()), double.Parse(stm[i][2].ToString()));
                                            dif[i] = zarib.ToString();
                                        }
                                        else
                                        {
                                            dif[i] = 'y'.ToString() + '^'.ToString() + stm[i][2];
                                        }
                                    }
                                }
                                else
                                {
                                    pow[0] = int.Parse(stm[i][3].ToString());
                                    if (stm[i][0] == 'x')   //xy^2
                                    {
                                        if (Yvalue.Text != "")
                                        {
                                            dif[i] = Math.Pow(double.Parse(Yvalue.Text.ToString()), double.Parse(stm[i][3].ToString())).ToString();
                                        }
                                        else
                                        {
                                            dif[i] = 'y'.ToString() + '^'.ToString() + stm[i][3];
                                        }
                                    }
                                    else    //yx^3
                                    {

                                        if (Yvalue.Text != "")
                                        {
                                            zarib = int.Parse(Yvalue.Text.ToString()) * int.Parse(stm[i][3].ToString());
                                            pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                        }
                                        else
                                        {
                                            zarib = int.Parse(stm[i][3].ToString());
                                            pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'y' + 'x' + '^' + pow[0].ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString());
                                if (stm[i][1] == 'x')   //6x^4
                                {
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                }
                                else    //2y^5
                                {
                                    dif[i] = "";
                                }
                            }
                            break;
                        case 5:
                            if (stm[i][2] == '^')
                            {

                                if (stm[i][1] == 'x')   //6x^2y
                                {
                                    pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                    if (Yvalue.Text != "")
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString()) * int.Parse(Yvalue.Text.ToString());
                                        dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                    }
                                    else
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString());
                                        dif[i] = zarib.ToString() + 'y' + 'x' + '^' + pow[0].ToString();

                                    }

                                }
                                else    //6y^2x
                                {
                                    if (Yvalue.Text != "")
                                    {
                                        pow[0] = int.Parse(stm[i][3].ToString());
                                        zarib = Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * int.Parse(stm[i][0].ToString());
                                        dif[i] = zarib.ToString();
                                    }
                                    else
                                    {
                                        dif[i] = stm[i][0].ToString() + 'y' + '^' + stm[i][3].ToString();
                                    }
                                }
                            }
                            else
                            {

                                if (stm[i][1] == 'x')   //6xy^2
                                {
                                    if (Yvalue.Text != "")
                                    {
                                        pow[0] = double.Parse(stm[i][4].ToString());
                                        zarib = Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[0]) * int.Parse(stm[i][0].ToString());
                                        dif[i] = zarib.ToString();
                                    }
                                    else
                                    {
                                        dif[i] = stm[i][0].ToString() + 'y' + '^' + stm[i][4].ToString();
                                    }
                                }
                                else    //5yx^2
                                {
                                    pow[0] = double.Parse(stm[i][4].ToString()) - 1;
                                    if (Yvalue.Text != "")
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][4].ToString()) * int.Parse(Yvalue.Text.ToString());
                                        dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                    }
                                    else
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][4].ToString());
                                        dif[i] = zarib.ToString() + 'y' + 'x' + '^' + pow[0].ToString();

                                    }
                                }
                            }

                            break;
                        case 6:

                            if (stm[i][0] == 'x')   //x^2y^6
                            {
                                pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][5].ToString());

                                if (Yvalue.Text != "")
                                {
                                    zarib = Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][2].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][2].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[1].ToString() + 'x' + '^' + pow[0].ToString();
                                }
                            }
                            else    //y^2x^6
                            {
                                pow[0] = int.Parse(stm[i][5].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][2].ToString());
                                if (Yvalue.Text != "")
                                {
                                    zarib = Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][5].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][5].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[1].ToString() + 'x' + '^' + pow[0].ToString();
                                }

                            }
                            break;
                        case 7:


                            if (stm[i][1] == 'x')   //6x^2y^2
                            {
                                pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][6].ToString());
                                if (Yvalue.Text != "")
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][3].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[1].ToString() + 'x' + '^' + pow[0].ToString();
                                }
                            }
                            else    //5y^2x^3
                            {
                                pow[0] = int.Parse(stm[i][6].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][3].ToString());
                                if (Yvalue.Text != "")
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * Math.Pow(double.Parse(Yvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][6].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][6].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[1].ToString() + 'x' + '^' + pow[0].ToString();
                                }
                            }
                            break;
                        default:
                            ;
                            break;

                    }
                }
                difx.Text = "";
                if (alamat[0] == -1)
                {
                    if (dif[0] != "")
                    {
                        difx.Text += '-'.ToString() + dif[0].ToString();
                    }
                }
                else
                {
                    if (dif[0] != "")
                    {
                        difx.Text += dif[0].ToString();
                    }
                }
                for (int j = 1; j < alamat.Length; j++)
                {
                    if (alamat[j] == -1)
                    {
                        if (dif[j] != "")
                        {
                            difx.Text += '-'.ToString() + dif[j].ToString();
                        }
                    }
                    else
                    {
                        if (dif[j] != "")
                        {
                            difx.Text += '+'.ToString() + dif[j];
                        }
                    }
                }
                if (difx.Text[0] == '+')
                {
                    difx.Text = difx.Text.Substring(1);
                }
            }
        }

        private void ydif_Click(object sender, EventArgs e)
        {
            if (funbox.Text != "")
            {
                double result = 0;
                int[] alamat;
                char[] spliter = { '+', '-' };
                string[] stm; //stm = statment . jomle haye moadele
                stm = funbox.Text.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
                alamat = new int[stm.Length];

                if (funbox.Text[0] == '-')
                    alamat[0] = -1;
                else
                    alamat[0] = 1;
                for (int i = 1, cc = 1; i < funbox.Text.Length; i++)
                {
                    if (funbox.Text[i] == '-')
                    {
                        alamat[cc] = -1;
                        cc++;
                    }
                    if (funbox.Text[i] == '+')
                    {
                        alamat[cc] = 1;
                        cc++;
                    }
                }
                double[] pow = new double[2];
                double zarib;
                string[] dif = new string[stm.Length];
                for (int i = 0; i < stm.Length; i++)
                {
                    switch (stm[i].Length)
                    {
                        case 1:
                            if (stm[i] == "y")  //y
                            {
                                dif[i] = "1";
                            }
                            else               //x || 5
                            {
                                dif[i] = "";
                            }
                            break;
                        case 2:
                            if (stm[i][0] == 'x') //xy
                            {
                                if (Xvalue.Text != "")
                                {
                                    dif[i] = Xvalue.Text;
                                }
                                else
                                {
                                    dif[i] = "x";
                                }
                            }
                            else if (stm[i][0] == 'y') //yx
                            {
                                if (Xvalue.Text != "")
                                {
                                    dif[i] = Xvalue.Text;
                                }
                                else
                                {
                                    dif[i] = "x";
                                }
                            }
                            else     //6x
                            {
                                if (stm[i][1] == 'x')
                                {
                                    dif[i] = "";
                                }
                                else //7y
                                {
                                    dif[i] = stm[i][0].ToString();
                                }

                            }

                            break;
                        case 3:
                            if (stm[i][0] == 'x')   //x^3
                            {
                                dif[i] = "";
                            }
                            else if (stm[i][0] == 'y')  //y^4
                            {

                                zarib = int.Parse(stm[i][2].ToString());
                                pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                            }
                            else    //6xy
                            {
                                if (Xvalue.Text != "")
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * int.Parse(Xvalue.Text.ToString());
                                    dif[i] = zarib.ToString();
                                }
                                else
                                {
                                    dif[i] = stm[i][0].ToString() + 'x';
                                }
                            }
                            break;
                        case 4:
                            if (stm[i][0] == 'x' || stm[i][0] == 'y')
                            {
                                if (stm[i][1] == '^')
                                {
                                    pow[0] = int.Parse(stm[i][2].ToString());
                                    if (stm[i][0] == 'x')   //x^2y
                                    {
                                        if (Xvalue.Text != "")
                                        {
                                            zarib = Math.Pow(double.Parse(Xvalue.Text.ToString()), double.Parse(stm[i][2].ToString()));
                                            dif[i] = zarib.ToString();
                                        }
                                        else
                                        {
                                            dif[i] = 'x'.ToString() + '^'.ToString() + stm[i][2];
                                        }

                                    }
                                    else    //y^2x
                                    {
                                        if (Xvalue.Text != "")
                                        {
                                            zarib = int.Parse(stm[i][2].ToString()) * int.Parse(Xvalue.Text.ToString());
                                            pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                        }
                                        else
                                        {
                                            zarib = int.Parse(stm[i][2].ToString());
                                            pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'x' + 'y' + '^' + pow[0].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    pow[0] = int.Parse(stm[i][3].ToString());
                                    if (stm[i][0] == 'x')   //xy^2
                                    {
                                        if (Xvalue.Text != "")
                                        {
                                            zarib = int.Parse(Xvalue.Text.ToString()) * int.Parse(stm[i][3].ToString());
                                            pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                        }
                                        else
                                        {
                                            zarib = int.Parse(stm[i][3].ToString());
                                            pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                            dif[i] = zarib.ToString() + 'x' + 'y' + '^' + pow[0].ToString();
                                        }
                                    }
                                    else    //yx^3
                                    {

                                        if (Xvalue.Text != "")
                                        {
                                            dif[i] = Math.Pow(double.Parse(Xvalue.Text.ToString()), double.Parse(stm[i][3].ToString())).ToString();
                                        }
                                        else
                                        {
                                            dif[i] = 'x'.ToString() + '^'.ToString() + stm[i][3];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString());
                                if (stm[i][1] == 'x')   //6x^4
                                {
                                    dif[i] = "";
                                }
                                else    //2y^5
                                {
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                }
                            }
                            break;
                        case 5:

                            if (stm[i][2] == '^')
                            {

                                if (stm[i][1] == 'y')   //6y^2x
                                {
                                    pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                    if (Xvalue.Text != "")
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString()) * int.Parse(Xvalue.Text.ToString());
                                        dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                    }
                                    else
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString());
                                        dif[i] = zarib.ToString() + 'x' + 'y' + '^' + pow[0].ToString();

                                    }

                                }
                                else    //6x^2y
                                {
                                    if (Xvalue.Text != "")
                                    {
                                        pow[0] = int.Parse(stm[i][3].ToString());
                                        zarib = Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * int.Parse(stm[i][0].ToString());
                                        dif[i] = zarib.ToString();
                                    }
                                    else
                                    {
                                        dif[i] = stm[i][0].ToString() + 'x' + '^' + stm[i][3].ToString();
                                    }
                                }
                            }
                            else
                            {

                                if (stm[i][1] == 'y')   //6yx^2
                                {
                                    if (Xvalue.Text != "")
                                    {
                                        pow[0] = double.Parse(stm[i][4].ToString());
                                        zarib = Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[0]) * int.Parse(stm[i][0].ToString());
                                        dif[i] = zarib.ToString();
                                    }
                                    else
                                    {
                                        dif[i] = stm[i][0].ToString() + 'x' + '^' + stm[i][4].ToString();
                                    }
                                }
                                else    //5xy^2
                                {
                                    pow[0] = double.Parse(stm[i][4].ToString()) - 1;
                                    if (Xvalue.Text != "")
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][4].ToString()) * int.Parse(Xvalue.Text.ToString());
                                        dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                    }
                                    else
                                    {
                                        zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][4].ToString());
                                        dif[i] = zarib.ToString() + 'x' + 'y' + '^' + pow[0].ToString();

                                    }
                                }
                            }

                            break;
                        case 6:

                            if (stm[i][0] == 'y')   //y^2x^6
                            {
                                pow[0] = int.Parse(stm[i][2].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][5].ToString());

                                if (Xvalue.Text != "")
                                {
                                    zarib = Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][2].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][2].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[1].ToString() + 'y' + '^' + pow[0].ToString();
                                }
                            }
                            else    //x^2y^6
                            {
                                pow[0] = int.Parse(stm[i][5].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][2].ToString());
                                if (Xvalue.Text != "")
                                {
                                    zarib = Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][5].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][5].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[1].ToString() + 'y' + '^' + pow[0].ToString();
                                }

                            }
                            break;
                        case 7:


                            if (stm[i][1] == 'y')   //6y^2x^2
                            {
                                pow[0] = int.Parse(stm[i][3].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][6].ToString());
                                if (Xvalue.Text != "")
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][3].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][3].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[1].ToString() + 'y' + '^' + pow[0].ToString();
                                }
                            }
                            else    //5x^2y^3
                            {
                                pow[0] = int.Parse(stm[i][6].ToString()) - 1;
                                pow[1] = int.Parse(stm[i][3].ToString());
                                if (Xvalue.Text != "")
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * Math.Pow(double.Parse(Xvalue.Text.ToString()), pow[1]) * int.Parse(stm[i][6].ToString());
                                    dif[i] = zarib.ToString() + 'y' + '^' + pow[0].ToString();
                                }
                                else
                                {
                                    zarib = int.Parse(stm[i][0].ToString()) * int.Parse(stm[i][6].ToString());
                                    dif[i] = zarib.ToString() + 'x' + '^' + pow[1].ToString() + 'y' + '^' + pow[0].ToString();
                                }
                            }
                            break;
                        default:
                            ;
                            break;

                    }
                }
                dify.Text = "";
                if (alamat[0] == -1)
                {
                    if (dif[0] != "")
                    {
                        dify.Text += '-'.ToString() + dif[0].ToString();
                    }
                }
                else
                {
                    if (dif[0] != "")
                    {
                        dify.Text += dif[0].ToString();
                    }
                }
                for (int j = 1; j < alamat.Length; j++)
                {
                    if (alamat[j] == -1)
                    {
                        if (dif[j] != "")
                        {
                            dify.Text += '-'.ToString() + dif[j].ToString();
                        }
                    }
                    else
                    {
                        if (dif[j] != "")
                        {
                            dify.Text += '+'.ToString() + dif[j];
                        }
                    }
                }
                if (dify.Text[0] == '+')
                {
                    dify.Text = dify.Text.Substring(1);
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            WParent.DevB.IsEnabled = true;
        }
    }
}
