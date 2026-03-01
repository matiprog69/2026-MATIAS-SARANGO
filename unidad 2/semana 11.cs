using System;
using System.Collections.Generic;
using System.Linq;

namespace TraductorBasico
{
    class Program
    {
        static Dictionary<string, string> diccionarioEspanolIngles = new Dictionary<string, string>();
        static Dictionary<string, string> diccionarioInglesEspanol = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            InicializarDiccionario();
            MostrarMenu();
        }

        static void InicializarDiccionario()
        {
            // Diccionario Español -> Inglés
            diccionarioEspanolIngles.Add("tiempo", "time");
            diccionarioEspanolIngles.Add("persona", "person");
            diccionarioEspanolIngles.Add("año", "year");
            diccionarioEspanolIngles.Add("camino", "way");
            diccionarioEspanolIngles.Add("forma", "way");
            diccionarioEspanolIngles.Add("día", "day");
            diccionarioEspanolIngles.Add("cosa", "thing");
            diccionarioEspanolIngles.Add("hombre", "man");
            diccionarioEspanolIngles.Add("mundo", "world");
            diccionarioEspanolIngles.Add("vida", "life");
            diccionarioEspanolIngles.Add("mano", "hand");
            diccionarioEspanolIngles.Add("parte", "part");
            diccionarioEspanolIngles.Add("niño", "child");
            diccionarioEspanolIngles.Add("niña", "child");
            diccionarioEspanolIngles.Add("ojo", "eye");
            diccionarioEspanolIngles.Add("mujer", "woman");
            diccionarioEspanolIngles.Add("lugar", "place");
            diccionarioEspanolIngles.Add("trabajo", "work");
            diccionarioEspanolIngles.Add("semana", "week");
            diccionarioEspanolIngles.Add("caso", "case");
            diccionarioEspanolIngles.Add("punto", "point");
            diccionarioEspanolIngles.Add("tema", "point");
            diccionarioEspanolIngles.Add("gobierno", "government");
            diccionarioEspanolIngles.Add("empresa", "company");
            diccionarioEspanolIngles.Add("compañía", "company");

            // Diccionario Inglés -> Español (para la traducción inversa)
            foreach (var item in diccionarioEspanolIngles)
            {
                if (!diccionarioInglesEspanol.ContainsKey(item.Value))
                {
                    diccionarioInglesEspanol.Add(item.Value, item.Key);
                }
            }
        }

