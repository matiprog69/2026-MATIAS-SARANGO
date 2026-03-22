using System;

namespace BinarySearchTree
{
    // Clase Nodo: representa cada elemento del árbol
    public class Nodo
    {
        public int Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // Clase ArbolBinarioBusqueda: implementa las operaciones del BST
    public class ArbolBinarioBusqueda
    {
        public Nodo Raiz { get; private set; }

        public ArbolBinarioBusqueda()
        {
            Raiz = null;
        }

        // 1. Insertar un valor en el árbol
        public void Insertar(int valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
            Console.WriteLine($" Valor {valor} insertado correctamente.");
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                return new Nodo(valor);
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            }
            else
            {
                Console.WriteLine($"El valor {valor} ya existe en el árbol. No se insertó duplicado.");
            }

            return nodo;
        }

        // 2. Buscar un valor en el árbol
        public bool Buscar(int valor)
        {
            return BuscarRecursivo(Raiz, valor);
        }

        private bool BuscarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                return false;
            }

            if (valor == nodo.Valor)
            {
                return true;
            }

            if (valor < nodo.Valor)
            {
                return BuscarRecursivo(nodo.Izquierdo, valor);
            }
            else
            {
                return BuscarRecursivo(nodo.Derecho, valor);
            }
        }

        // 3. Eliminar un valor del árbol
        public void Eliminar(int valor)
        {
            if (!Buscar(valor))
            {
                Console.WriteLine($" El valor {valor} no existe en el árbol. No se puede eliminar.");
                return;
            }

            Raiz = EliminarRecursivo(Raiz, valor);
            Console.WriteLine($" Valor {valor} eliminado correctamente.");
        }

        private Nodo EliminarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                return null;
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
            }
            else
            {
                // Caso 1: Nodo hoja (sin hijos)
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                {
                    return null;
                }
                // Caso 2: Nodo con un solo hijo
                else if (nodo.Izquierdo == null)
                {
                    return nodo.Derecho;
                }
                else if (nodo.Derecho == null)
                {
                    return nodo.Izquierdo;
                }
                // Caso 3: Nodo con dos hijos
                else
                {
                    // Encontrar el sucesor inorden (el valor mínimo del subárbol derecho)
                    Nodo sucesor = ObtenerMinimoNodo(nodo.Derecho);
                    nodo.Valor = sucesor.Valor;
                    // Eliminar el sucesor del subárbol derecho
                    nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Valor);
                }
            }

            return nodo;
        }

        // 4. Recorrido Preorden (Raíz - Izquierdo - Derecho)
        public void MostrarPreorden()
        {
            if (Raiz == null)
            {
                Console.WriteLine(" El árbol está vacío.");
                return;
            }

            Console.Write(" Recorrido Preorden: ");
            PreordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void PreordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                Console.Write($"{nodo.Valor} ");
                PreordenRecursivo(nodo.Izquierdo);
                PreordenRecursivo(nodo.Derecho);
            }
        }

        // 5. Recorrido Inorden (Izquierdo - Raíz - Derecho) - Muestra los valores ordenados
        public void MostrarInorden()
        {
            if (Raiz == null)
            {
                Console.WriteLine(" El árbol está vacío.");
                return;
            }

            Console.Write(" Recorrido Inorden (valores ordenados): ");
            InordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                InordenRecursivo(nodo.Izquierdo);
                Console.Write($"{nodo.Valor} ");
                InordenRecursivo(nodo.Derecho);
            }
        }

        // 6. Recorrido Postorden (Izquierdo - Derecho - Raíz)
        public void MostrarPostorden()
        {
            if (Raiz == null)
            {
                Console.WriteLine(" El árbol está vacío.");
                return;
            }

            Console.Write("Recorrido Postorden: ");
            PostordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void PostordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                PostordenRecursivo(nodo.Izquierdo);
                PostordenRecursivo(nodo.Derecho);
                Console.Write($"{nodo.Valor} ");
            }
        }

        // 7. Mostrar el valor mínimo
        public void MostrarMinimo()
        {
            if (Raiz == null)
            {
                Console.WriteLine("El árbol está vacío. No hay valor mínimo.");
                return;
            }

            Nodo minimo = ObtenerMinimoNodo(Raiz);
            Console.WriteLine($"Valor mínimo del árbol: {minimo.Valor}");
        }

        private Nodo ObtenerMinimoNodo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual;
        }

        // 8. Mostrar el valor máximo
        public void MostrarMaximo()
        {
            if (Raiz == null)
            {
                Console.WriteLine("El árbol está vacío. No hay valor máximo.");
                return;
            }

            Nodo maximo = ObtenerMaximoNodo(Raiz);
            Console.WriteLine($"Valor máximo del árbol: {maximo.Valor}");
        }

        private Nodo ObtenerMaximoNodo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.Derecho != null)
            {
                actual = actual.Derecho;
            }
            return actual;
        }

        // 9. Mostrar la altura del árbol
        public void MostrarAltura()
        {
            int altura = CalcularAltura(Raiz);
            Console.WriteLine($"Altura del árbol: {altura}");
        }

        private int CalcularAltura(Nodo nodo)
        {
            if (nodo == null)
            {
                return -1; // Altura de árbol vacío es -1 (o 0 según convención)
            }

            int alturaIzquierda = CalcularAltura(nodo.Izquierdo);
            int alturaDerecha = CalcularAltura(nodo.Derecho);

            return Math.Max(alturaIzquierda, alturaDerecha) + 1;
        }

        // 10. Limpiar completamente el árbol
        public void Limpiar()
        {
            Raiz = null;
            Console.WriteLine("🧹 El árbol ha sido limpiado completamente.");
        }
    }

    // Clase principal con el menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
            bool salir = false;

            Console.WriteLine("========================================");
            Console.WriteLine("   ÁRBOL BINARIO DE BÚSQUEDA (BST)");
            Console.WriteLine("========================================");

            while (!salir)
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Buscar valor");
                Console.WriteLine("3. Eliminar valor");
                Console.WriteLine("4. Mostrar recorrido Preorden");
                Console.WriteLine("5. Mostrar recorrido Inorden (valores ordenados)");
                Console.WriteLine("6. Mostrar recorrido Postorden");
                Console.WriteLine("7. Mostrar valor mínimo");
                Console.WriteLine("8. Mostrar valor máximo");
                Console.WriteLine("9. Mostrar altura del árbol");
                Console.WriteLine("10. Limpiar árbol");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el valor a insertar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorInsertar))
                        {
                            arbol.Insertar(valorInsertar);
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido. Debe ser un número entero.");
                        }
                        break;

                    case "2":
                        Console.Write("Ingrese el valor a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorBuscar))
                        {
                            bool encontrado = arbol.Buscar(valorBuscar);
                            if (encontrado)
                            {
                                Console.WriteLine($"El valor {valorBuscar} SÍ existe en el árbol.");
                            }
                            else
                            {
                                Console.WriteLine($"El valor {valorBuscar} NO existe en el árbol.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido. Debe ser un número entero.");
                        }
                        break;

                    case "3":
                        Console.Write("Ingrese el valor a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorEliminar))
                        {
                            arbol.Eliminar(valorEliminar);
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido. Debe ser un número entero.");
                        }
                        break;

                    case "4":
                        arbol.MostrarPreorden();
                        break;

                    case "5":
                        arbol.MostrarInorden();
                        break;

                    case "6":
                        arbol.MostrarPostorden();
                        break;

                    case "7":
                        arbol.MostrarMinimo();
                        break;

                    case "8":
                        arbol.MostrarMaximo();
                        break;

                    case "9":
                        arbol.MostrarAltura();
                        break;

                    case "10":
                        arbol.Limpiar();
                        break;

                    case "0":
                        salir = true;
                        Console.WriteLine("\n ¡Gracias por usar el programa! Hasta luego.");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione una opción del 0 al 10.");
                        break;
                }
            }
        }
    }
}