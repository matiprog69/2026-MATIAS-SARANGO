// Triangulo.cs
// Esta clase representa un triángulo y hereda de la clase base FiguraGeometrica
// Encapsula la base y altura como datos privados y proporciona métodos para calcular área y perímetro

using System;

namespace FigurasGeometricas
{
    // Clase Triangulo que hereda de FiguraGeometrica
    public class Triangulo : FiguraGeometrica
    {
        // Campos privados para almacenar la base y altura del triángulo
        // Se encapsulan para proteger los datos y controlar su acceso
        private double baseTriangulo;
        private double altura;
        private double lado1;
        private double lado2;
        private double lado3;
        
        // Propiedad para la base del triángulo con validación
        public double BaseTriangulo
        {
            get { return baseTriangulo; }
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentException("La base no puede ser negativa");
                }
                baseTriangulo = value; 
            }
        }
        
        // Propiedad para la altura del triángulo con validación
        public double Altura
        {
            get { return altura; }
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentException("La altura no puede ser negativa");
                }
                altura = value; 
            }
        }
        
        // Constructor para triángulo con base y altura (para área)
        public Triangulo(double baseTriangulo, double altura)
        {
            BaseTriangulo = baseTriangulo;
            Altura = altura;
            // Para un triángulo rectángulo simple
            lado1 = baseTriangulo;
            lado2 = altura;
            lado3 = Math.Sqrt(baseTriangulo * baseTriangulo + altura * altura);
        }
        
        // Constructor completo para triángulo con tres lados
        public Triangulo(double lado1, double lado2, double lado3, double altura = 0)
        {
            if (lado1 <= 0 || lado2 <= 0 || lado3 <= 0)
            {
                throw new ArgumentException("Los lados del triángulo deben ser positivos");
            }
            
            // Validar desigualdad triangular
            if (lado1 + lado2 <= lado3 || lado1 + lado3 <= lado2 || lado2 + lado3 <= lado1)
            {
                throw new ArgumentException("Los lados no forman un triángulo válido");
            }
            
            this.lado1 = lado1;
            this.lado2 = lado2;
            this.lado3 = lado3;
            this.altura = altura;
            this.baseTriangulo = lado1; // Asumimos que la base es lado1 por defecto
        }
        
        // Método para calcular el área del triángulo
        // Implementa el método abstracto de la clase base
        // Fórmula: (base * altura) / 2
        public override double CalcularArea()
        {
            return (baseTriangulo * altura) / 2;
        }
        
        // Método para calcular el perímetro del triángulo
        // Implementa el método abstracto de la clase base
        // Fórmula: lado1 + lado2 + lado3
        public override double CalcularPerimetro()
        {
            return lado1 + lado2 + lado3;
        }
        
        // Método para mostrar información específica del triángulo
        // Sobrescribe el método virtual de la clase base
        public override void MostrarInformacion()
        {
            Console.WriteLine("=== TRIÁNGULO ===");
            Console.WriteLine($"Base: {baseTriangulo:F2}");
            Console.WriteLine($"Altura: {altura:F2}");
            Console.WriteLine($"Lados: {lado1:F2}, {lado2:F2}, {lado3:F2}");
            base.MostrarInformacion(); // Llama al método de la clase base
        }
    }
}