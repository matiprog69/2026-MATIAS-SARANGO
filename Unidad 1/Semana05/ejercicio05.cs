
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var numeros = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            numeros.Add(i);
        }

        numeros.Reverse();

        // Mostrar separados por comas
        Console.WriteLine(string.Join(", ", numeros));
    }
}
