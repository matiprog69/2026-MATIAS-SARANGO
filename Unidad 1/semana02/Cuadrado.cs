// Cuadrado.cs
// Esta clase representa un cuadrado y hereda de la clase base FiguraGeometrica
// Encapsula el lado como dato privado y proporciona métodos para calcular área y perímetro

using System;

namespace FigurasGeometricas
{
    // Clase Cuadrado que hereda de FiguraGeometrica
    public class Cuadrado : FiguraGeometrica
    {
        // Campo privado para almacenar la longitud del lado del cuadrado
        // Se encapsula para proteger el dato y controlar su acceso
        private double lado;
        
        // Propiedad pública para acceder y modificar el lado
        // Incluye validación para asegurar que el lado no sea negativo
        public double Lado
        {
            get { return lado; }
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentException("El lado no puede ser negativo");
                }
                lado = value; 
            }
        }
        
        // Constructor de la clase Cuadrado
        // Inicializa el lado del cuadrado con el valor proporcionado
        public Cuadrado(double lado)
        {
            Lado = lado; // Utiliza la propiedad para aprovechar la validación
        }
        
        // Método para calcular el área del cuadrado
        // Implementa el método abstracto de la clase base
        // Fórmula: lado * lado (lado²)
        public override double CalcularArea()
        {
            return lado * lado;
        }
        
        // Método para calcular el perímetro del cuadrado
        // Implementa el método abstracto de la clase base
        // Fórmula: 4 * lado
        public override double CalcularPerimetro()
        {
            return 4 * lado;
        }
        
        // Método para mostrar información específica del cuadrado
        // Sobrescribe el método virtual de la clase base
        public override void MostrarInformacion()
        {
            Console.WriteLine("=== CUADRADO ===");
            Console.WriteLine($"Lado: {lado:F2}");
            base.MostrarInformacion(); // Llama al método de la clase base
        }
    }
}