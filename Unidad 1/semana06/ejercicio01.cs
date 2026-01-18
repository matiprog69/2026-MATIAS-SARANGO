using System;

namespace ListasEnlazadas
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
        
        // Método para agregar elementos al final
        public void AgregarFinal(int dato)
        {
            Nodo nuevoNodo = new Nodo(dato);
            
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                return;
            }
            
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
        
        // Función que calcula el número de elementos
        public int ContarElementos()
        {
            int contador = 0;
            Nodo actual = cabeza;
            
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            
            return contador;
        }
        
        // Método para mostrar la lista
        public void MostrarLista()
        {
            Nodo actual = cabeza;
            while (actual != null)
            {
                Console.Write(actual.Dato + " -> ");
                actual = actual.Siguiente;
            }
            Console.WriteLine("null");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaEnlazada lista = new ListaEnlazada();
            
            // Agregar algunos elementos de prueba
            lista.AgregarFinal(10);
            lista.AgregarFinal(20);
            lista.AgregarFinal(30);
            lista.AgregarFinal(40);
            lista.AgregarFinal(50);
            
            Console.WriteLine("Lista enlazada:");
            lista.MostrarLista();
            
            int cantidad = lista.ContarElementos();
            Console.WriteLine($"\nNúmero de elementos en la lista: {cantidad}");
            
            // Caso especial: lista vacía
            ListaEnlazada listaVacia = new ListaEnlazada();
            Console.WriteLine($"\nElementos en lista vacía: {listaVacia.ContarElementos()}");
        }
    }
}