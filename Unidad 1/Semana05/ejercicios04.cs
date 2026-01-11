
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const int cantidad = 6;
        var ganadores = new List<int>();

        Console.WriteLine($"Introduce {cantidad} números ganadores (enteros):");

        while (ganadores.Count < cantidad)
        {
            Console.Write($"Número {ganadores.Count + 1}: ");
            var entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int n))
            {
                if (!ganadores.Contains(n))
                {
                    ganadores.Add(n);
                }
                else
                {
                    Console.WriteLine("Ese número ya fue introducido. Intenta con otro.");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida. Debe ser un entero.");
            }
        }

        ganadores.Sort();

        Console.WriteLine("\nNúmeros ganadores (ordenados de menor a mayor):");
        foreach (var n in ganadores)
        {
            Console.WriteLine(n);
        }
    }
}
