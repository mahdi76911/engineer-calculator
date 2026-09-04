using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer_Calculator
{
    class BigDouble : BigInt
    {
        BigInt Decimal = new BigInt();
        BigInt Integer = new BigInt();

        public BigDouble() : base() { }
        public BigDouble(string Num) : base(Num)
        {
            string[] N = new string[2];
            N[0] = "";
            N[1] = "";
            int B = 0;
            for (int i = 0; i < this.Number.Length; i++)
            {
                if (this.Number[i] == '.')
                    B++;
                else
                    N[B] += this.Number[i];
            }
            if (N[0] == "")
                N[0] = "0";
            if (N[1] == "")
                N[1] = "0";

            Integer.Number = N[0];
            Decimal.Number = N[1];
        }

        public BigDouble(BigInt In, BigInt De) { this.SetDoubleNumber(In, De); }

        public void SetDoubleNumber(BigInt In,BigInt De)
        {
            this.Integer = In;
            this.Decimal = De;
            this.Number = In + "." + De;
        }

        protected BigDouble DoubleZeroDel(BigDouble num)
        {
            BigDouble Res = num;
            int Len = Res.Decimal.Length;
            while (Len > 1 && Res.Decimal.Number[Len - 1] == '0')
            {
                Len--;
            }
            BigInt De = new BigInt(Res.Decimal.Number.Substring(0, Len));
            Res.SetDoubleNumber(ZeroDel(Res.Integer), De);
            //Res.SetDoubleNumber(Res.Integer, De);
            //return new BigDouble(Res.Integer.ToString() + "." + Res.Decimal.Number.Substring(0, Len));
            return Res;
        }

        protected void DoubleZeroDel() { this.Number = DoubleZeroDel(this).Number; }

        private BigInt DecimalSum(BigInt Num1, BigInt Num2)
        {

            BigInt Res = new BigInt();
            if (Num1.Length > Num2.Length)
            {
                int pow = Num1.Length - Num2.Length;
                BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                Num2.Cross(Num2, tenpow);
            }
            else
            {
                int pow = Num2.Length - Num1.Length;
                BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                Num1.Cross(Num1, tenpow);
            }
            Res = Num1.Sum(Num2);
            return Res;
        }

        public void Sum(BigDouble Num1, BigDouble Num2) { this.SetDoubleNumber(Num1.Sum(Num2).Integer, Num1.Sum(Num2).Decimal); }// +

        public void Sub(BigDouble Num1, BigDouble Num2) { this.SetDoubleNumber(Num1.Sub(Num2).Integer, Num1.Sub(Num2).Decimal); }// -

        public void Div(BigDouble Num1, BigDouble Num2, int Accuracy = 3) { this.SetDoubleNumber(Num1.Div(Num2, Accuracy).Integer, Num1.Div(Num2, Accuracy).Decimal); }// /

        public void Cross(BigDouble Num1, BigDouble Num2) { this.SetDoubleNumber(Num1.Cross(Num2).Integer, Num1.Cross(Num2).Decimal); }// *


        public BigDouble Sum(BigDouble Num)// +
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigDouble a1 = new BigDouble(this.Number.Substring(1));
                BigDouble a2 = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = a1.Sum(a2);
                Result.Integer.Number = "-" + Result.Integer.Number;
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigDouble a = new BigDouble(this.Number.Substring(1));
                BigDouble Result = Num.Sub(a);
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigDouble a = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = this.Sub(a);
                return Result;
            }


            BigInt In = new BigInt();
            BigInt De = new BigInt();
            BigInt one = new BigInt("1");
            De = DecimalSum(this.Decimal, Num.Decimal);
            In = this.Integer.Sum(Num.Integer);
            if (De.Length > Math.Max(this.Decimal.Length, Num.Decimal.Length))
            {
                In.Sum(In, one);
                De.Number = De.Number.Substring(1);
            }
            BigDouble Res = new BigDouble(In.ToString() + "." + De.ToString());
            Res.DoubleZeroDel();
            return Res;
        }

        public BigDouble Sub(BigDouble Num)// -
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigDouble a1 = new BigDouble(this.Number.Substring(1));
                BigDouble a2 = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = a2.Sub(a1);
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigDouble aa = new BigDouble(this.Number.Substring(1));
                BigDouble Result = Num.Sum(aa);
                Result.Integer.Number = "-" + Result.Integer.Number;
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigDouble aa = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = this.Sum(aa);
                return Result;
            }


            if (this.Decimal.Length > Num.Decimal.Length)
            {
                int pow = this.Decimal.Length - Num.Decimal.Length;
                BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                Num.Decimal.Cross(Num.Decimal, tenpow);
            }
            else
            {
                int pow = Num.Decimal.Length - this.Decimal.Length;
                BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                this.Decimal.Cross(this.Decimal, tenpow);
            }
            if (this.Decimal.Number == "0")
                this.Decimal.Length = Math.Max(this.Decimal.Length, Num.Decimal.Length);
            if (Num.Decimal.Number == "0")
                Num.Decimal.Length = Math.Max(this.Decimal.Length, Num.Decimal.Length);
            BigInt a = new BigInt(this.Integer.ToString() + this.Decimal.ToString());
            BigInt b = new BigInt(Num.Integer.ToString() + Num.Decimal.ToString());
            BigInt R = new BigInt();
            R.Sub(a, b);
            int x = Math.Max(a.Length, b.Length) - this.Decimal.Length;
            bool M = false;
            if (R.Number[0] == '-')
            {
                M = true;
                R.Number = R.Number.Substring(1);
                R.Length = Math.Max(a.Length, b.Length);
            }
            BigDouble Res = new BigDouble(R.Number.Substring(0, x) + "." + R.Number.Substring(x));
            if (M)
            {
                Res.Number = "-" + Res.Number;
                Res.Integer.Number = "-" + Res.Integer.Number;
            }
            return Res;
        }

        public BigDouble Div(BigDouble Num, int Accuracy = 3)// /
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigDouble a1 = new BigDouble(this.Number.Substring(1));
                BigDouble a2 = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = a1.Div(a2);
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigDouble a = new BigDouble(this.Number.Substring(1));
                BigDouble Result = Num.Div(a);
                Result.Integer.Number = "-" + Result.Integer.Number;
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigDouble a = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = this.Div(a);
                Result.Integer.Number = "-" + Result.Integer.Number;
                return Result;
            }


            //agar in if ham nabashad crash rokh nemidahad va barname kamelan dorost amal mikonad
            if (!(Num.Integer.Number == "0" && Num.Decimal.Number == "0"))
            {
                if (this.Decimal.Length > Num.Decimal.Length)
                {
                    int pow = this.Decimal.Length - Num.Decimal.Length;
                    BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                    Num.Decimal.Cross(Num.Decimal, tenpow);
                }
                else
                {
                    int pow = Num.Decimal.Length - this.Decimal.Length;
                    BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                    this.Decimal.Cross(this.Decimal, tenpow);
                }

                if (this.Decimal.Number == "0")
                    this.Decimal.Length = Math.Max(this.Decimal.Length, Num.Decimal.Length);
                if (Num.Decimal.Number == "0")
                    Num.Decimal.Length = Math.Max(this.Decimal.Length, Num.Decimal.Length);
                BigInt a = new BigInt(this.Integer.ToString() + this.Decimal.ToString());
                BigInt b = new BigInt(Num.Integer.ToString() + Num.Decimal.ToString());
                BigInt R = new BigInt();

                R.Div(a, b);
                string MyInt = R.Number;
                string MyDec = "";
                BigInt Ten = new BigInt("10");
                for (int i = 0; i < Accuracy; i++)
                {
                    BigInt r = new BigInt(a.Sub(b.Cross(R)).ToString());
                    if (r.Number == "0")
                        break;
                    r.Cross(r, Ten);
                    a = r;
                    R.Div(a, b);
                    MyDec += R.Number;
                }
                //int rem1 = int.Parse(a.Sub(b.Cross(R)).Number);

                BigDouble Res = new BigDouble(MyInt + "." + MyDec);
                return Res;
            }
            else
            {//Error
                throw new DivideByZeroException();
            }
        }

        public BigDouble Cross(BigDouble Num)// *
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigDouble a1 = new BigDouble(this.Number.Substring(1));
                BigDouble a2 = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = a1.Cross(a2);
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigDouble aa = new BigDouble(this.Number.Substring(1));
                BigDouble Result = Num.Cross(aa);
                Result.Integer.Number = "-" + Result.Integer.Number;
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigDouble aa = new BigDouble(Num.Number.Substring(1));
                BigDouble Result = this.Cross(aa);
                Result.Integer.Number = "-" + Result.Integer.Number;
                
                return Result;
            }



            if (this.Decimal.Length > Num.Decimal.Length)
            {
                int pow = this.Decimal.Length - Num.Decimal.Length;
                BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                Num.Decimal.Cross(Num.Decimal, tenpow);
            }
            else
            {
                int pow = Num.Decimal.Length - this.Decimal.Length;
                BigInt tenpow = new BigInt(Math.Pow(10, pow).ToString());

                this.Decimal.Cross(this.Decimal, tenpow);
            }
            if (this.Decimal.Number == "0")
                this.Decimal.Length = Math.Max(this.Decimal.Length, Num.Decimal.Length);
            if (Num.Decimal.Number == "0")
                Num.Decimal.Length = Math.Max(this.Decimal.Length, Num.Decimal.Length);
            BigInt a = new BigInt(this.Integer.ToString() + this.Decimal.ToString());
            BigInt b = new BigInt(Num.Integer.ToString() + Num.Decimal.ToString());
            BigDouble R = new BigDouble();
            R.Cross(a, b);
            //tedad ashar ma = this.Decimal.Length + Num.Decimal.Length;
            int x = R.Number.Length - (this.Decimal.Length + Num.Decimal.Length);
            BigDouble Res;
            if (R.Number == "0")
                Res = new BigDouble("0.0");
            else
                Res = new BigDouble(R.Number.Substring(0, x) + "." + R.Number.Substring(x));
            Res.DoubleZeroDel();
            return Res;
        }

        public override string ToString()
        {
            this.DoubleZeroDel();
            //return this.Number;  in doroste 0 haye bad az momayezesh
            return this.Integer + "." + this.Decimal;
        }
    }
}
