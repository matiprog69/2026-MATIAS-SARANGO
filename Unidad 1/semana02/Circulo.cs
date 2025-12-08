// Circulo.cs
// Esta clase representa un círculo y hereda de la clase base FiguraGeometrica
// Encapsula el radio como dato privado y proporciona métodos para calcular área y perímetro

using System;

namespace FigurasGeometricas
{
    // Clase Circulo que hereda de FiguraGeometrica
    public class Circulo : FiguraGeometrica
    {
        // Campo privado para almacenar el radio del círculo
        // Se encapsula para proteger el dato y controlar su acceso
        private double radio;
        
        // Propiedad pública para acceder y modificar el radio
        // Incluye validación para asegurar que el radio no sea negativo
        public double Radio
        {
            get { return radio; }
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentException("El radio no puede ser negativo");
                }
                radio = value; 
            }
        }
        
        // Constructor de la clase Circulo
        // Inicializa el radio del círculo con el valor proporcionado
        public Circulo(double radio)
        {
            Radio = radio; // Utiliza la propiedad para aprovechar la validación
        }
        
        // Método para calcular el área del círculo
        // Implementa el método abstracto de la clase base
        // Fórmula: π * r²
        public override double CalcularArea()
        {
            return Math.PI * radio * radio;
        }
        
        // Método para calcular el perímetro (circunferencia) del círculo
        // Implementa el método abstracto de la clase base
        // Fórmula: 2 * π * r
        public override double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }
        
        // Método para mostrar información específica del círculo
        // Sobrescribe el método virtual de la clase base
        public override void MostrarInformacion()
        {
            Console.WriteLine("=== CÍRCULO ===");
            Console.WriteLine($"Radio: {radio:F2}");
            base.MostrarInformacion(); // Llama al método de la clase base
        }
    }
}