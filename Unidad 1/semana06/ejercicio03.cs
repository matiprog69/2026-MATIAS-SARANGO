using System;

namespace ListasEnlazadasBusqueda
{
    public class Nodo
    {
        public int Dato { get; set; }
        public Nodo Siguiente { get; set; }
        
        public Nodo(int dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    public class ListaEnlazada
    {
        private Nodo cabeza;
        
        public ListaEnlazada()
        {
            cabeza = null;
        }
        
        // Método para agregar elementos
        public void Agregar(int dato)
        {
            Nodo nuevoNodo = new Nodo(dato);
            
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                Nodo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
        }
        
        // Método de búsqueda que retorna el número de ocurrencias
        public int BuscarOcurrencias(int valorBuscado)
        {
            int contador = 0;
            Nodo actual = cabeza;
            
            while (actual != null)
            {
                if (actual.Dato == valorBuscado)
                {
                    contador++;
                }
                actual = actual.Siguiente;
            }
            
            if (contador == 0)
            {
                Console.WriteLine($"El dato {valorBuscado} no fue encontrado en la lista.");
            }
            else
            {
                Console.WriteLine($"El dato {valorBuscado} aparece {contador} vez/veces en la lista.");
            }
            
            return contador;
        }
        
        // Método para mostrar la lista
        public void MostrarLista()
        {
            Nodo actual = cabeza;
            Console.Write("Lista: ");
            while (actual != null)
            {
                Console.Write(actual.Dato + " ");
                actual = actual.Siguiente;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaEnlazada lista = new ListaEnlazada();
            
            // Agregar elementos de prueba con repeticiones
            lista.Agregar(5);
            lista.Agregar(10);
            lista.Agregar(5);
            lista.Agregar(15);
            lista.Agregar(10);
            lista.Agregar(5);
            lista.Agregar(20);
            lista.Agregar(5);
            
            Console.WriteLine("=== MÉTODO DE BÚSQUEDA DE OCURRENCIAS ===");
            lista.MostrarLista();
            Console.WriteLine();
            
            // Caso 1: Dato que existe múltiples veces
            int valor1 = 5;
            int ocurrencias1 = lista.BuscarOcurrencias(valor1);
            
            // Caso 2: Dato que existe una vez
            int valor2 = 15;
            int ocurrencias2 = lista.BuscarOcurrencias(valor2);
            
            // Caso 3: Dato que no existe
            int valor3 = 100;
            int ocurrencias3 = lista.BuscarOcurrencias(valor3);
            
            // Caso 4: Dato que existe varias veces
            int valor4 = 10;
            int ocurrencias4 = lista.BuscarOcurrencias(valor4);
            
            Console.WriteLine("\nResumen de búsquedas:");
            Console.WriteLine($"- {valor1}: {ocurrencias1} ocurrencias");
            Console.WriteLine($"- {valor2}: {ocurrencias2} ocurrencias");
            Console.WriteLine($"- {valor3}: {ocurrencias3} ocurrencias");
            Console.WriteLine($"- {valor4}: {ocurrencias4} ocurrencias");
            
            // Caso especial: lista vacía
            Console.WriteLine("\n=== PRUEBA CON LISTA VACÍA ===");
            ListaEnlazada listaVacia = new ListaEnlazada();
            Console.WriteLine("Lista vacía creada");
            int ocurrenciasVacia = listaVacia.BuscarOcurrencias(5);
            Console.WriteLine($"Ocurrencias en lista vacía: {ocurrenciasVacia}");
        }
    }
}