        static void MostrarMenu()
        {
            int opcion;
            
            do
            {
                Console.Clear();
                Console.WriteLine("==================== MENÚ ====================");
                Console.WriteLine();
                Console.WriteLine("1. Traducir una frase (Español a Inglés)");
                Console.WriteLine("2. Traducir una frase (Inglés a Español)");
                Console.WriteLine("3. Agregar palabras al diccionario");
                Console.WriteLine("4. Ver diccionario actual");
                Console.WriteLine("0. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            TraducirFrase("es");
                            break;
                        case 2:
                            TraducirFrase("en");
                            break;
                        case 3:
                            AgregarPalabra();
                            break;
                        case 4:
                            VerDiccionario();
                            break;
                        case 0:
                            Console.WriteLine("¡Hasta luego!");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido. Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            } while (opcion != 0);
        }

        static void TraducirFrase(string idiomaOrigen)
        {
            Console.Clear();
            Console.WriteLine(idiomaOrigen == "es" ? "=== TRADUCTOR ESPAÑOL -> INGLÉS ===" : "=== TRADUCTOR INGLÉS -> ESPAÑOL ===");
            Console.WriteLine();
            Console.Write("Ingrese la frase a traducir: ");
            
            string frase = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine("Frase vacía. Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Separar la frase en palabras, manteniendo signos de puntuación
            string[] palabras = frase.Split(' ');
            List<string> fraseTraducida = new List<string>();
            int palabrasTraducidas = 0;
            int palabrasNoEncontradas = 0;

            foreach (string palabra in palabras)
            {
                // Limpiar la palabra de signos de puntuación para buscar en el diccionario
                string palabraLimpia = palabra.Trim(new char[] { ',', '.', ';', ':', '!', '¡', '¿', '?', '"', '(' , ')' }).ToLower();
                
                string traduccion;
                bool encontrada = false;

                if (idiomaOrigen == "es")
                {
                    encontrada = diccionarioEspanolIngles.TryGetValue(palabraLimpia, out traduccion);
                }
                else
                {
                    encontrada = diccionarioInglesEspanol.TryGetValue(palabraLimpia, out traduccion);
                }

                if (encontrada)
                {
                    // Mantener mayúsculas si la palabra original empezaba con mayúscula
                    if (char.IsUpper(palabra.FirstOrDefault()))
                    {
                        traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);
                    }
                    
                    // Restaurar signos de puntuación
                    foreach (char c in palabra)
                    {
                        if (!char.IsLetterOrDigit(c))
                        {
                            traduccion += c;
                        }
                    }
                    
                    fraseTraducida.Add(traduccion);
                    palabrasTraducidas++;
                }
                else
                {
                    fraseTraducida.Add(palabra);
                    palabrasNoEncontradas++;
                }
            }

            Console.WriteLine();
            Console.WriteLine("FRASE TRADUCIDA:");
            Console.WriteLine(string.Join(" ", fraseTraducida));
            Console.WriteLine();
            Console.WriteLine($"Estadísticas: {palabrasTraducidas} palabras traducidas, {palabrasNoEncontradas} palabras no encontradas");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        static void AgregarPalabra()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR PALABRAS AL DICCIONARIO ===");
            Console.WriteLine();
            Console.WriteLine("Seleccione el tipo de traducción:");
            Console.WriteLine("1. Español a Inglés");
            Console.WriteLine("2. Inglés a Español");
            Console.Write("Opción: ");
            
            if (!int.TryParse(Console.ReadLine(), out int opcion) || (opcion != 1 && opcion != 2))
            {
                Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese la palabra a agregar: ");
            string palabraOrigen = Console.ReadLine().Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(palabraOrigen))
            {
                Console.WriteLine("Palabra inválida. Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese su traducción: ");
            string palabraDestino = Console.ReadLine().Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(palabraDestino))
            {
                Console.WriteLine("Traducción inválida. Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            if (opcion == 1) // Español a Inglés
            {
                if (!diccionarioEspanolIngles.ContainsKey(palabraOrigen))
                {
                    diccionarioEspanolIngles.Add(palabraOrigen, palabraDestino);
                    
                    // Actualizar también el diccionario inverso si es necesario
                    if (!diccionarioInglesEspanol.ContainsKey(palabraDestino))
                    {
                        diccionarioInglesEspanol.Add(palabraDestino, palabraOrigen);
                    }
                    
                    Console.WriteLine($"Palabra '{palabraOrigen}' agregada correctamente con traducción '{palabraDestino}'.");
                }
                else
                {
                    Console.WriteLine($"La palabra '{palabraOrigen}' ya existe en el diccionario con traducción '{diccionarioEspanolIngles[palabraOrigen]}'.");
                    Console.Write("¿Desea sobrescribirla? (s/n): ");
                    if (Console.ReadLine().Trim().ToLower() == "s")
                    {
                        diccionarioEspanolIngles[palabraOrigen] = palabraDestino;
                        Console.WriteLine("Palabra actualizada correctamente.");
                    }
                }
            }
            else // Inglés a Español
            {
                if (!diccionarioInglesEspanol.ContainsKey(palabraOrigen))
                {
                    diccionarioInglesEspanol.Add(palabraOrigen, palabraDestino);
                    
                    // Actualizar también el diccionario inverso
                    if (!diccionarioEspanolIngles.ContainsKey(palabraDestino))
                    {
                        diccionarioEspanolIngles.Add(palabraDestino, palabraOrigen);
                    }
                    
                    Console.WriteLine($"Palabra '{palabraOrigen}' agregada correctamente con traducción '{palabraDestino}'.");
                }
                else
                {
                    Console.WriteLine($"La palabra '{palabraOrigen}' ya existe en el diccionario con traducción '{diccionarioInglesEspanol[palabraOrigen]}'.");
                    Console.Write("¿Desea sobrescribirla? (s/n): ");
                    if (Console.ReadLine().Trim().ToLower() == "s")
                    {
                        diccionarioInglesEspanol[palabraOrigen] = palabraDestino;
                        Console.WriteLine("Palabra actualizada correctamente.");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        static void VerDiccionario()
        {
            Console.Clear();
            Console.WriteLine("=== DICCIONARIO ACTUAL ===");
            Console.WriteLine();
            Console.WriteLine("ESPAÑOL -> INGLÉS:");
            Console.WriteLine("-------------------");
            
            foreach (var palabra in diccionarioEspanolIngles.OrderBy(p => p.Key))
            {
                Console.WriteLine($"{palabra.Key.PadRight(15)} -> {palabra.Value}");
            }
            
            Console.WriteLine();
            Console.WriteLine("INGLÉS -> ESPAÑOL:");
            Console.WriteLine("-------------------");
            
            foreach (var palabra in diccionarioInglesEspanol.OrderBy(p => p.Key))
            {
                Console.WriteLine($"{palabra.Key.PadRight(15)} -> {palabra.Value}");
            }
            
            Console.WriteLine();
            Console.WriteLine($"Total de palabras en diccionario: {diccionarioEspanolIngles.Count}");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}