using System;
using System.IO;

namespace AgendaTelefonica
{
    // ==================== ESTRUCTURA (struct) ====================
    public struct FechaRegistro
    {
        public int Dia;
        public int Mes;
        public int Anio;
        public string Hora;

        public FechaRegistro(int dia, int mes, int anio, string hora)
        {
            Dia = dia;
            Mes = mes;
            Anio = anio;
            Hora = hora;
        }

        public override string ToString()
        {
            return $"{Dia:00}/{Mes:00}/{Anio} {Hora}";
        }
    }

    // ==================== CLASE CONTACTO (registro) ====================
    public class Contacto
    {
        // Propiedades del contacto
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Categoria { get; set; }
        public FechaRegistro FechaCreacion { get; set; }

        // Constructor
        public Contacto(string nombre, string telefono, string email, string categoria)
        {
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
            Categoria = categoria;
            FechaCreacion = new FechaRegistro(
                DateTime.Now.Day,
                DateTime.Now.Month,
                DateTime.Now.Year,
                DateTime.Now.ToString("HH:mm")
            );
        }

        public override string ToString()
        {
            return $"👤 {Nombre,-20} | 📞 {Telefono,-12} | 🏷️ {Categoria,-10} | 📅 {FechaCreacion}";
        }

        public string ToCSV()
        {
            return $"{Nombre},{Telefono},{Email},{Categoria},{FechaCreacion.Dia},{FechaCreacion.Mes},{FechaCreacion.Anio},{FechaCreacion.Hora}";
        }
    }

    // ==================== CLASE AGENDA TELEFÓNICA ====================
    public class AgendaTelefonica
    {
        // VECTOR para almacenar contactos (Array unidimensional)
        private Contacto[] vectorContactos;
        private int cantidadContactos;
        private const int CAPACIDAD_INICIAL = 50;

        // MATRIZ para estadísticas de categorías (Array bidimensional)
        // Fila 0: Familiar, Fila 1: Trabajo, Fila 2: Amigo, Fila 3: Otro
        // Columna 0: Nombre categoría, Columna 1: Cantidad, Columna 2: Porcentaje
        private string[,] matrizEstadisticas;

        // Archivo para persistencia
        private readonly string archivoContactos = "agenda_telefonica.txt";

        public AgendaTelefonica()
        {
            // Inicializar VECTOR de contactos
            vectorContactos = new Contacto[CAPACIDAD_INICIAL];
            cantidadContactos = 0;

            // Inicializar MATRIZ de estadísticas (4x3)
            matrizEstadisticas = new string[4, 3];
            InicializarMatrizEstadisticas();

            Console.WriteLine("📱 AGENDA TELEFÓNICA INICIALIZADA");
            Console.WriteLine($"📁 Archivo: {Path.GetFullPath(archivoContactos)}");
            Console.WriteLine($"🗂️  Capacidad inicial: {CAPACIDAD_INICIAL} contactos\n");

            CargarContactosDesdeArchivo();
        }

        // ==================== MÉTODOS DEL VECTOR ====================

        // Agregar contacto al VECTOR
        public void AgregarContacto(Contacto nuevoContacto)
        {
            if (cantidadContactos >= vectorContactos.Length)
            {
                RedimensionarVector();
            }

            vectorContactos[cantidadContactos] = nuevoContacto;
            cantidadContactos++;

            Console.WriteLine($"✅ CONTACTO AGREGADO:");
            Console.WriteLine($"   👤 {nuevoContacto.Nombre}");
            Console.WriteLine($"   📞 {nuevoContacto.Telefono}");
            Console.WriteLine($"   🏷️  {nuevoContacto.Categoria}");
            Console.WriteLine($"   📅 {nuevoContacto.FechaCreacion}");

            GuardarContactosEnArchivo();
            ActualizarEstadisticas();
        }

        // Redimensionar VECTOR cuando se llena
        private void RedimensionarVector()
        {
            int nuevaCapacidad = vectorContactos.Length * 2;
            Contacto[] nuevoVector = new Contacto[nuevaCapacidad];

            for (int i = 0; i < cantidadContactos; i++)
            {
                nuevoVector[i] = vectorContactos[i];
            }

            vectorContactos = nuevoVector;
            Console.WriteLine($"⚠️  Vector redimensionado: {nuevaCapacidad} contactos");
        }

