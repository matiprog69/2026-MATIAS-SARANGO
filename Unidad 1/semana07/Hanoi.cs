using System;
using System.Collections.Generic;

class TorresHanoi
{
    private Stack<int> torreA;
    private Stack<int> torreB;
    private Stack<int> torreC;
    private int totalDiscos;

    public TorresHanoi(int discos)
    {
        totalDiscos = discos;
        torreA = new Stack<int>();
        torreB = new Stack<int>();
        torreC = new Stack<int>();

        // Inicializar torre A con discos (más grande abajo)
        for (int i = discos; i >= 1; i--)
        {
            torreA.Push(i);
        }
    }

    public void Resolver()
    {
        MostrarTorres();
        MoverDiscos(totalDiscos, torreA, torreC, torreB);
    }

    private void MoverDiscos(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar)
    {
        if (n > 0)
        {
            MoverDiscos(n - 1, origen, auxiliar, destino);

            // Mover el disco n de origen a destino
            int disco = origen.Pop();
            destino.Push(disco);
            Console.WriteLine($"Mover disco {disco} de {NombreTorre(origen)} a {NombreTorre(destino)}");
            MostrarTorres();

            MoverDiscos(n - 1, auxiliar, destino, origen);
        }
    }

    private string NombreTorre(Stack<int> torre)
    {
        if (torre == torreA) return "A";
        if (torre == torreB) return "B";
        if (torre == torreC) return "C";
        return "?";
    }

    private void MostrarTorres()
    {
        Console.WriteLine("\nEstado actual:");
        Console.WriteLine($"Torre A: {string.Join(", ", torreA.ToArray())}");
        Console.WriteLine($"Torre B: {string.Join(", ", torreB.ToArray())}");
        Console.WriteLine($"Torre C: {string.Join(", ", torreC.ToArray())}");
        Console.WriteLine();
    }

    static void Main()
    {
        Console.WriteLine("Ingrese el número de discos para Torres de Hanoi:");
        int discos = int.Parse(Console.ReadLine());

        TorresHanoi juego = new TorresHanoi(discos);
        juego.Resolver();
        Console.WriteLine("¡Resuelto!");
    }
}