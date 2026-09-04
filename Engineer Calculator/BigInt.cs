using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer_Calculator
{
    class BigInt
    {
        //Variables:
        string number;
        private int length;
        //Properties:
        public string Number { get { return number; } set { number = value; length = number.Length; } }
        public int Length
        {
            get { return length; }  //dorost neshan dadan length dar adad manfi.
            set
            {
                if (Length > value)
                {
                    Number = Number.Substring(Length - value);
                }
                else if (Length < value)
                {
                    string Zeros = "";
                    for (int i = 0; i < value - Length; i++)
                        Zeros += "0";
                    Number = Zeros + Number;
                }
            }
        }
        //Constructors:
        public BigInt() { Number = "0"; }
        public BigInt(string Num)
        {
            string res = Num;
            int c = 0;
            bool M = false;
            if (res[0] == '-')
                M = true;
            for (int i = 0; i < Num.Length; i++)
            {
                if (res[i] == '0' || res[i] == '-')
                    c++;
                else
                    break;
            }
            if (c == Num.Length)
                res = "0";
            else
            {
                res = res.Substring(c);
                if (M)
                    res = "-" + res;
            }
            Number = res;
        }
        public BigInt(int Len) { Number = "0"; Length = Len; }

        //Methods:

        protected int ChtoI(char a)
        { return a - 48; }

        private void Minus()
        {
            if (this.Number[0] == '-')
                this.Number = this.Number.Substring(1);
            else
                this.Number = "-" + this.Number;
        }

        protected BigInt ZeroDel(BigInt num)
        {
            string res = num.Number;
            int c = 0;
            bool M = false;
            if (res[0] == '-')
                M = true;
            for (int i = 0; i < num.Length; i++)
            {
                if (res[i] == '0' || res[i] == '-')
                    c++;
                else
                    break;
            }
            if (c == num.Length)
                res = "0";
            else
            {
                res = res.Substring(c);
                if (M)
                    res = "-" + res;
            }
            BigInt Res = new BigInt(res);
            return Res;
        }//0 haye avale adad ra hazf mikonad

        protected void ZeroDel() { this.Number = ZeroDel(this).Number; }

        protected BigInt Supplement(BigInt num)
        {
            string res = "";
            for (int i = 0; i < num.Length; i++)
            {
                res += 9 - ChtoI(num.Number[i]);
            }
            BigInt One = new BigInt("1");
            BigInt Res = new BigInt(res);
            Res.Sum(Res, One);
            return Res;
        }//mokamel 10

        protected BigInt Supplement(BigInt num, int Num)//mokamele 10 e Num(10 ^ Num)
        {
            num.Length = Num;
            return Supplement(num);
        }
        protected void Supplement()
        {
            this.Number = Supplement(this).Number;
        }

        public void Sum(BigInt Num1, BigInt Num2) { this.Number = Num1.Sum(Num2).Number; }// Num1 + Num2

        public void Sub(BigInt Num1, BigInt Num2) { this.Number = Num1.Sub(Num2).Number; }// Num1 - Num2

        public void Div(BigInt Num1, BigInt Num2) { this.Number = Num1.Div(Num2).Number; }// Num1 / Num2

        public void Cross(BigInt Num1, BigInt Num2) { this.Number = Num1.Cross(Num2).Number; }// Num1 * Num2

        public void Power(BigInt Num1, BigInt Num2) { this.Number = Num1.Power(Num2).Number; }// Num1 ^ Num2

        public void Remainder(BigInt Num1, BigInt Num2) { this.Number = Num1.Remainder(Num2).Number; }// Num1 % Num2

        public void PP() { this.Number = this.Sum(new BigInt("1")).Number; }// this++

        public void MM() { this.Number = this.Sub(new BigInt("1")).Number; }// this--

        public BigInt Sum(BigInt Num)// this + Num
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigInt a1 = new BigInt(this.Number.Substring(1));
                BigInt a2 = new BigInt(Num.Number.Substring(1));
                BigInt Result = a1.Sum(a2);
                Result.Minus();
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigInt a = new BigInt(this.Number.Substring(1));
                BigInt Result = Num.Sub(a);
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigInt a = new BigInt(Num.Number.Substring(1));
                BigInt Result = this.Sub(a);
                return Result;
            }


            string res = "";
            if (Num.Length < this.Length)
                Num.Length = this.Length;
            else
                this.Length = Num.Length;
            int Carry = 0;
            for (int i = this.Length - 1; i >= 0; i--)
            {
                res = (ChtoI(this.Number[i]) + ChtoI(Num.Number[i]) + Carry) % 10 + res;
                Carry = (ChtoI(this.Number[i]) + ChtoI(Num.Number[i]) + Carry) / 10;
            }
            if (Carry != 0)
                res = Carry + res;
            BigInt Res = new BigInt(res);
            Res.ZeroDel();
            this.ZeroDel();//hazf 0 haye ezafe shode
            Num.ZeroDel();
            return Res;
        }

        public BigInt Sub(BigInt Num)// this - Num
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigInt a1 = new BigInt(this.Number.Substring(1));
                BigInt a2 = new BigInt(Num.Number.Substring(1));
                BigInt Result = a2.Sub(a1);
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigInt a = new BigInt(this.Number.Substring(1));
                BigInt Result = Num.Sum(a);
                Result.Minus();
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigInt a = new BigInt(Num.Number.Substring(1));
                BigInt Result = this.Sum(a);
                return Result;
            }


            // be ravesh mokamel 10 hal mikonam(hamooni ke too madar khoondim!)
            BigInt SNum = Supplement(Num, Math.Max(this.Length, Num.Length));
            BigInt Res = this.Sum(SNum);
            //carry akhar dashtan yani "+" boodan.
            if (Res.Length > Math.Max(this.Length, Num.Length)) //true yani carry dashtan.
            {
                Res.Number = Res.Number.Substring(1);
            }
            else
            {
                Res.Supplement();
                Res.Minus();
            }
            Res.ZeroDel();
            return Res;
        }

        public BigInt Div(BigInt Num)// this / Num
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigInt a1 = new BigInt(this.Number.Substring(1));
                BigInt a2 = new BigInt(Num.Number.Substring(1));
                BigInt Result = a1.Div(a2);
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigInt a = new BigInt(this.Number.Substring(1));
                BigInt Result = Num.Div(a);
                Result.Minus();
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigInt a = new BigInt(Num.Number.Substring(1));
                BigInt Result = this.Div(a);
                Result.Minus();
                return Result;
            }


            Num.ZeroDel();
            if (Num.Number != "0")
            {
                string res = "", x = "";
                BigInt X = new BigInt();
                for (int i = 0; i < this.Length; i++)
                {
                    x += this.Number[i];
                    int k = 0;
                    X.Number = x;
                    while (X.Sub(Num).Number[0] != '-')
                    {
                        X.Sub(X, Num);
                        k++;
                    }
                    x = X.Number;
                    res += k;
                }
                BigInt Res = new BigInt(res);
                Res.ZeroDel();
                return Res;
            }
            else
            {   //Error
                throw new DivideByZeroException();
            }
        }

        public BigInt Cross(BigInt Num)// this * Num
        {
            //baraye kar ba adad manfi
            if (this.Number[0] == '-' && Num.Number[0] == '-')
            {
                BigInt a1 = new BigInt(this.Number.Substring(1));
                BigInt a2 = new BigInt(Num.Number.Substring(1));
                BigInt Result = a1.Cross(a2);
                return Result;
            }
            else if (this.Number[0] == '-')
            {
                BigInt aa = new BigInt(this.Number.Substring(1));
                BigInt Result = Num.Cross(aa);
                Result.Minus();
                return Result;
            }
            else if (Num.Number[0] == '-')
            {
                BigInt aa = new BigInt(Num.Number.Substring(1));
                BigInt Result = this.Cross(aa);
                Result.Minus();
                return Result;
            }


            BigInt Res = new BigInt();
            int Minus = 1;
            BigInt a = this;
            BigInt b = Num;
            if (a.Number[0] == '-')
            {
                a.Minus();
                Minus *= -1;
            }
            if (b.Number[0] == '-')
            {
                b.Minus();
                Minus *= -1;
            }

            for (int i = b.Length - 1; i >= 0; i--)
            {
                string X = "";
                for (int k = 0; k < (b.Length - 1) - i; k++)
                    X = X + "0";
                int carry = 0;
                for (int j = a.Length - 1; j >= 0; j--)
                {
                    X = (ChtoI(b.Number[i]) * ChtoI(a.Number[j]) + carry) % 10 + X;
                    carry = (ChtoI(b.Number[i]) * ChtoI(a.Number[j]) + carry) / 10;
                }
                if (carry != 0)
                    X = carry + X;
                BigInt x = new BigInt(X);
                Res.Sum(Res, x);
            }
            if (Minus == -1)
                Res.Minus();
            Res.ZeroDel();
            return Res;
        }

        public BigInt Power(BigInt Num)//this ^ Num
        {
            BigInt res = new BigInt("1");
            BigInt C = new BigInt("0");
            while (C.ToString() != Num.ToString())
            {
                C.Sum(C, new BigInt("1"));
                res.Cross(res, this);
            }
            return res;
        }

        public BigInt Remainder(BigInt Num)// this % Num
        {
            return this.Sub(this.Div(Num).Cross(Num));
        }

        public BigInt Factorial(UInt64 x)
        {
            BigInt res = new BigInt("1");
            for (UInt64 i = x; i > 1; i--)
            {
                BigInt y = new BigInt(i.ToString());
                res.Cross(res, y);
            }
            return res;
        }

        public void Radical(BigInt Num)
        { }

        public override string ToString()
        {
            this.ZeroDel();
            return this.Number;
        }
    }
}
