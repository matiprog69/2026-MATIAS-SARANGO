using System;
using System.Collections.Generic;
using System.Linq;

namespace AuditorioColas
{
    /// <summary>
    /// Clase que representa a un asistente al auditorio.
    /// </summary>
    public class Asistente
    {
        // Identificador único (puede ser consecutivo o generado).
        public int Id { get; }

        // Nombre (opcional, para reportería más amigable).
        public string Nombre { get; }

        // Número de llegada global: determina el orden REAL de arribo al auditorio.
        // Este número es estricto y creciente, y gobierna la prioridad de asignación.
        public int NumeroLlegadaGlobal { get; }

        // Cola de origen: "A" o "B" (registrador que lo atendió).
        public string ColaOrigen { get; }

        public Asistente(int id, string nombre, int numeroLlegadaGlobal, string colaOrigen)
        {
            Id = id;
            Nombre = nombre;
            NumeroLlegadaGlobal = numeroLlegadaGlobal;
            ColaOrigen = colaOrigen;
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} | Llegada #{NumeroLlegadaGlobal} | Cola {ColaOrigen}";
        }
    }

    /// <summary>
    /// Clase que representa a un registrador humano (persona) con su propia cola de atención.
    /// Internamente se utiliza Queue<Asistente> por su comportamiento FIFO (First-In, First-Out).
    /// </summary>
    public class Registrador
    {
        public string Nombre { get; }
        private readonly Queue<Asistente> _cola;

        public Registrador(string nombre)
        {
            Nombre = nombre;
            _cola = new Queue<Asistente>();
        }

        /// <summary>
        /// Encola a un nuevo asistente en la cola de este registrador.
        /// Complejidad O(1).
        /// </summary>
        public void Encolar(Asistente a)
        {
            _cola.Enqueue(a);
        }

        /// <summary>
        /// Consulta el primero en la cola sin desencolar. Devuelve null si está vacía.
        /// Complejidad O(1).
        /// </summary>
        public Asistente Frente() => _cola.Count > 0 ? _cola.Peek() : null;

        /// <summary>
        /// Desencola al primero (si lo hay). Complejidad O(1).
        /// </summary>
        public Asistente Desencolar() => _cola.Count > 0 ? _cola.Dequeue() : null;

        /// <summary>
        /// Número de personas en la cola.
        /// </summary>
        public int Conteo => _cola.Count;

        /// <summary>
        /// Enumeración de la cola para reportería (se preserva el orden FIFO).
        /// </summary>
        public IEnumerable<Asistente> VerCola() => _cola.ToArray();

        public override string ToString()
        {
            return $"{Nombre} (en cola: {Conteo})";
        }
    }

    /// <summary>
    /// Clase responsable de asignar asientos de manera secuencial, hasta un máximo de capacidad.
    /// Mantiene reportería y consultas O(1) con diccionarios auxiliares.
    /// </summary>
    public class AsignadorAsientos
    {
        private readonly int _capacidadTotal;
        private int _siguienteAsientoLibre;

        // Mapeos para consultas eficientes.
        private readonly Dictionary<int, int> _asientoPorAsistenteId; // asistenteId -> nro asiento
        private readonly Dictionary<int, Asistente> _asistentePorAsiento; // nro asiento -> asistente

        public AsignadorAsientos(int capacidadTotal)
        {
            _capacidadTotal = capacidadTotal;
            _siguienteAsientoLibre = 1;
            _asientoPorAsistenteId = new Dictionary<int, int>();
            _asistentePorAsiento = new Dictionary<int, Asistente>();
        }

        /// <summary>
        /// Indica si aún quedan asientos disponibles.
        /// </summary>
        public bool HayDisponibilidad => _siguienteAsientoLibre <= _capacidadTotal;

        /// <summary>
        /// Asigna el siguiente asiento al asistente recibido.
        /// Retorna el número de asiento asignado o -1 si ya no hay disponibilidad.
        /// Complejidad O(1).
        /// </summary>
        public int AsignarSiguienteAsiento(Asistente a)
        {
            if (!HayDisponibilidad) return -1;

            int asiento = _siguienteAsientoLibre;
            _siguienteAsientoLibre++;

            _asientoPorAsistenteId[a.Id] = asiento;
            _asistentePorAsiento[asiento] = a;

            return asiento;
        }

        /// <summary>
        /// Consulta asiento por Id de asistente en O(1). Retorna -1 si no tiene.
        /// </summary>
        public int ConsultarAsientoPorAsistenteId(int asistenteId)
        {
            return _asientoPorAsistenteId.TryGetValue(asistenteId, out int asiento) ? asiento : -1;
        }

        /// <summary>
        /// Consulta asistente por número de asiento en O(1). Retorna null si no existe.
        /// </summary>
        public Asistente ConsultarAsistentePorAsiento(int asiento)
        {
            return _asistentePorAsiento.TryGetValue(asiento, out var a) ? a : null;
        }

        /// <summary>
        /// Enumeración de todos los asientos asignados con su asistente (para reportería).
        /// </summary>
        public IEnumerable<(int Asiento, Asistente Persona)> VerAsignaciones()
        {
            // Ordenar por número de asiento para presentación limpia.
            foreach (var kv in _asistentePorAsiento.OrderBy(k => k.Key))
            {
                yield return (kv.Key, kv.Value);
            }
        }

        /// <summary>
        /// Cantidad de asientos ya asignados.
        /// </summary>
        public int Asignados => _asistentePorAsiento.Count;

        /// <summary>
        /// Capacidad total del auditorio.
        /// </summary>
        public int Capacidad => _capacidadTotal;
    }

    /// <summary>
    /// Sistema principal que modela:
    /// - Dos registradores (dos colas).
    /// - La generación de asistentes (con número de llegada global).
    /// - La asignación de asientos respetando el orden global de llegada.
    /// - Reportería para visualizar colas y asientos.
    /// </summary>
    public class SistemaAuditorio
    {
        private readonly Registrador _registradorA;
        private readonly Registrador _registradorB;
        private readonly AsignadorAsientos _asignador;

        // Contadores internos para generar IDs y números de llegada.
        private int _siguienteId;
        private int _siguienteLlegadaGlobal;

        // Random para generación de datos (opcional).
        private readonly Random _rand;

        public SistemaAuditorio(int capacidadAsientos = 100)
        {
            _registradorA = new Registrador("Registrador A");
            _registradorB = new Registrador("Registrador B");
            _asignador = new AsignadorAsientos(capacidadAsientos);
            _siguienteId = 1;
            _siguienteLlegadaGlobal = 1;
            _rand = new Random();
        }

        /// <summary>
        /// Genera N asistentes "llegando" al auditorio.
        /// Cada asistente se asigna a la cola A o B (al azar o alternando),
        /// pero todos tienen un número de llegada global único y creciente.
        /// Complejidad: O(N) en total (cada encolado es O(1)).
        /// </summary>
        public void GenerarAsistentes(int n, bool asignacionAleatoria = true)
        {
            for (int i = 0; i < n; i++)
            {
                int id = _siguienteId++;
                int llegada = _siguienteLlegadaGlobal++;
                string nombre = $"Asistente_{id}";

                // Decide a qué cola va este asistente: aleatorio o alternado.
                bool vaA = asignacionAleatoria ? _rand.Next(2) == 0 : (llegada % 2 == 1);

                var asistente = new Asistente(
                    id: id,
                    nombre: nombre,
                    numeroLlegadaGlobal: llegada,
                    colaOrigen: vaA ? "A" : "B"
                );

                if (vaA) _registradorA.Encolar(asistente);
                else _registradorB.Encolar(asistente);
            }
        }

        /// <summary>
        /// Procesa un único asiento (si hay disponibilidad), tomando SIEMPRE
        /// al asistente que llegó primero ENTRE los frentes de ambas colas.
        /// - Si una cola está vacía, se atiende la otra.
        /// - Si ambas están vacías, no hace nada.
        /// Retorna una tupla con información de la asignación o (0, null, "Ninguno") si no se asignó.
        /// Complejidad O(1).
        /// </summary>
        public (int Asiento, Asistente Persona, string Fuente) ProcesarSiguiente()
        {
            if (!_asignador.HayDisponibilidad)
                return (0, null, "Ninguno"); // No hay asientos

            var frenteA = _registradorA.Frente();
            var frenteB = _registradorB.Frente();

            if (frenteA == null && frenteB == null)
                return (0, null, "Ninguno"); // No hay personas en cola

            Asistente siguiente;
            string fuente;

            if (frenteA != null && frenteB != null)
            {
                // Elegimos por menor NumeroLlegadaGlobal para respetar orden global.
                if (frenteA.NumeroLlegadaGlobal <= frenteB.NumeroLlegadaGlobal)
                {
                    siguiente = _registradorA.Desencolar();
                    fuente = "A";
                }
                else
                {
                    siguiente = _registradorB.Desencolar();
                    fuente = "B";
                }
            }
            else if (frenteA != null)
            {
                siguiente = _registradorA.Desencolar();
                fuente = "A";
            }
            else
            {
                siguiente = _registradorB.Desencolar();
                fuente = "B";
            }

            int asiento = _asignador.AsignarSiguienteAsiento(siguiente);
            return (asiento, siguiente, fuente);
        }

        /// <summary>
        /// Procesa todo lo que se pueda hasta que se agoten asientos o colas.
        /// Retorna cuántas asignaciones se realizaron.
        /// Complejidad O(K), K = número de asignaciones efectivas (<= capacidad).
        /// </summary>
        public int ProcesarTodo()
        {
            int conteo = 0;
            while (_asignador.HayDisponibilidad &&
                   (_registradorA.Conteo > 0 || _registradorB.Conteo > 0))
            {
                var res = ProcesarSiguiente();
                if (res.Asiento <= 0) break;
                conteo++;
            }
            return conteo;
        }

        /// <summary>
        /// Reporte: estado de colas (A y B).
        /// </summary>
        public void ImprimirEstadoColas()
        {
            Console.WriteLine("=== ESTADO DE COLAS ===");
            Console.WriteLine(_registradorA.ToString());
            foreach (var a in _registradorA.VerCola())
                Console.WriteLine($"  - {a}");

            Console.WriteLine(_registradorB.ToString());
            foreach (var a in _registradorB.VerCola())
                Console.WriteLine($"  - {a}");
            Console.WriteLine();
        }

        /// <summary>
        /// Reporte: asientos asignados.
        /// </summary>
        public void ImprimirAsignaciones()
        {
            Console.WriteLine("=== ASIENTOS ASIGNADOS ===");
            foreach (var tupla in _asignador.VerAsignaciones())
            {
                Console.WriteLine($"Asiento {tupla.Asiento}: {tupla.Persona}");
            }
            Console.WriteLine($"Total asignados: {_asignador.Asignados}/{_asignador.Capacidad}\n");
        }

        /// <summary>
        /// Reporte: “mapa” simple de disponibilidad.
        /// Muestra un bloque por cada 10 asientos: [#####.....] (5 ocupados, 5 libres p.ej.).
        /// </summary>
        public void ImprimirMapaAsientos()
        {
            Console.WriteLine("=== MAPA DE ASIENTOS (bloques de 10) ===");
            int capacidad = _asignador.Capacidad;
            int asignados = _asignador.Asignados;

            for (int inicio = 1; inicio <= capacidad; inicio += 10)
            {
                int fin = Math.Min(inicio + 9, capacidad);
                int ocupadosEnBloque = Enumerable.Range(inicio, fin - inicio + 1)
                                                 .Count(n => _asignador.ConsultarAsistentePorAsiento(n) != null);
                int libresEnBloque = (fin - inicio + 1) - ocupadosEnBloque;

                string barra = new string('#', ocupadosEnBloque) + new string('.', libresEnBloque);
                Console.WriteLine($"Asientos {inicio:000}-{fin:000}: [{barra}]  (#{ocupadosEnBloque} / .{libresEnBloque})");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Consulta por Id de asistente. O(1) gracias a diccionario.
        /// </summary>
        public void ConsultarAsientoPorAsistenteId(int id)
        {
            int asiento = _asignador.ConsultarAsientoPorAsistenteId(id);
            if (asiento > 0)
                Console.WriteLine($"El asistente {id} tiene el asiento {asiento}.");
            else
                Console.WriteLine($"El asistente {id} aún no tiene asiento asignado.");
        }

        /// <summary>
        /// Consulta por número de asiento. O(1) gracias a diccionario.
        /// </summary>
        public void ConsultarAsistentePorAsiento(int asiento)
        {
            var a = _asignador.ConsultarAsistentePorAsiento(asiento);
            if (a != null)
                Console.WriteLine($"El asiento {asiento} pertenece a: {a}");
            else
                Console.WriteLine($"El asiento {asiento} está libre (o no existe).");
        }

        /// <summary>
        /// Reinicia completamente la simulación.
        /// </summary>
        public void Reiniciar()
        {
            // Simplemente mostramos un mensaje ya que en esta implementación
            // es más fácil crear una nueva instancia del sistema
            Console.WriteLine("Para reiniciar, cree una nueva instancia de SistemaAuditorio.");
        }
    }

    /// <summary>
    /// Programa principal (consola) que expone un menú para:
    /// - Generar asistentes.
    /// - Procesar 1 o todos los asientos posibles.
    /// - Ver reportería (colas, asignaciones, mapa).
    /// - Consultar por Id o por asiento.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Creamos el sistema con 100 asientos como indica el enunciado.
            var sistema = new SistemaAuditorio(capacidadAsientos: 100);

            while (true)
            {
                Console.WriteLine("==========================================");
                Console.WriteLine(" ASIGNACIÓN DE ASIENTOS - AUDITORIO (2 colas)");
                Console.WriteLine("==========================================");
                Console.WriteLine("1) Generar asistentes (aleatorio)");
                Console.WriteLine("2) Procesar siguiente asignación");
                Console.WriteLine("3) Procesar todo");
                Console.WriteLine("4) Ver estado de colas");
                Console.WriteLine("5) Ver asientos asignados");
                Console.WriteLine("6) Ver mapa de asientos");
                Console.WriteLine("7) Consultar asiento por Id de asistente");
                Console.WriteLine("8) Consultar asistente por número de asiento");
                Console.WriteLine("9) Salir");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("¿Cuántos asistentes generar? ");
                        if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
                        {
                            sistema.GenerarAsistentes(n);
                            Console.WriteLine($"Se generaron {n} asistentes y fueron encolados en A/B.\n");
                        }
                        else
                        {
                            Console.WriteLine("Número inválido.\n");
                        }
                        break;

                    case "2":
                        {
                            var res = sistema.ProcesarSiguiente();
                            if (res.Asiento > 0)
                            {
                                Console.WriteLine($"Asignado asiento {res.Asiento} a {res.Persona} (desde cola {res.Fuente}).\n");
                            }
                            else
                            {
                                Console.WriteLine("No fue posible asignar: sin asientos o sin personas en cola.\n");
                            }
                        }
                        break;

                    case "3":
                        {
                            int k = sistema.ProcesarTodo();
                            Console.WriteLine($"Se realizaron {k} asignaciones.\n");
                        }
                        break;

                    case "4":
                        sistema.ImprimirEstadoColas();
                        break;

                    case "5":
                        sistema.ImprimirAsignaciones();
                        break;

                    case "6":
                        sistema.ImprimirMapaAsientos();
                        break;

                    case "7":
                        Console.Write("Ingresa Id de asistente: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                            sistema.ConsultarAsientoPorAsistenteId(id);
                        else
                            Console.WriteLine("Id inválido.");
                        Console.WriteLine();
                        break;

                    case "8":
                        Console.Write("Ingresa número de asiento: ");
                        if (int.TryParse(Console.ReadLine(), out int asiento))
                            sistema.ConsultarAsistentePorAsiento(asiento);
                        else
                            Console.WriteLine("Asiento inválido.");
                        Console.WriteLine();
                        break;

                    case "9":
                        Console.WriteLine("¡Hasta luego!");
                        return;

                    default:
                        Console.WriteLine("Opción no válida.\n");
                        break;
                }
            }
        }
    }
}