
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };

        Console.WriteLine("Asignaturas del curso:");
        foreach (var asig in asignaturas)
        {
            Console.WriteLine(asig);
        }
    }
}
