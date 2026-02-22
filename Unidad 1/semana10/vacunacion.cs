using System;
using System.Collections.Generic;
using System.Linq;

namespace CampañaVacunacion
{
    // Clase que representa a un ciudadano
    public class Ciudadano
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Vacuna { get; set; } // "Pfizer", "AstraZeneca", "Ninguna", "Ambas"

        public override string ToString()
        {
            return $"{Nombre} (ID: {Id})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Ciudadano otro)
                return this.Id == otro.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE VACUNACIÓN COVID-19 ===\n");
            Console.WriteLine("Generando datos ficticios...\n");

            // 1. Crear conjunto de 500 ciudadanos
            HashSet<Ciudadano> todosLosCiudadanos = GenerarCiudadanos(500);
            Console.WriteLine($"✓ Total ciudadanos registrados: {todosLosCiudadanos.Count}");

            // 2. Crear conjunto de 75 ciudadanos vacunados con Pfizer
            HashSet<Ciudadano> vacunadosPfizer = GenerarVacunados(todosLosCiudadanos, 75, "Pfizer");
            Console.WriteLine($"✓ Vacunados con Pfizer: {vacunadosPfizer.Count}");

            // 3. Crear conjunto de 75 ciudadanos vacunados con AstraZeneca
            HashSet<Ciudadano> vacunadosAstraZeneca = GenerarVacunados(todosLosCiudadanos, 75, "AstraZeneca", vacunadosPfizer);
            Console.WriteLine($"✓ Vacunados con AstraZeneca: {vacunadosAstraZeneca.Count}");

            // 4. Determinar ciudadanos con ambas dosis (intersección)
            HashSet<Ciudadano> ambasDosis = new HashSet<Ciudadano>(vacunadosPfizer.Intersect(vacunadosAstraZeneca));
            
            // Actualizar estado de vacunación
            foreach (var ciudadano in ambasDosis)
            {
                ciudadano.Vacuna = "Ambas";
            }

            Console.WriteLine($"✓ Ciudadanos con ambas dosis: {ambasDosis.Count}");

            // 5. Ciudadanos con una sola dosis (diferencia simétrica)
            HashSet<Ciudadano> unaDosis = new HashSet<Ciudadano>();
            unaDosis.UnionWith(vacunadosPfizer.Except(ambasDosis));
            unaDosis.UnionWith(vacunadosAstraZeneca.Except(ambasDosis));

            Console.WriteLine($"✓ Ciudadanos con una sola dosis: {unaDosis.Count}");

            // 6. Ciudadanos no vacunados (todos - (Pfizer ∪ AstraZeneca))
            HashSet<Ciudadano> vacunadosTotal = new HashSet<Ciudadano>(vacunadosPfizer);
            vacunadosTotal.UnionWith(vacunadosAstraZeneca);
            
            HashSet<Ciudadano> noVacunados = new HashSet<Ciudadano>(todosLosCiudadanos.Except(vacunadosTotal));

            Console.WriteLine($"✓ Ciudadanos NO vacunados: {noVacunados.Count}\n");

            // Mostrar resultados
            MostrarResultados(noVacunados, ambasDosis, vacunadosPfizer, vacunadosAstraZeneca, unaDosis);

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static HashSet<Ciudadano> GenerarCiudadanos(int cantidad)
        {
            HashSet<Ciudadano> ciudadanos = new HashSet<Ciudadano>();
            Random random = new Random();

            for (int i = 1; i <= cantidad; i++)
            {
                ciudadanos.Add(new Ciudadano
                {
                    Id = i,
                    Nombre = $"Ciudadano {i}",
                    Vacuna = "Ninguna"
                });
            }

            return ciudadanos;
        }

        static HashSet<Ciudadano> GenerarVacunados(HashSet<Ciudadano> todos, int cantidad, string tipoVacuna, HashSet<Ciudadano> existentes = null)
        {
            HashSet<Ciudadano> vacunados = new HashSet<Ciudadano>();
            Random random = new Random();
            List<Ciudadano> listaCiudadanos = todos.ToList();

            while (vacunados.Count < cantidad)
            {
                int indice = random.Next(0, listaCiudadanos.Count);
                Ciudadano seleccionado = listaCiudadanos[indice];

                // Evitar duplicados en el mismo conjunto
                if (!vacunados.Contains(seleccionado))
                {
                    // Si hay conjunto existente, permitir solapamiento para crear intersecciones
                    vacunados.Add(seleccionado);
                    seleccionado.Vacuna = tipoVacuna;
                }
            }

            return vacunados;
        }

        static void MostrarResultados(
            HashSet<Ciudadano> noVacunados,
            HashSet<Ciudadano> ambasDosis,
            HashSet<Ciudadano> soloPfizer,
            HashSet<Ciudadano> soloAstraZeneca,
            HashSet<Ciudadano> unaDosis)
        {
            Console.WriteLine("\n=== LISTADOS SOLICITADOS ===\n");

            // 1. Ciudadanos NO vacunados
            Console.WriteLine("1. CIUDADANOS NO VACUNADOS:");
            Console.WriteLine($"   Total: {noVacunados.Count}");
            if (noVacunados.Count > 0)
            {
                foreach (var ciudadano in noVacunados.Take(10)) // Mostrar solo primeros 10
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
                if (noVacunados.Count > 10)
                    Console.WriteLine($"   ... y {noVacunados.Count - 10} más");
            }
            Console.WriteLine();

            // 2. Ciudadanos con AMBAS DOSIS
            Console.WriteLine("2. CIUDADANOS CON AMBAS DOSIS:");
            Console.WriteLine($"   Total: {ambasDosis.Count}");
            if (ambasDosis.Count > 0)
            {
                foreach (var ciudadano in ambasDosis)
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
            }
            Console.WriteLine();

            // 3. Ciudadanos con SOLO PFIZER
            HashSet<Ciudadano> soloPfizerSet = new HashSet<Ciudadano>(soloPfizer.Except(ambasDosis));
            Console.WriteLine("3. CIUDADANOS CON SOLO PFIZER:");
            Console.WriteLine($"   Total: {soloPfizerSet.Count}");
            if (soloPfizerSet.Count > 0)
            {
                foreach (var ciudadano in soloPfizerSet.Take(10))
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
                if (soloPfizerSet.Count > 10)
                    Console.WriteLine($"   ... y {soloPfizerSet.Count - 10} más");
            }
            Console.WriteLine();

            // 4. Ciudadanos con SOLO ASTRAZENECA
            HashSet<Ciudadano> soloAstraSet = new HashSet<Ciudadano>(soloAstraZeneca.Except(ambasDosis));
            Console.WriteLine("4. CIUDADANOS CON SOLO ASTRAZENECA:");
            Console.WriteLine($"   Total: {soloAstraSet.Count}");
            if (soloAstraSet.Count > 0)
            {
                foreach (var ciudadano in soloAstraSet.Take(10))
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
                if (soloAstraSet.Count > 10)
                    Console.WriteLine($"   ... y {soloAstraSet.Count - 10} más");
            }
            Console.WriteLine();

            // Resumen estadístico
            Console.WriteLine("\n=== RESUMEN ESTADÍSTICO ===");
            Console.WriteLine($"Total población: 500 ciudadanos");
            Console.WriteLine($"✓ Vacunados (al menos una dosis): {500 - noVacunados.Count} ({((500 - noVacunados.Count) / 5.0):F1}%)");
            Console.WriteLine($"   ├─ Solo Pfizer: {soloPfizerSet.Count}");
            Console.WriteLine($"   ├─ Solo AstraZeneca: {soloAstraSet.Count}");
            Console.WriteLine($"   └─ Ambas dosis: {ambasDosis.Count}");
            Console.WriteLine($"✗ No vacunados: {noVacunados.Count} ({(noVacunados.Count / 5.0):F1}%)");
        }
    }
}