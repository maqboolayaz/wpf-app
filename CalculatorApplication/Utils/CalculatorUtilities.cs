using CalculatorApplication.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorApplication.Utils
{
    public enum Operations
    {
        Add,
        Substruct,
        Multiply,
        Divide,
        Reset,
        Fib,
        Equal,
        Empty
    }

    public class CalculatorUtilities : ICalculatorOperations
    {
        public string Expression { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;

        public CalculatorUtilities()
        {
        }

        private void Reset()
        {
            Expression = Result = string.Empty;
        }

        private void Equal()
        {
            Result = CalculateExpression();
            Expression = Result;
        }

        public void AddInput(string digit)
        {
            if (Regex.IsMatch(digit, @"[+\-*/%,]"))
                Expression += string.IsNullOrEmpty(Expression) || Regex.IsMatch(Expression, @"[+\-*/%]$") ? string.Empty : digit;
            else
                Expression += digit;
        }

        public void AddOperation(Operations operation)
        {
            switch (operation)
            {
                case Operations.Reset:
                    Reset();
                    break;
                case Operations.Equal:
                    Equal();
                    break;
                case Operations.Fib:
                    Fib();
                    break;
            }
        }

        private void Fib()
        {
            if (string.IsNullOrEmpty(Expression)) return;
            Expression = Fibonacci(Convert.ToInt32(Expression)).ToString();
        }

        public static int Fibonacci(int n)
        {
            if (n == 0) return 0;
            else if (n == 1) return 1;
            else
            {
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }
        }

        string CalculateExpression()
        {
            var listOfDigits = Regex.Split(Expression, @"\D").Select(Convert.ToDouble).ToList();
            var listOfOperations = Regex.Split(Expression, @"(-|\+|\*|\/)").Select(sign =>
            {
                switch (sign)
                {
                    case "+":
                        return Operations.Add;
                    case "-":
                        return Operations.Substruct;
                    case "*":
                        return Operations.Multiply;
                    case "/":
                        return Operations.Divide;
                    default:
                        return Operations.Empty;
                }
            }).Where(x => x != Operations.Empty).ToList();

            if (listOfDigits.Count > listOfOperations.Count)
            {
                var CurrentOperation = listOfOperations[0];
                var result = listOfDigits[0];
                for (int index = 1; index < listOfDigits.Count; index++)
                {
                    var digit = listOfDigits[index];
                    switch (CurrentOperation)
                    {
                        case Operations.Add:
                            result += digit;
                            break;
                        case Operations.Substruct:
                            result -= digit;
                            break;
                        case Operations.Multiply:
                            result *= digit;
                            break;
                        case Operations.Divide:
                            result /= digit;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                return result.ToString(CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }
    }
}