        // Buscar contacto por nombre en el VECTOR
        public void BuscarContactoPorNombre(string nombre)
        {
            Console.WriteLine($"\n🔍 BUSCANDO: '{nombre}'");
            Console.WriteLine(new string('─', 80));

            bool encontrado = false;
            for (int i = 0; i < cantidadContactos; i++)
            {
                if (vectorContactos[i].Nombre.ToLower().Contains(nombre.ToLower()))
                {
                    Console.WriteLine($"✅ ENCONTRADO:");
                    Console.WriteLine($"   {vectorContactos[i]}");
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("❌ Contacto no encontrado.");
            }
        }

        // Buscar contactos por teléfono en el VECTOR
        public void BuscarContactoPorTelefono(string telefono)
        {
            Console.WriteLine($"\n🔍 BUSCANDO TELÉFONO: '{telefono}'");
            Console.WriteLine(new string('─', 80));

            bool encontrado = false;
            for (int i = 0; i < cantidadContactos; i++)
            {
                if (vectorContactos[i].Telefono.Contains(telefono))
                {
                    Console.WriteLine($"✅ ENCONTRADO:");
                    Console.WriteLine($"   👤 {vectorContactos[i].Nombre}");
                    Console.WriteLine($"   📞 {vectorContactos[i].Telefono}");
                    Console.WriteLine($"   🏷️  {vectorContactos[i].Categoria}");
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("❌ Teléfono no encontrado.");
            }
        }

        // Listar TODOS los contactos del VECTOR
        public void ListarTodosContactos()
        {
            Console.WriteLine("\n" + new string('═', 80));
            Console.WriteLine("📞 LISTA COMPLETA DE CONTACTOS TELEFÓNICOS");
            Console.WriteLine(new string('═', 80));

            if (cantidadContactos == 0)
            {
                Console.WriteLine("📭 No hay contactos en la agenda.");
                return;
            }

            for (int i = 0; i < cantidadContactos; i++)
            {
                Console.WriteLine($"{i + 1:00}. {vectorContactos[i]}");
            }

            Console.WriteLine($"\n📊 TOTAL: {cantidadContactos} contactos");
        }

        // Listar contactos por categoría del VECTOR
        public void ListarContactosPorCategoria(string categoria)
        {
            Console.WriteLine($"\n🏷️  CONTACTOS EN CATEGORÍA: {categoria.ToUpper()}");
            Console.WriteLine(new string('─', 80));

            int contador = 0;
            for (int i = 0; i < cantidadContactos; i++)
            {
                if (vectorContactos[i].Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{contador + 1}. {vectorContactos[i]}");
                    contador++;
                }
            }

            if (contador == 0)
            {
                Console.WriteLine("📭 No hay contactos en esta categoría.");
            }
            else
            {
                Console.WriteLine($"\n📊 Encontrados: {contador} contactos");
            }
        }

        // Eliminar contacto del VECTOR
        public bool EliminarContacto(string nombre)
        {
            int indice = -1;

            // Buscar el índice en el VECTOR
            for (int i = 0; i < cantidadContactos; i++)
            {
                if (vectorContactos[i].Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    indice = i;
                    break;
                }
            }

            if (indice != -1)
            {
                Console.WriteLine($"\n🗑️  ELIMINANDO CONTACTO: '{vectorContactos[indice].Nombre}'");

                // Desplazar elementos en el VECTOR
                for (int i = indice; i < cantidadContactos - 1; i++)
                {
                    vectorContactos[i] = vectorContactos[i + 1];
                }

                cantidadContactos--;
                vectorContactos[cantidadContactos] = null;

                Console.WriteLine("✅ Contacto eliminado exitosamente.");
                GuardarContactosEnArchivo();
                ActualizarEstadisticas();
                return true;
            }

            Console.WriteLine($"\n❌ No se encontró el contacto '{nombre}'.");
            return false;
        }

        // ==================== MÉTODOS DE LA MATRIZ ====================

        // Inicializar MATRIZ de estadísticas
        private void InicializarMatrizEstadisticas()
        {
            string[] categorias = { "FAMILIAR", "TRABAJO", "AMIGO", "OTRO" };

            for (int fila = 0; fila < 4; fila++)
            {
                matrizEstadisticas[fila, 0] = categorias[fila];  // Nombre categoría
                matrizEstadisticas[fila, 1] = "0";               // Cantidad
                matrizEstadisticas[fila, 2] = "0%";              // Porcentaje
            }
        }

        // Actualizar MATRIZ de estadísticas
        private void ActualizarEstadisticas()
        {
            // Contar contactos por categoría
            int[] contadores = new int[4]; // 0:Familiar, 1:Trabajo, 2:Amigo, 3:Otro

            for (int i = 0; i < cantidadContactos; i++)
            {
                string categoria = vectorContactos[i].Categoria.ToUpper();
                
                if (categoria == "FAMILIAR") contadores[0]++;
                else if (categoria == "TRABAJO") contadores[1]++;
                else if (categoria == "AMIGO") contadores[2]++;
                else contadores[3]++;
            }

            // Actualizar MATRIZ
            for (int fila = 0; fila < 4; fila++)
            {
                matrizEstadisticas[fila, 1] = contadores[fila].ToString();
                
                if (cantidadContactos > 0)
                {
                    double porcentaje = (contadores[fila] * 100.0) / cantidadContactos;
                    matrizEstadisticas[fila, 2] = $"{porcentaje:F1}%";
                }
                else
                {
                    matrizEstadisticas[fila, 2] = "0%";
                }
            }
        }

        // Mostrar MATRIZ de estadísticas
        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine("📈 ESTADÍSTICAS DE CONTACTOS TELEFÓNICOS");
            Console.WriteLine(new string('═', 50));

            Console.WriteLine("\n┌────────────┬──────────┬────────────┐");
            Console.WriteLine("│ CATEGORÍA  │ CANTIDAD │ PORCENTAJE │");
            Console.WriteLine("├────────────┼──────────┼────────────┤");

            for (int fila = 0; fila < 4; fila++)
            {
                Console.WriteLine($"│ {matrizEstadisticas[fila, 0],-10} │ {matrizEstadisticas[fila, 1],8} │ {matrizEstadisticas[fila, 2],10} │");
            }

            Console.WriteLine("└────────────┴──────────┴────────────┘");
            Console.WriteLine($"\n📊 TOTAL CONTACTOS: {cantidadContactos}");
        }

        // ==================== PERSISTENCIA EN ARCHIVO ====================

        private void GuardarContactosEnArchivo()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(archivoContactos))
                {
                    for (int i = 0; i < cantidadContactos; i++)
                    {
                        sw.WriteLine(vectorContactos[i].ToCSV());
                    }
                }
                Console.WriteLine($"💾 Contactos guardados en archivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar: {ex.Message}");
            }
        }

        private void CargarContactosDesdeArchivo()
        {
            if (File.Exists(archivoContactos))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(archivoContactos);
                    int cargados = 0;

                    foreach (string linea in lineas)
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length >= 4)
                            {
                                Contacto contacto = new Contacto(
                                    datos[0],  // Nombre
                                    datos[1],  // Teléfono
                                    datos[2],  // Email
                                    datos[3]   // Categoría
                                );

                                if (cantidadContactos < vectorContactos.Length)
                                {
                                    vectorContactos[cantidadContactos] = contacto;
                                    cantidadContactos++;
                                    cargados++;
                                }
                            }
                        }
                    }

                    if (cargados > 0)
                    {
                        Console.WriteLine($"✅ {cargados} contactos telefónicos cargados.");
                        ActualizarEstadisticas();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️  Error al cargar: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("📄 Archivo no encontrado. Se creará uno nuevo.");
            }
        }

        // ==================== MÉTODOS DE UTILIDAD ====================

        public int ObtenerTotalContactos()
        {
            return cantidadContactos;
        }

        public int ObtenerCapacidadVector()
        {
            return vectorContactos.Length;
        }

        public void MostrarInfoSistema()
        {
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine("ℹ️  INFORMACIÓN DEL SISTEMA TELEFÓNICO");
            Console.WriteLine(new string('═', 50));
            Console.WriteLine($"📱 Contactos almacenados: {cantidadContactos}");
            Console.WriteLine($"🗂️  Capacidad del vector: {vectorContactos.Length}");
            Console.WriteLine($"📊 Uso de memoria: {(cantidadContactos * 100.0 / vectorContactos.Length):F1}%");
            Console.WriteLine($"📁 Archivo: {Path.GetFullPath(archivoContactos)}");
        }
    }

    // ==================== PROGRAMA PRINCIPAL ====================
    class Program
    {
        static void MostrarMenu()
        {
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("📱 SISTEMA DE AGENDA TELEFÓNICA");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine("1. 📞 Agregar nuevo contacto telefónico");
            Console.WriteLine("2. 🔍 Buscar contacto por nombre");
            Console.WriteLine("3. 📞 Buscar contacto por teléfono");
            Console.WriteLine("4. 📋 Listar todos los contactos");
            Console.WriteLine("5. 🏷️  Listar contactos por categoría");
            Console.WriteLine("6. 🗑️  Eliminar contacto");
            Console.WriteLine("7. 📈 Ver estadísticas");
            Console.WriteLine("8. ℹ️  Información del sistema");
            Console.WriteLine("9. 🚪 Salir");
            Console.WriteLine(new string('═', 60));
            Console.Write("👉 Seleccione una opción (1-9): ");
        }

        static void Pausa()
        {
            Console.Write("\n⏎ Presione ENTER para continuar...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("\n" + new string('★', 60));
            Console.WriteLine("🌟 BIENVENIDO AL SISTEMA DE AGENDA TELEFÓNICA 🌟");
            Console.WriteLine(new string('★', 60));

            AgendaTelefonica agenda = new AgendaTelefonica();
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": // Agregar contacto telefónico
                        Console.WriteLine("\n" + new string('─', 50));
                        Console.WriteLine("📞 AGREGAR NUEVO CONTACTO TELEFÓNICO");
                        Console.WriteLine(new string('─', 50));
                        
                        Console.Write("👤 Nombre completo: ");
                        string nombre = Console.ReadLine();
                        
                        Console.Write("📞 Número de teléfono: ");
                        string telefono = Console.ReadLine();
                        
                        Console.Write("📧 Email (opcional): ");
                        string email = Console.ReadLine();
                        
                        Console.Write("🏷️  Categoría (Familiar/Trabajo/Amigo/Otro): ");
                        string categoria = Console.ReadLine();
                        
                        if (string.IsNullOrWhiteSpace(telefono))
                        {
                            Console.WriteLine("\n❌ ERROR: El número de teléfono es obligatorio.");
                        }
                        else
                        {
                            agenda.AgregarContacto(new Contacto(nombre, telefono, email, categoria));
                        }
                        Pausa();
                        break;

                    case "2": // Buscar por nombre
                        Console.WriteLine("\n" + new string('─', 50));
                        Console.WriteLine("🔍 BUSCAR CONTACTO POR NOMBRE");
                        Console.WriteLine(new string('─', 50));
                        
                        Console.Write("Ingrese nombre a buscar: ");
                        agenda.BuscarContactoPorNombre(Console.ReadLine());
                        Pausa();
                        break;

                    case "3": // Buscar por teléfono
                        Console.WriteLine("\n" + new string('─', 50));
                        Console.WriteLine("📞 BUSCAR CONTACTO POR TELÉFONO");
                        Console.WriteLine(new string('─', 50));
                        
                        Console.Write("Ingrese teléfono a buscar: ");
                        agenda.BuscarContactoPorTelefono(Console.ReadLine());
                        Pausa();
                        break;

                    case "4": // Listar todos
                        agenda.ListarTodosContactos();
                        Pausa();
                        break;

                    case "5": // Listar por categoría
                        Console.WriteLine("\n" + new string('─', 50));
                        Console.WriteLine("🏷️  LISTAR POR CATEGORÍA");
                        Console.WriteLine(new string('─', 50));
                        
                        Console.Write("Categoría (Familiar/Trabajo/Amigo/Otro): ");
                        agenda.ListarContactosPorCategoria(Console.ReadLine());
                        Pausa();
                        break;

                    case "6": // Eliminar contacto
                        Console.WriteLine("\n" + new string('─', 50));
                        Console.WriteLine("🗑️  ELIMINAR CONTACTO");
                        Console.WriteLine(new string('─', 50));
                        
                        Console.Write("Nombre del contacto a eliminar: ");
                        agenda.EliminarContacto(Console.ReadLine());
                        Pausa();
                        break;

                    case "7": // Estadísticas
                        agenda.MostrarEstadisticas();
                        Pausa();
                        break;

                    case "8": // Información del sistema
                        agenda.MostrarInfoSistema();
                        Pausa();
                        break;

                    case "9": // Salir
                        salir = true;
                        Console.WriteLine("\n" + new string('★', 60));
                        Console.WriteLine("📱 ¡Gracias por usar la Agenda Telefónica!");
                        Console.WriteLine("💾 Todos los contactos han sido guardados.");
                        Console.WriteLine("👋 ¡Hasta pronto!");
                        Console.WriteLine(new string('★', 60));
                        break;

                    default:
                        Console.WriteLine("\n❌ Opción no válida. Intente de nuevo.");
                        Pausa();
                        break;
                }
            }
        }
    }
}