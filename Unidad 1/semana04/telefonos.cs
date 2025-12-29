using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AgendaTelefonica
{
    // ==================== CLASE CONTACTO ====================
    public class Contacto
    {
        // Propiedades con inicialización para evitar warnings
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;

        // Constructor
        public Contacto(string nombre, string telefono, string email, string categoria)
        {
            Nombre = nombre ?? string.Empty;
            Telefono = telefono ?? string.Empty;
            Email = email ?? string.Empty;
            Categoria = categoria ?? string.Empty;
        }

        // Método para mostrar información
        public override string ToString()
        {
            return $"Nombre: {Nombre} | Tel: {Telefono} | Email: {Email} | Categoría: {Categoria}";
        }

        // Método para formato CSV
        public string ToCSV()
        {
            return $"{Nombre},{Telefono},{Email},{Categoria}";
        }
    }

    // ==================== CLASE AGENDA ====================
    public class Agenda
    {
        private List<Contacto> contactos;
        private readonly string archivoContactos;

        public Agenda()
        {
            // Usar ruta absoluta para asegurar que se guarde en ubicación conocida
            archivoContactos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contactos.txt");
            Console.WriteLine($"\n📁 Archivo de contactos: {archivoContactos}");
            
            contactos = new List<Contacto>();
            CargarContactosDesdeArchivo();
        }

        // Agregar contacto
        public void AgregarContacto(Contacto contacto)
        {
            if (string.IsNullOrWhiteSpace(contacto.Nombre))
            {
                Console.WriteLine("❌ Error: El nombre no puede estar vacío.");
                return;
            }

            contactos.Add(contacto);
            GuardarContactosEnArchivo();
            Console.WriteLine($"\n✅ Contacto '{contacto.Nombre}' agregado exitosamente.");
        }

        // Buscar contacto por nombre
        public Contacto BuscarContacto(string nombre)
        {
            return contactos.FirstOrDefault(c => 
                c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Buscar contactos por categoría
        public List<Contacto> BuscarPorCategoria(string categoria)
        {
            return contactos
                .Where(c => c.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Listar todos los contactos
        public void ListarContactos()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📋 LISTA DE CONTACTOS");
            Console.WriteLine(new string('=', 60));
            
            if (contactos.Count == 0)
            {
                Console.WriteLine("📭 No hay contactos registrados.");
                return;
            }

            int contador = 1;
            foreach (var contacto in contactos)
            {
                Console.WriteLine($"{contador}. {contacto}");
                contador++;
            }
            
            Console.WriteLine($"\n📊 Total: {contactos.Count} contacto(s)");
        }

        // Eliminar contacto
        public bool EliminarContacto(string nombre)
        {
            var contacto = BuscarContacto(nombre);
            if (contacto != null)
            {
                contactos.Remove(contacto);
                GuardarContactosEnArchivo();
                return true;
            }
            return false;
        }

        // Contar contactos por categoría
        public void MostrarEstadisticas()
        {
            var grupos = contactos
                .GroupBy(c => c.Categoria)
                .Select(g => new { Categoria = g.Key, Cantidad = g.Count() });

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📈 ESTADÍSTICAS");
            Console.WriteLine(new string('=', 60));
            
            if (!grupos.Any())
            {
                Console.WriteLine("📭 No hay contactos registrados.");
                return;
            }

            foreach (var grupo in grupos.OrderByDescending(g => g.Cantidad))
            {
                Console.WriteLine($"📁 {grupo.Categoria}: {grupo.Cantidad} contacto(s)");
            }
            
            Console.WriteLine($"\n📊 Total general: {contactos.Count} contacto(s)");
        }

        // Guardar contactos en archivo
        private void GuardarContactosEnArchivo()
        {
            try
            {
                Console.WriteLine($"\n💾 Guardando {contactos.Count} contacto(s) en archivo...");
                
                using (StreamWriter sw = new StreamWriter(archivoContactos))
                {
                    foreach (var contacto in contactos)
                    {
                        sw.WriteLine(contacto.ToCSV());
                    }
                }
                
                Console.WriteLine($"✅ Contactos guardados exitosamente en: {archivoContactos}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar contactos: {ex.Message}");
                Console.WriteLine($"Detalle: {ex.InnerException?.Message}");
            }
        }

        // Cargar contactos desde archivo
        private void CargarContactosDesdeArchivo()
        {
            try
            {
                if (File.Exists(archivoContactos))
                {
                    string[] lineas = File.ReadAllLines(archivoContactos);
                    int contactosCargados = 0;
                    
                    foreach (string linea in lineas)
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length == 4)
                            {
                                contactos.Add(new Contacto(
                                    datos[0].Trim(),
                                    datos[1].Trim(),
                                    datos[2].Trim(),
                                    datos[3].Trim()
                                ));
                                contactosCargados++;
                            }
                        }
                    }
                    
                    if (contactosCargados > 0)
                    {
                        Console.WriteLine($"✅ Se cargaron {contactosCargados} contacto(s) desde el archivo.");
                    }
                }
                else
                {
                    Console.WriteLine("📄 Archivo de contactos no encontrado. Se creará uno nuevo al agregar contactos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error al cargar contactos: {ex.Message}");
            }
        }

        // Mostrar información del archivo
        public void MostrarInfoArchivo()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📄 INFORMACIÓN DEL ARCHIVO");
            Console.WriteLine(new string('=', 60));
            
            if (File.Exists(archivoContactos))
            {
                var info = new FileInfo(archivoContactos);
                Console.WriteLine($"📍 Ruta: {archivoContactos}");
                Console.WriteLine($"📏 Tamaño: {info.Length} bytes");
                Console.WriteLine($"📅 Última modificación: {info.LastWriteTime}");
                
                string[] lineas = File.ReadAllLines(archivoContactos);
                Console.WriteLine($"📝 Líneas en archivo: {lineas.Length}");
                
                Console.WriteLine("\n📋 Contenido del archivo:");
                Console.WriteLine(new string('-', 60));
                foreach (var linea in lineas)
                {
                    Console.WriteLine(linea);
                }
            }
            else
            {
                Console.WriteLine("📭 El archivo de contactos no existe aún.");
            }
        }
    }

    // ==================== PROGRAMA PRINCIPAL ====================
    class Program
    {
        static void MostrarMenu()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📱 AGENDA TELEFÓNICA - SISTEMA DE GESTIÓN");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("1. Agregar nuevo contacto");
            Console.WriteLine("2. Buscar contacto por nombre");
            Console.WriteLine("3. Listar todos los contactos");
            Console.WriteLine("4. Buscar contactos por categoría");
            Console.WriteLine("5. Eliminar contacto");
            Console.WriteLine("6. Mostrar estadísticas");
            Console.WriteLine("7. Ver información del archivo");
            Console.WriteLine("8. Salir");
            Console.WriteLine(new string('=', 60));
            Console.Write("👉 Seleccione una opción (1-8): ");
        }

        static void Pausa()
        {
            Console.Write("\n⏎ Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MostrarEncabezado(string titulo)
        {
            Console.WriteLine($"\n{new string('═', 60)}");
            Console.WriteLine($"📌 {titulo}");
            Console.WriteLine(new string('═', 60));
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Agenda agenda = new Agenda();
            bool salir = false;

            Console.WriteLine("\n" + new string('★', 60));
            Console.WriteLine("🌟 BIENVENIDO AL SISTEMA DE AGENDA TELEFÓNICA 🌟");
            Console.WriteLine(new string('★', 60));

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": // Agregar contacto
                        MostrarEncabezado("AGREGAR NUEVO CONTACTO");
                        
                        Console.Write("👤 Nombre completo: ");
                        string nombre = Console.ReadLine();
                        
                        Console.Write("📞 Teléfono: ");
                        string telefono = Console.ReadLine();
                        
                        Console.Write("📧 Email: ");
                        string email = Console.ReadLine();
                        
                        Console.Write("🏷️  Categoría (Familiar/Trabajo/Amigo/Otro): ");
                        string categoria = Console.ReadLine();
                        
                        agenda.AgregarContacto(new Contacto(nombre, telefono, email, categoria));
                        Pausa();
                        break;

                    case "2": // Buscar contacto
                        MostrarEncabezado("BUSCAR CONTACTO");
                        
                        Console.Write("🔍 Ingrese el nombre a buscar: ");
                        var contactoEncontrado = agenda.BuscarContacto(Console.ReadLine());
                        
                        if (contactoEncontrado != null)
                        {
                            Console.WriteLine("\n✅ CONTACTO ENCONTRADO:");
                            Console.WriteLine(new string('─', 60));
                            Console.WriteLine(contactoEncontrado);
                        }
                        else
                        {
                            Console.WriteLine("\n❌ Contacto no encontrado.");
                        }
                        Pausa();
                        break;

                    case "3": // Listar contactos
                        agenda.ListarContactos();
                        Pausa();
                        break;

                    case "4": // Buscar por categoría
                        MostrarEncabezado("BUSCAR POR CATEGORÍA");
                        
                        Console.Write("🏷️  Ingrese la categoría (Familiar/Trabajo/Amigo/Otro): ");
                        string categoriaBuscar = Console.ReadLine();
                        
                        var contactosCategoria = agenda.BuscarPorCategoria(categoriaBuscar);
                        
                        if (contactosCategoria.Count > 0)
                        {
                            Console.WriteLine($"\n📋 Contactos en categoría '{categoriaBuscar}':");
                            Console.WriteLine(new string('─', 60));
                            
                            int i = 1;
                            foreach (var c in contactosCategoria)
                            {
                                Console.WriteLine($"{i}. {c}");
                                i++;
                            }
                            Console.WriteLine($"\n📊 Total: {contactosCategoria.Count} contacto(s)");
                        }
                        else
                        {
                            Console.WriteLine($"\n📭 No hay contactos en la categoría '{categoriaBuscar}'.");
                        }
                        Pausa();
                        break;

                    case "5": // Eliminar contacto
                        MostrarEncabezado("ELIMINAR CONTACTO");
                        
                        Console.Write("🗑️  Ingrese el nombre del contacto a eliminar: ");
                        string nombreEliminar = Console.ReadLine();
                        
                        if (agenda.EliminarContacto(nombreEliminar))
                        {
                            Console.WriteLine($"\n✅ Contacto '{nombreEliminar}' eliminado exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine($"\n❌ No se encontró el contacto '{nombreEliminar}'.");
                        }
                        Pausa();
                        break;

                    case "6": // Estadísticas
                        agenda.MostrarEstadisticas();
                        Pausa();
                        break;

                    case "7": // Ver información del archivo
                        agenda.MostrarInfoArchivo();
                        Pausa();
                        break;

                    case "8": // Salir
                        salir = true;
                        Console.WriteLine("\n" + new string('★', 60));
                        Console.WriteLine("🙏 ¡Gracias por usar la Agenda Telefónica!");
                        Console.WriteLine("💾 Los contactos se han guardado automáticamente.");
                        Console.WriteLine("👋 ¡Hasta pronto!");
                        Console.WriteLine(new string('★', 60));
                        Console.WriteLine("\n⏎ Presione cualquier tecla para salir...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("\n❌ Opción inválida. Por favor seleccione 1-8.");
                        Pausa();
                        break;
                }
            }
        }
    }
}