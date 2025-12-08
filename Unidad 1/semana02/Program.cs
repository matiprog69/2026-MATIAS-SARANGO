// Program.cs
// Este es el programa principal que demuestra el uso de las clases de figuras geométricas
// Crea instancias de diferentes figuras y muestra sus propiedades calculadas

using System;

namespace FigurasGeometricas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PROGRAMA DE FIGURAS GEOMÉTRICAS ===\n");
            
            try
            {
                // Crear un círculo con radio 5
                Console.WriteLine("1. CREANDO UN CÍRCULO:");
                Circulo miCirculo = new Circulo(5.0);
                miCirculo.MostrarInformacion();
                
                Console.WriteLine("\n" + new string('-', 50) + "\n");
                
                // Crear un cuadrado con lado 4
                Console.WriteLine("2. CREANDO UN CUADRADO:");
                Cuadrado miCuadrado = new Cuadrado(4.0);
                miCuadrado.MostrarInformacion();
                
                Console.WriteLine("\n" + new string('-', 50) + "\n");
                
                // Crear un triángulo con base 6 y altura 8
                Console.WriteLine("3. CREANDO UN TRIÁNGULO:");
                Triangulo miTriangulo = new Triangulo(6.0, 8.0);
                miTriangulo.MostrarInformacion();
                
                Console.WriteLine("\n" + new string('-', 50) + "\n");
                
                // Demostración de polimorfismo
                Console.WriteLine("4. DEMOSTRACIÓN DE POLIMORFISMO:");
                Console.WriteLine("Usando la clase base para manejar diferentes figuras:\n");
                
                FiguraGeometrica[] figuras = new FiguraGeometrica[3];
                figuras[0] = new Circulo(3.0);
                figuras[1] = new Cuadrado(5.0);
                figuras[2] = new Triangulo(4.0, 3.0);
                
                foreach (var figura in figuras)
                {
                    // Polimorfismo: mismo método, comportamientos diferentes
                    Console.WriteLine($"Tipo: {figura.GetType().Name}");
                    Console.WriteLine($"Área: {figura.CalcularArea():F2}");
                    Console.WriteLine($"Perímetro: {figura.CalcularPerimetro():F2}");
                    Console.WriteLine();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}