using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer_Calculator
{
    class BigComplex : BigDouble
    {
        //a + bi
        //a Real       b Image
        BigDouble Real = new BigDouble();
        BigDouble Image = new BigDouble();

        public BigComplex() : this(new BigDouble(), new BigDouble()) { }
        public BigComplex(BigDouble Re, BigDouble Im) { this.SetComplexNumber(Re, Im); }

        public void SetComplexNumber(BigDouble Re, BigDouble Im)
        {
            this.Real = Re;
            this.Image = Im;
            this.Number = Real.ToString() + " + " + Image.ToString() + "i";
        }

        public void ComplexZeroDel()
        {
            this.Real = DoubleZeroDel(this.Real);
            this.Image = DoubleZeroDel(this.Image);
        }

        public void Sum(BigComplex Num1, BigComplex Num2) { this.SetComplexNumber(Num1.Sum(Num2).Real, Num1.Sum(Num2).Image); }// +

        public void Sub(BigComplex Num1, BigComplex Num2) { this.SetComplexNumber(Num1.Sub(Num2).Real, Num1.Sub(Num2).Image); }// -

        public void Div(BigComplex Num1, BigComplex Num2) { this.SetComplexNumber(Num1.Div(Num2).Real, Num1.Div(Num2).Image); }// /

        public void Cross(BigComplex Num1, BigComplex Num2) { this.SetComplexNumber(Num1.Cross(Num2).Real, Num1.Cross(Num2).Image); }// *

        public BigComplex Sum(BigComplex Num)// +
        {
            BigComplex Res = new BigComplex();
            Res.Real = this.Real.Sum(Num.Real);
            Res.Image = this.Image.Sum(Num.Image);
            return Res;
        }

        public BigComplex Sub(BigComplex Num)// -
        {
            BigComplex Res = new BigComplex();
            Res.Real = this.Real.Sub(Num.Real);
            Res.Image = this.Image.Sub(Num.Image);
            return Res;
        }

        public BigComplex Div(BigComplex Num)// /
        {
            if (!(Num.Real.ToString() == "0.0" && Num.Image.ToString() == "0.0"))
            {
                BigComplex Res = new BigComplex();
                BigDouble Minus = Num.Image;
                if (Minus.Number[0] == '-')
                    Minus.Number = Minus.Number.Substring(1);
                else
                    Minus.Number = "-" + Minus.Number;
                BigComplex X = new BigComplex(Num.Real, Minus);
                Res.Real.Div(this.Cross(X).Real, Num.Cross(X).Real);
                Res.Image.Div(this.Cross(X).Image, Num.Cross(X).Real);
                return Res;
            }
            else
            {
                throw new DivideByZeroException();
            }
        }

        public BigComplex Cross(BigComplex Num)// *
        {
            BigComplex Res = new BigComplex();
            Res.Real.Sub(this.Real.Cross(Num.Real), this.Image.Cross(Num.Image));
            Res.Image.Sum(this.Real.Cross(Num.Image), this.Image.Cross(Num.Real));
            return Res;
        }

        public override string ToString()
        {
            this.ComplexZeroDel();
            if(Image.ToString()[0]=='-')
                return Real.ToString() + " - " + Image.ToString().Substring(1) + "i";
            else
                return Real.ToString() + " + " + Image.ToString() + "i";
        }

    }
}