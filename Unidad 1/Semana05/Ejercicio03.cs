
using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        var asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
        var notas = new List<double>();

        Console.WriteLine("Introduce tu nota para cada asignatura (usa punto decimal si hace falta):");

        foreach (var asig in asignaturas)
        {
            double nota;
            while (true)
            {
                Console.Write($"{asig}: ");
                var entrada = Console.ReadLine();

                if (double.TryParse(entrada, NumberStyles.Float, CultureInfo.InvariantCulture, out nota))
                {
                    notas.Add(nota);
                    break;
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Intenta nuevamente.");
                }
            }
        }

        Console.WriteLine("\nResultados:");
        for (int i = 0; i < asignaturas.Count; i++)
        {
            Console.WriteLine($"En {asignaturas[i]} has sacado {notas[i]}");
        }
    }
}
