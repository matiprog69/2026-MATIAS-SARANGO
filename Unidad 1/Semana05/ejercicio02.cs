
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };

        foreach (var asig in asignaturas)
        {
            Console.WriteLine($"Yo estudio {asig}");
        }
    }
}
