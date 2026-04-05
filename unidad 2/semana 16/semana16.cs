using System;
using System.Collections.Generic;
using System.Linq;

namespace VuelosBaratos
{
    /// <summary>
    /// Representa una arista (vuelo) con destino y costo
    /// </summary>
    public class Arista
    {
        public string Destino { get; set; }
        public double Costo { get; set; }

        public Arista(string destino, double costo)
        {
            Destino = destino;
            Costo = costo;
        }
    }

    /// <summary>
    /// Grafo dirigido ponderado para representar vuelos
    /// </summary>
    public class GrafoVuelos
    {
        private Dictionary<string, List<Arista>> adyacencia;

        public GrafoVuelos()
        {
            adyacencia = new Dictionary<string, List<Arista>>();
        }

        /// <summary>
        /// Agrega una ciudad (nodo) al grafo
        /// </summary>
        public void AgregarCiudad(string ciudad)
        {
            if (!adyacencia.ContainsKey(ciudad))
            {
                adyacencia[ciudad] = new List<Arista>();
            }
        }

        /// <summary>
        /// Agrega un vuelo dirigido con su costo
        /// </summary>
        public bool AgregarVuelo(string origen, string destino, double costo)
        {
            if (!adyacencia.ContainsKey(origen))
                AgregarCiudad(origen);
            if (!adyacencia.ContainsKey(destino))
                AgregarCiudad(destino);

            foreach (var arista in adyacencia[origen])
            {
                if (arista.Destino == destino && arista.Costo == costo)
                    return false;
            }

            adyacencia[origen].Add(new Arista(destino, costo));
            return true;
        }

        /// <summary>
        /// Retorna lista de todas las ciudades
        /// </summary>
        public List<string> ObtenerCiudades()
        {
            return new List<string>(adyacencia.Keys);
        }

        /// <summary>
        /// Retorna lista de todos los vuelos
        /// </summary>
        public List<(string Origen, string Destino, double Costo)> ObtenerVuelos()
        {
            var vuelos = new List<(string, string, double)>();
            foreach (var origen in adyacencia)
            {
                foreach (var arista in origen.Value)
                {
                    vuelos.Add((origen.Key, arista.Destino, arista.Costo));
                }
            }
            return vuelos;
        }

        /// <summary>
        /// Cola de prioridad manual para Dijkstra
        /// Implementación simple usando lista y ordenamiento
        /// </summary>
        private class ColaPrioridad
        {
            private List<(string ciudad, double distancia)> elementos = new List<(string, double)>();

            public void Encolar(string ciudad, double distancia)
            {
                elementos.Add((ciudad, distancia));
                // Ordenar por distancia ascendente
                elementos = elementos.OrderBy(e => e.distancia).ToList();
            }

            public string Desencolar()
            {
                if (elementos.Count == 0)
                    return null;
                string ciudad = elementos[0].ciudad;
                elementos.RemoveAt(0);
                return ciudad;
            }

            public bool EstaVacia()
            {
                return elementos.Count == 0;
            }

            public void ActualizarDistancia(string ciudad, double nuevaDistancia)
            {
                // Eliminar entrada anterior si existe
                elementos.RemoveAll(e => e.ciudad == ciudad);
                Encolar(ciudad, nuevaDistancia);
            }

            public bool Contiene(string ciudad)
            {
                return elementos.Any(e => e.ciudad == ciudad);
            }
        }

