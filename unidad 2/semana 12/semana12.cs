using System;
using System.Collections.Generic;
using System.Linq;

namespace TorneoFutbol
{
    // Clase que representa un Jugador
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Posicion { get; set; } // ARQ, DEF, MED, DEL
        public int NumeroCamiseta { get; set; }

        // Constructor manual
        public Jugador(string nombre, int edad, string posicion, int numeroCamiseta)
        {
            Nombre = nombre;
            Edad = edad;
            Posicion = posicion;
            NumeroCamiseta = numeroCamiseta;
        }

        // Método para mostrar información del jugador
        public string ObtenerInfo()
        {
            return $"#{NumeroCamiseta:D2} | {Nombre,-20} | {Posicion} | {Edad} años";
        }
    }

    // Clase que representa un Equipo
    public class Equipo
    {
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Estadio { get; set; }
        public string Entrenador { get; set; }
        
        // Lista de jugadores con control manual de duplicados
        private List<Jugador> jugadores;

        public Equipo(string nombre, string ciudad, string estadio, string entrenador)
        {
            Nombre = nombre;
            Ciudad = ciudad;
            Estadio = estadio;
            Entrenador = entrenador;
            jugadores = new List<Jugador>();
        }

        public int TotalJugadores
        {
            get { return jugadores.Count; }
        }

        // Obtener copia de la lista de jugadores (para evitar modificaciones externas)
        public List<Jugador> ObtenerJugadores()
        {
            return new List<Jugador>(jugadores);
        }

        // Método para agregar jugador con control manual de duplicados
        public bool AgregarJugador(Jugador nuevoJugador)
        {
            // Verificar si el número de camiseta ya existe en el equipo
            foreach (Jugador j in jugadores)
            {
                if (j.NumeroCamiseta == nuevoJugador.NumeroCamiseta)
                {
                    return false; // Número duplicado
                }
            }
            
            jugadores.Add(nuevoJugador);
            return true;
        }

        // Método para eliminar jugador por número
        public bool EliminarJugador(int numeroCamiseta)
        {
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].NumeroCamiseta == numeroCamiseta)
                {
                    jugadores.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        // Método para buscar jugador por número
        public Jugador BuscarJugadorPorNumero(int numeroCamiseta)
        {
            foreach (Jugador j in jugadores)
            {
                if (j.NumeroCamiseta == numeroCamiseta)
                {
                    return j;
                }
            }
            return null;
        }

        // Método para obtener información del equipo
        public string ObtenerInfo()
        {
            return $"{Nombre} - {Ciudad} (Estadio: {Estadio}, DT: {Entrenador}) - {TotalJugadores} jugadores";
        }
    }

    // Clase principal del Torneo
    public class TorneoFutbol
    {
        // Dictionary (Mapa): clave = nombre del equipo, valor = objeto Equipo
        private Dictionary<string, Equipo> equipos;
        public string NombreTorneo { get; set; }

        // Constructor manual
        public TorneoFutbol(string nombreTorneo)
        {
            NombreTorneo = nombreTorneo;
            equipos = new Dictionary<string, Equipo>();
        }

        // 1. Registrar equipo (implementación manual)
        public string RegistrarEquipo(string nombre, string ciudad, string estadio, string entrenador)
        {
            // Validación manual de equipo existente
            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                if (kvp.Key.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return $"❌ El equipo {nombre} ya está registrado";
                }
            }

            Equipo nuevoEquipo = new Equipo(nombre, ciudad, estadio, entrenador);
            equipos.Add(nombre, nuevoEquipo);
            return $"✅ Equipo {nombre} registrado exitosamente";
        }

        // 2. Registrar jugador (implementación manual)
        public string RegistrarJugador(string nombreEquipo, string nombreJugador, int edad, string posicion, int numero)
        {
            // Validar posición manualmente
            string pos = posicion.ToUpper();
            if (pos != "ARQ" && pos != "DEF" && pos != "MED" && pos != "DEL")
            {
                return "❌ Posición inválida. Use: ARQ, DEF, MED, DEL";
            }

            // Buscar equipo manualmente
            Equipo equipoEncontrado = null;
            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                if (kvp.Key.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase))
                {
                    equipoEncontrado = kvp.Value;
                    break;
                }
            }

            if (equipoEncontrado == null)
            {
                return $"❌ El equipo {nombreEquipo} no existe";
            }

            // Crear jugador
            Jugador nuevoJugador = new Jugador(nombreJugador, edad, pos, numero);

            // Intentar agregar (control de duplicados dentro del método del equipo)
            if (equipoEncontrado.AgregarJugador(nuevoJugador))
            {
                return $"✅ {nombreJugador} registrado en {nombreEquipo} con #{numero}";
            }
            else
            {
                return $"❌ El número {numero} ya está asignado en {nombreEquipo}";
            }
        }

        // 3. Listar todos los equipos (implementación manual)
        public string ListarEquipos()
        {
            if (equipos.Count == 0)
            {
                return "📋 No hay equipos registrados";
            }

            string resultado = $"\n{'='.PadRight(60, '=')}\n";
            resultado += $"🏆 TORNEO: {NombreTorneo}\n";
            resultado += $"{'='.PadRight(60, '=')}\n";

            int i = 1;
            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                resultado += $"\n{i++}. {kvp.Value.ObtenerInfo()}";
            }

            return resultado;
        }

        // 4. Ver jugadores de un equipo específico (implementación manual)
        public string VerJugadores(string nombreEquipo)
        {
            // Buscar equipo manualmente
            Equipo equipoEncontrado = null;
            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                if (kvp.Key.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase))
                {
                    equipoEncontrado = kvp.Value;
                    break;
                }
            }

            if (equipoEncontrado == null)
            {
                return $"❌ El equipo {nombreEquipo} no existe";
            }

            List<Jugador> jugadores = equipoEncontrado.ObtenerJugadores();
            
            if (jugadores.Count == 0)
            {
                return $"📋 {nombreEquipo} no tiene jugadores registrados";
            }

            // Ordenar jugadores manualmente (algoritmo de burbuja)
            for (int i = 0; i < jugadores.Count - 1; i++)
            {
                for (int j = 0; j < jugadores.Count - i - 1; j++)
                {
                    if (jugadores[j].NumeroCamiseta > jugadores[j + 1].NumeroCamiseta)
                    {
                        // Intercambiar
                        Jugador temp = jugadores[j];
                        jugadores[j] = jugadores[j + 1];
                        jugadores[j + 1] = temp;
                    }
                }
            }

            string resultado = $"\n{'='.PadRight(50, '=')}\n";
            resultado += $"👥 JUGADORES DE {nombreEquipo.ToUpper()}\n";
            resultado += $"{'='.PadRight(50, '=')}\n";
            resultado += " #  | NOMBRE                 | POS | EDAD\n";
            resultado += $"{'-'.PadRight(50, '-')}\n";

            foreach (Jugador jugador in jugadores)
            {
                resultado += $"{jugador.ObtenerInfo()}\n";
            }

            return resultado;
        }

        // 5. Buscar jugador por nombre (implementación manual)
        public string BuscarJugadorPorNombre(string termino)
        {
            List<string> resultados = new List<string>();

            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                List<Jugador> jugadores = kvp.Value.ObtenerJugadores();
                
                foreach (Jugador jugador in jugadores)
                {
                    if (jugador.Nombre.IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        resultados.Add($"{jugador.ObtenerInfo()} | {kvp.Key}");
                    }
                }
            }

            if (resultados.Count == 0)
            {
                return $"🔍 No se encontraron jugadores con nombre '{termino}'";
            }

            string resultado = $"\n{'='.PadRight(60, '=')}\n";
            resultado += $"🔍 RESULTADOS DE BÚSQUEDA: {termino}\n";
            resultado += $"{'='.PadRight(60, '=')}\n";

            foreach (string r in resultados)
            {
                resultado += r + "\n";
            }

            return resultado;
        }

        // 6. Buscar jugador por posición (implementación manual)
        public string BuscarJugadorPorPosicion(string posicion)
        {
            string pos = posicion.ToUpper();
            List<string> resultados = new List<string>();

            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                List<Jugador> jugadores = kvp.Value.ObtenerJugadores();
                
                foreach (Jugador jugador in jugadores)
                {
                    if (jugador.Posicion == pos)
                    {
                        resultados.Add($"{jugador.ObtenerInfo()} | {kvp.Key}");
                    }
                }
            }

            if (resultados.Count == 0)
            {
                return $"🔍 No se encontraron jugadores en posición '{posicion}'";
            }

            string resultado = $"\n{'='.PadRight(60, '=')}\n";
            resultado += $"🔍 JUGADORES EN POSICIÓN: {posicion}\n";
            resultado += $"{'='.PadRight(60, '=')}\n";

            foreach (string r in resultados)
            {
                resultado += r + "\n";
            }

            return resultado;
        }

        // 7. Eliminar jugador (implementación manual)
        public string EliminarJugador(string nombreEquipo, int numero)
        {
            // Buscar equipo manualmente
            Equipo equipoEncontrado = null;
            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                if (kvp.Key.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase))
                {
                    equipoEncontrado = kvp.Value;
                    break;
                }
            }

            if (equipoEncontrado == null)
            {
                return $"❌ El equipo {nombreEquipo} no existe";
            }

            if (equipoEncontrado.EliminarJugador(numero))
            {
                return $"✅ Jugador #{numero} eliminado de {nombreEquipo}";
            }
            else
            {
                return $"❌ No existe jugador con #{numero} en {nombreEquipo}";
            }
        }

        // 8. Buscar equipos por ciudad (implementación manual)
        public string BuscarEquiposPorCiudad(string ciudad)
        {
            List<string> equiposEncontrados = new List<string>();

            foreach (KeyValuePair<string, Equipo> kvp in equipos)
            {
                if (kvp.Value.Ciudad.Equals(ciudad, StringComparison.OrdinalIgnoreCase))
                {
                    equiposEncontrados.Add(kvp.Key);
                }
            }

            if (equiposEncontrados.Count == 0)
            {
                return $"📍 No hay equipos en {ciudad}";
            }

            string resultado = $"\n📍 EQUIPOS EN {ciudad.ToUpper()}\n";
            resultado += $"{'-'.PadRight(30, '-')}\n";

            foreach (string equipo in equiposEncontrados)
            {
                resultado += $"• {equipo}\n";
            }

            return resultado;
        }

        // Método para inicializar datos de prueba (implementación manual)
        public void InicializarDatosPrueba()
        {
            RegistrarEquipo("Barcelona", "Guayaquil", "Monumental", "Segundo Castillo");
            RegistrarEquipo("Liga de Quito", "Quito", "Rodrigo Paz", "Pablo Sánchez");
            RegistrarEquipo("Emelec", "Guayaquil", "Capwell", "Leonardo Álvarez");

            RegistrarJugador("Barcelona", "Damián Díaz", 35, "MED", 10);
            RegistrarJugador("Barcelona", "Fidel Martínez", 34, "DEL", 11);
            RegistrarJugador("Barcelona", "Javier Burrai", 33, "ARQ", 1);
            RegistrarJugador("Barcelona", "Lucas Sosa", 26, "DEF", 4);

            RegistrarJugador("Liga de Quito", "Alexander Domínguez", 37, "ARQ", 1);
            RegistrarJugador("Liga de Quito", "Lisandro Alzugaray", 34, "MED", 10);
            RegistrarJugador("Liga de Quito", "Alex Arce", 29, "DEL", 9);

            RegistrarJugador("Emelec", "Miller Bolaños", 34, "MED", 10);
            RegistrarJugador("Emelec", "Pedro Ortiz", 34, "ARQ", 1);
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=".PadRight(60, '='));
            Console.WriteLine("🏆 SISTEMA DE REGISTRO PARA TORNEO DE FÚTBOL 🏆");
            Console.WriteLine("=".PadRight(60, '='));

            Console.Write("\nIngrese el nombre del torneo: ");
            string nombreTorneo = Console.ReadLine();

            TorneoFutbol torneo = new TorneoFutbol(nombreTorneo);

            Console.Write("\n¿Desea cargar datos de prueba? (s/n): ");
            string respuesta = Console.ReadLine();
            if (respuesta.ToLower() == "s")
            {
                torneo.InicializarDatosPrueba();
                Console.WriteLine("✅ Datos de prueba cargados exitosamente");
            }

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n" + "-".PadRight(50, '-'));
                Console.WriteLine("MENÚ PRINCIPAL");
                Console.WriteLine("-".PadRight(50, '-'));
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Registrar jugador");
                Console.WriteLine("3. Listar todos los equipos");
                Console.WriteLine("4. Ver jugadores de un equipo");
                Console.WriteLine("5. Buscar jugador por nombre");
                Console.WriteLine("6. Buscar jugador por posición");
                Console.WriteLine("7. Eliminar jugador");
                Console.WriteLine("8. Buscar equipos por ciudad");
                Console.WriteLine("9. Salir");
                Console.WriteLine("-".PadRight(50, '-'));

                Console.Write("Seleccione una opción (1-9): ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarEquipoMenu(torneo);
                        break;
                    case "2":
                        RegistrarJugadorMenu(torneo);
                        break;
                    case "3":
                        Console.WriteLine(torneo.ListarEquipos());
                        Pausa();
                        break;
                    case "4":
                        VerJugadoresMenu(torneo);
                        break;
                    case "5":
                        BuscarJugadorNombreMenu(torneo);
                        break;
                    case "6":
                        BuscarJugadorPosicionMenu(torneo);
                        break;
                    case "7":
                        EliminarJugadorMenu(torneo);
                        break;
                    case "8":
                        BuscarEquiposCiudadMenu(torneo);
                        break;
                    case "9":
                        salir = true;
                        Console.WriteLine("\n👋 ¡Gracias por usar el sistema!");
                        break;
                    default:
                        Console.WriteLine("❌ Opción no válida");
                        Pausa();
                        break;
                }
            }
        }

        static void RegistrarEquipoMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n📝 REGISTRO DE EQUIPO");
            Console.Write("Nombre del equipo: ");
            string nombre = Console.ReadLine();
            Console.Write("Ciudad: ");
            string ciudad = Console.ReadLine();
            Console.Write("Estadio: ");
            string estadio = Console.ReadLine();
            Console.Write("Entrenador: ");
            string entrenador = Console.ReadLine();

            Console.WriteLine(torneo.RegistrarEquipo(nombre, ciudad, estadio, entrenador));
            Pausa();
        }

        static void RegistrarJugadorMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n📝 REGISTRO DE JUGADOR");
            Console.Write("Nombre del equipo: ");
            string equipo = Console.ReadLine();
            Console.Write("Nombre del jugador: ");
            string nombre = Console.ReadLine();
            Console.Write("Edad: ");
            
            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("❌ Edad inválida");
                Pausa();
                return;
            }
            
            Console.WriteLine("Posiciones: ARQ (Arquero), DEF (Defensa), MED (Mediocampista), DEL (Delantero)");
            Console.Write("Posición: ");
            string posicion = Console.ReadLine();
            Console.Write("Número de camiseta: ");
            
            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("❌ Número inválido");
                Pausa();
                return;
            }

            Console.WriteLine(torneo.RegistrarJugador(equipo, nombre, edad, posicion, numero));
            Pausa();
        }

        static void VerJugadoresMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n👥 CONSULTAR JUGADORES");
            Console.Write("Nombre del equipo: ");
            string equipo = Console.ReadLine();
            Console.WriteLine(torneo.VerJugadores(equipo));
            Pausa();
        }

        static void BuscarJugadorNombreMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n🔍 BUSCAR JUGADOR POR NOMBRE");
            Console.Write("Ingrese nombre o parte del nombre: ");
            string termino = Console.ReadLine();
            Console.WriteLine(torneo.BuscarJugadorPorNombre(termino));
            Pausa();
        }

        static void BuscarJugadorPosicionMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n🔍 BUSCAR JUGADOR POR POSICIÓN");
            Console.WriteLine("Posiciones: ARQ, DEF, MED, DEL");
            Console.Write("Ingrese posición: ");
            string posicion = Console.ReadLine();
            Console.WriteLine(torneo.BuscarJugadorPorPosicion(posicion));
            Pausa();
        }

        static void EliminarJugadorMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n🗑️ ELIMINAR JUGADOR");
            Console.Write("Nombre del equipo: ");
            string equipo = Console.ReadLine();
            Console.Write("Número de camiseta del jugador a eliminar: ");
            
            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("❌ Número inválido");
                Pausa();
                return;
            }
            
            Console.WriteLine(torneo.EliminarJugador(equipo, numero));
            Pausa();
        }

        static void BuscarEquiposCiudadMenu(TorneoFutbol torneo)
        {
            Console.WriteLine("\n📍 BUSCAR EQUIPOS POR CIUDAD");
            Console.Write("Ingrese nombre de la ciudad: ");
            string ciudad = Console.ReadLine();
            Console.WriteLine(torneo.BuscarEquiposPorCiudad(ciudad));
            Pausa();
        }

        static void Pausa()
        {
            Console.Write("\nPresione Enter para continuar...");
            Console.ReadLine();
        }
    }
}