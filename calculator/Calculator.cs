using System;
using System.Collections.Generic;
using System.Text;

public class Evaluator
    {
        private AvailableOperationsWithPriorities operationsWithPriorities;

        public Evaluator()
        {
            operationsWithPriorities = new AvailableOperationsWithPriorities(2);
            operationsWithPriorities.AddBinaryOperationToList('+', 0, Addition);
            operationsWithPriorities.AddBinaryOperationToList('-', 0, Subtraction);
            operationsWithPriorities.AddBinaryOperationToList('*', 1, Multiplication);
            operationsWithPriorities.AddBinaryOperationToList('/', 1, Division);
        }

        private string InfixToPrefix(string infix)
        {
            Stack<string> stack = new Stack<string>();
            StringBuilder result = new StringBuilder();
            int i = 0;
            string currentDigit = null;
            while (i < infix.Length)
            {
                
                if (CharIsDigit(infix[i]))
                {
                    currentDigit = DefineDigit(infix, i);
                    result.Append(currentDigit);
                    i += currentDigit.Length;
                }
                else
                {
                    if (infix[i] == '(')
                    {
                        stack.Push(infix[i].ToString());

                    }
                    else if (infix[i] == ')')
                    {
                        while (stack.Peek() != "(")
                        {
                            result.Append($" {stack.Pop()}");
                        }
                        stack.Pop();
                    }
                    else if (operationsWithPriorities.Contains(infix[i]))
                    {
                        while (stack.Count > 0 && operationsWithPriorities.Contains(infix[i]) &&
                            operationsWithPriorities.GetPriority(stack.Peek().ToCharArray()[0]) >= operationsWithPriorities.GetPriority(infix[i]))
                        {
                            result.Append($" {stack.Pop()}");
                        }
                        result.Append(' ');
                        stack.Push(infix[i].ToString());
                    }
                    else
                    {
                        throw new Exception("Wrong string");
                    }
                    i++;
                }
                
            }
            while (stack.Count > 0)
            {
                result.Append($" {stack.Pop()}");
            }

            return result.ToString();
        }

        private bool CharIsDigit(char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double Evaluate(string value)
        {
            string str = value.Replace(" ", "");
            Console.WriteLine(str);
            str = RemoveNegativeDigits(str);
            Console.WriteLine(str);
            str = InfixToPrefix(str);
          Console.WriteLine(str);

            double secondOperand;
            double firstOperand;

            Stack<double> stack = new Stack<double>();

            int i = 0;

            string currentDigit;

            while (i < str.Length)
            {
                if (CharIsDigit(str[i]))
                {
                    currentDigit = DefineDigit(str, i);
                    i += currentDigit.Length;
                    stack.Push(Double.Parse(currentDigit));
                }
                else 
                {
                    if (operationsWithPriorities.Contains(str[i]))
                    {
                        if (stack.Count > 0)
                        {
                            secondOperand = stack.Pop();
                            firstOperand = stack.Pop();
                            stack.Push(operationsWithPriorities.PerformBinaryOperation(firstOperand, secondOperand, str[i]));
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    i++;                   
                }                
            }

            

            return stack.Pop();
        }

        private string RemoveNegativeDigits(string str)
        {
            StringBuilder result = new StringBuilder();
            bool isPrevCharOperator = true;
            int i = 0;

            while (i < str.Length)
            {
                string currentExpression = null;
                if (str[i] == '-' && isPrevCharOperator)
                {
                    if (i + 1 < str.Length)
                    {
                        if (CharIsDigit(str[i + 1]))
                        {
                            currentExpression = DefineDigit(str, i + 1);
                        }
                        else if (str[i + 1] == '(')
                        {
                            if (i + 2 < str.Length)
                            {
                                currentExpression = DefineExpressionInBrackets(str, i + 1);
                                currentExpression = RemoveNegativeDigits(str.Substring(i + 1, currentExpression.Length));
                            }    
                            else
                            {
                                throw new Exception("Wrong string!");
                            }
                            
                        }

                        if (currentExpression != null)
                        {
                            result.Append($"(0-{currentExpression})");
                            i += currentExpression.Length;
                        }
                        else
                        {
                            throw new NullReferenceException();
                        }
                        
                    }
                    
                }
                else
                {
                    if (operationsWithPriorities.Contains(str[i]) || str[i] == '(')
                    {
                        isPrevCharOperator = true;
                    }
                    else if ((CharIsDigit(str[i]) || str[i] == ')') && isPrevCharOperator)
                    {
                        isPrevCharOperator = false;
                    }
                    result.Append(str[i]);
                    
                }
                i++;
            }

            return result.ToString();
        }

        private string DefineDigit(string str, int startIndex)
        {
            int index = startIndex;
            while (index < str.Length && (CharIsDigit(str[index]) || str[index] == '.'))
            {
                index++;
            }

            if (startIndex != index)
            {
                return str.Substring(startIndex, index - startIndex);
            }
            else
            {
                return null;
            }
                
        }

        private string DefineExpressionInBrackets(string str, int startIndex)
        {
            int index = startIndex;
            while (index < str.Length && str[index] != ')')
            {
                index++;
            }

            if (startIndex != index)
            {
                return str.Substring(startIndex, index - startIndex + 1);
            }
            else
            {
                return null;
            }
        }

        private double Addition(double a, double b)
        {
            return a + b;
        }

        private double Subtraction(double a, double b)
        {
            return a - b;
        }

        private double Multiplication(double a, double b)
        {
            return a * b;
        }

        private double Division(double a, double b)
        {
            return a / b;
        }

    }


public  class AvailableOperationsWithPriorities
    {
        private List<char>[] priorities;
        private Dictionary<char, Func<double, double, double>> getOperation;


        public AvailableOperationsWithPriorities(int numberOfPriorities)
        {
            priorities = new List<char>[numberOfPriorities];
            getOperation = new Dictionary<char, Func<double, double, double>>();

            for (int i = 0; i < priorities.Length; i++)
            {
                priorities[i] = new List<char>();
            }
        }

        public void AddBinaryOperationToList(char operationChar, int priorityLevel, Func<double, double, double> func)
        {
            bool isExist = Contains(operationChar);

            if (priorityLevel >= 0 && priorityLevel < priorities.Length)
            {           
                if (!isExist)
                {
                    priorities[priorityLevel].Add(operationChar);
                    getOperation.Add(operationChar, func);
                }
            }


                
        }

        public int GetPriority(char operation)
        {
            for (int i = 0; i < priorities.Length; i++)
            {
                if (priorities[i].Contains(operation))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(char operation)
        {
            for (int i = 0; i < priorities.Length; i++)
            {
                if (priorities[i].Contains(operation))
                {
                    return true;
                }
            }
            return false;
        }

        public double PerformBinaryOperation(double arg1, double arg2, char character)
        {
            return getOperation[character](arg1,arg2);
        }
    }
