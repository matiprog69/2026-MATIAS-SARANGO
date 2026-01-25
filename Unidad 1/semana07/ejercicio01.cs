using System;
using System.Collections.Generic;

class Program
{
    static bool EstaBalanceado(string expresion)
    {
        Stack<char> pila = new Stack<char>();
        Dictionary<char, char> pares = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' }
        };

        foreach (char c in expresion)
        {
            if (c == '(' || c == '{' || c == '[')
            {
                pila.Push(c);
            }
            else if (c == ')' || c == '}' || c == ']')
            {
                if (pila.Count == 0 || pila.Pop() != pares[c])
                {
                    return false;
                }
            }
        }

        return pila.Count == 0;
    }

    static void Main()
    {
        Console.WriteLine("Ingrese la expresión matemática:");
        string expresion = Console.ReadLine();

        if (EstaBalanceado(expresion))
        {
            Console.WriteLine("Fórmula balanceada.");
        }
        else
        {
            Console.WriteLine("Fórmula NO balanceada.");
        }
    }
}