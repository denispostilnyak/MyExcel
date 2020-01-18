using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace lab_2
{
    public class ToPolandNotation
    {
        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }
        static private bool IsOperator(string s)
        {
            if (s == "+" || s == "-" || s == "inc" || s == "dec" || s == "%" || s == "/" || s == "^" || s == "i" || s == "d" || s == "(" || s == ")")  
                return true;
            return false;
        }
        static private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(": return 0;
                case ")": return 1;
                case "+": return 2;
                case "-": return 3;
                case "%": return 4;
                case "/": return 4;
                case "^": return 5;
                case "i": return 2;
                case "d": return 2; 
                    
                default: return 7;
            }
        }
         public int Calculate(string input)
        {
            string output = GetExpression(input); 
            int result = Counting(output); 
            return result; 
        }
        static private int Counting(string input)
        {
            int k = 0;
            int result = 0; 
            string res1 = input;
            Stack<int> temp = new Stack<int>(); 
            
            for (int i = 0; i < input.Length; i++) 
            {
                
                if (Char.IsDigit(input[i]) && res1.IndexOf('i') == -1 && res1.IndexOf('d') == -1 ) 
                {
                    string a = string.Empty;
                   
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i].ToString())) 
                    {
                        a += input[i]; 
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(int.Parse(a)); 
                    i--;
                    k++;
                }
                else if (IsOperator(input[i].ToString())) 
                {
                   
                    

                    switch (input[i].ToString()) 
                    {
                        case "+": {
                                
                                int b = temp.Pop(); result = b; break; }
                        case "-": {
                                
                                int b = temp.Pop(); result = -b; break; }
                        case "%": {
                                int a = temp.Pop();
                                int b = temp.Pop(); result = b %a; break; }
                        case "/": {
                                int a = temp.Pop();
                                int b = temp.Pop(); result = b / a; break; }
                        case "^": {
                                int a = temp.Pop();
                                int b = temp.Pop(); result = int.Parse(Math.Pow(int.Parse(b.ToString()), int.Parse(a.ToString())).ToString()); break; }
                        case "i": {
                                int a = int.Parse(input[i + 1].ToString());
                                result = a + 1;
                                input = input.Replace(input[i + 1].ToString(), "");
                               res1 = res1.Replace("i", "");
                                break;
                            }
                        case "d":
                            {
                                int a = int.Parse(input[i + 1].ToString());
                                result = a -1;
                                input = input.Replace(input[i + 1].ToString(), "");
                                res1 = res1.Replace("d", "");
                                break;
                            }
                    }
                    temp.Push(result); 
                }
            }
            return temp.Peek(); 
        }
        static private string GetExpression(string input)
        {
           
            string output = string.Empty; 
            Stack<char> operStack = new Stack<char>(); 

            for (int i = 0; i < input.Length; i++) 
            {
                
                
                if (IsDelimeter(input[i]))
                    continue; 

               
                if (Char.IsDigit(input[i])) 
                {
                    
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i].ToString()))
                    {
                        output += input[i]; 
                        i++; 

                        if (i == input.Length) break; 
                    }

                    output += " ";
                    i--; 
                }
                int t1 = (int)input[i];
               
                int k = i;
                if (t1 >= 97 && t1 <= 122) 
                {
                    while(!char.IsDigit(input[k]))
                    {
                        output += input[k];
                        k++;
                        if (i == input.Length) break;
                    }
                    if (output == "inc")
                    {
                        output = "i";
                        input = input.Replace("inc", "i");
                    }
                    if (output == "dec")
                    {
                        output = "d";
                        input = input.Replace("dec", "d");
                    }
                  
                }
                if (IsOperator(input[i].ToString())) 
                {
                    if (input[i] == '(') 
                        operStack.Push(input[i]); 
                    else if (input[i] == ')') 
                    {
                       
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else 
                    {
                        if (operStack.Count > 0) 
                        {
                            if (GetPriority(input[i].ToString()) <= GetPriority(operStack.Peek().ToString())) 
                                output += operStack.Pop().ToString() + " "; 
                            if (operStack.Peek().ToString() == "i" || operStack.Peek().ToString() == "d")

                            {
                                operStack.Pop();
                            }
                        }



                        if (input[i].ToString() != "i" && input[i].ToString() != "d") 
                        operStack.Push(char.Parse(input[i].ToString())); 

                    }
                }
            }

            
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; 
        }
       
    }

}