        /// <summary>
        /// Algoritmo de Dijkstra implementado manualmente
        /// Retorna: (lista de ciudades en orden, costo_total) o null si no hay ruta
        /// </summary>
        public (List<string> Ruta, double CostoTotal)? RutaMasBarata(string inicio, string fin)
        {
            if (!adyacencia.ContainsKey(inicio) || !adyacencia.ContainsKey(fin))
                return null;

            // Inicializar distancias
            var distancias = new Dictionary<string, double>();
            var predecesores = new Dictionary<string, string>();
            var ciudades = adyacencia.Keys.ToList();

            foreach (var ciudad in ciudades)
            {
                distancias[ciudad] = double.PositiveInfinity;
                predecesores[ciudad] = null;
            }
            distancias[inicio] = 0;

            // Cola de prioridad manual
            var cola = new ColaPrioridad();
            cola.Encolar(inicio, 0);

            var visitados = new HashSet<string>();

            while (!cola.EstaVacia())
            {
                string ciudadActual = cola.Desencolar();

                if (visitados.Contains(ciudadActual))
                    continue;
                visitados.Add(ciudadActual);

                if (ciudadActual == fin)
                    break;

                foreach (var arista in adyacencia[ciudadActual])
                {
                    double nuevaDist = distancias[ciudadActual] + arista.Costo;
                    if (nuevaDist < distancias[arista.Destino])
                    {
                        distancias[arista.Destino] = nuevaDist;
                        predecesores[arista.Destino] = ciudadActual;
                        
                        if (cola.Contiene(arista.Destino))
                            cola.ActualizarDistancia(arista.Destino, nuevaDist);
                        else
                            cola.Encolar(arista.Destino, nuevaDist);
                    }
                }
            }

            if (double.IsPositiveInfinity(distancias[fin]))
                return null;

            // Reconstruir ruta
            var ruta = new List<string>();
            string actual = fin;
            while (actual != null)
            {
                ruta.Add(actual);
                actual = predecesores[actual];
            }
            ruta.Reverse();

            return (ruta, distancias[fin]);
        }
    }

    class Program
    {
        static GrafoVuelos CargarDatosEjemplo()
        {
            var grafo = new GrafoVuelos();

            // Base de datos ficticia
            var vuelos = new List<(string, string, double)>
            {
                ("Quito", "Guayaquil", 85), ("Quito", "Cuenca", 95),
                ("Quito", "Miami", 450), ("Quito", "Bogotá", 180),
                ("Guayaquil", "Quito", 85), ("Guayaquil", "Cuenca", 55),
                ("Guayaquil", "Galápagos", 120), ("Guayaquil", "Lima", 210),
                ("Cuenca", "Quito", 95), ("Cuenca", "Guayaquil", 55),
                ("Cuenca", "Lima", 280), ("Miami", "Quito", 450),
                ("Miami", "Bogotá", 320), ("Miami", "Nueva York", 180),
                ("Bogotá", "Quito", 180), ("Bogotá", "Miami", 320),
                ("Bogotá", "Lima", 150), ("Lima", "Guayaquil", 210),
                ("Lima", "Cuenca", 280), ("Lima", "Bogotá", 150),
                ("Lima", "Santiago", 200), ("Galápagos", "Guayaquil", 120),
                ("Nueva York", "Miami", 180), ("Nueva York", "Lima", 520),
                ("Santiago", "Lima", 200), ("Santiago", "Buenos Aires", 180),
                ("Buenos Aires", "Santiago", 180), ("Buenos Aires", "Lima", 350)
            };

            foreach (var (origen, destino, costo) in vuelos)
                grafo.AgregarVuelo(origen, destino, costo);

            return grafo;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE BÚSQUEDA DE VUELOS BARATOS ===\n");
            var grafo = CargarDatosEjemplo();
            Console.WriteLine($"Base cargada: {grafo.ObtenerCiudades().Count} ciudades, {grafo.ObtenerVuelos().Count} vuelos\n");

            while (true)
            {
                Console.WriteLine("\n1. Ver ciudades");
                Console.WriteLine("2. Ver vuelos");
                Console.WriteLine("3. Buscar ruta más barata");
                Console.WriteLine("4. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    Console.WriteLine("\nCiudades:");
                    var ciudades = grafo.ObtenerCiudades();
                    ciudades.Sort();
                    foreach (var c in ciudades) Console.WriteLine($"  - {c}");
                }
                else if (opcion == "2")
                {
                    Console.WriteLine("\nVuelos:");
                    var vuelos = grafo.ObtenerVuelos();
                    foreach (var v in vuelos.OrderBy(v => v.Origen))
                        Console.WriteLine($"  {v.Origen} -> {v.Destino}: ${v.Costo}");
                }
                else if (opcion == "3")
                {
                    Console.Write("\nOrigen: ");
                    string origen = Console.ReadLine();
                    Console.Write("Destino: ");
                    string destino = Console.ReadLine();
                    
                    var result = grafo.RutaMasBarata(origen, destino);
                    if (result == null)
                        Console.WriteLine($"\nNo hay ruta de {origen} a {destino}");
                    else
                        Console.WriteLine($"\nRuta: {string.Join(" -> ", result.Value.Ruta)} | Costo: ${result.Value.CostoTotal}");
                }
                else if (opcion == "4")
                    break;
                else
                    Console.WriteLine("Opción inválida");
            }
        }
    }
}