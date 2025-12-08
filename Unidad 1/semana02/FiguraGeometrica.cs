// FiguraGeometrica.cs
// Esta es la clase base abstracta que define la estructura común para todas las figuras geométricas
// Contiene los métodos abstractos que deben implementar las clases derivadas

using System;

namespace FigurasGeometricas
{
    // Clase abstracta que sirve como base para todas las figuras geométricas
    public abstract class FiguraGeometrica
    {
        // Método abstracto para calcular el área
        // Cada figura debe implementar su propia lógica para calcular el área
        public abstract double CalcularArea();
        
        // Método abstracto para calcular el perímetro
        // Cada figura debe implementar su propia lógica para calcular el perímetro
        public abstract double CalcularPerimetro();
        
        // Método virtual para mostrar información de la figura
        // Puede ser sobrescrito por las clases derivadas si necesitan mostrar información adicional
        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Área: {CalcularArea():F2}");
            Console.WriteLine($"Perímetro: {CalcularPerimetro():F2}");
        }
    }
}