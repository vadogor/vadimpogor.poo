using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculate
    {
        private string operation;
        public string Operation
        {
            get
            {
                return operation;
            }
            set
            {
                operation = value;
            }
        }

        private double firstValue = 0;
        public double FirstValue
        {
            get
            {
                return firstValue;
            }
            set
            {
                firstValue = value;
            }
        }

        public double Plus(double secondV)
        {
            firstValue += secondV;
            return firstValue;
        }
        public double Minus(double secondV)
        {
            firstValue -= secondV;
            return firstValue;
        }
        public double Multiply(double secondV)
        {
            firstValue *= secondV;
            return firstValue;
        }
        public double Divide(double secondV)
        {
            firstValue /= secondV;
            return firstValue;
        }
        public double Power(double secondV)
        {
            return secondV * secondV;
        }
        public double Sqrt(double secondV)
        {
            return Math.Sqrt(secondV);
        }
        public double Inverse(double secondV)
        {
            return 1 / secondV;
        }
        public double Percent(double secondV)
        {
            switch (Operation)
            {
                case "+":
                    return firstValue * secondV / 100;
                case "-":
                    return firstValue * secondV / 100;
                case "*":
                case "/":
                    return secondV / 100;
                default:
                    return secondV;
            }
        }

    }
}
