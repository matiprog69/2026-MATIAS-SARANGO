// Importación del espacio de nombres System que contiene clases fundamentales
// como Console para entrada/salida y Array para manejo de arreglos
using System;

// Definición del espacio de nombres para organizar las clases relacionadas
namespace GestionEstudiantes
{
    /// <summary>
    /// Clase principal que representa la entidad Estudiante
    /// Esta clase encapsula todos los datos y comportamientos de un estudiante
    /// siguiendo los principios de la Programación Orientada a Objetos
    /// </summary>
    public class Estudiante
    {
        // =============================================
        // DECLARACIÓN DE CAMPOS PRIVADOS (VARIABLES MIEMBRO)
        // =============================================
        
        // Campo para almacenar el identificador único del estudiante
        // Private asegura que solo sea accesible desde dentro de esta clase
        private int id;
        
        // Campo para almacenar los nombres del estudiante
        private string nombres;
        
        // Campo para almacenar los apellidos del estudiante
        private string apellidos;
        
        // Campo para almacenar la dirección del estudiante
        private string direccion;
        
        // DECLARACIÓN IMPORTANTE: Array unidimensional para teléfonos
        // Este array almacenará exactamente 3 números de teléfono
        // Los arrays en C# son de tamaño fijo una vez creados
        private string[] telefonos; 
        
        // =============================================
        // PROPIEDADES PÚBLICAS (GETTERS Y SETTERS)
        // =============================================
        
        // Propiedad para acceder al campo 'id'
        // Proporciona acceso controlado al campo privado
        public int Id 
        { 
            // Getter: devuelve el valor actual del campo
            get { return id; }
            
            // Setter: asigna un nuevo valor al campo
            // 'value' es una palabra clave que representa el valor asignado
            set { id = value; }
        }
        
        // Propiedad para acceder a los nombres
        public string Nombres 
        { 
            get { return nombres; }
            set { nombres = value; }
        }
        
        // Propiedad para acceder a los apellidos
        public string Apellidos 
        { 
            get { return apellidos; }
            set { apellidos = value; }
        }
        
        // Propiedad para acceder a la dirección
        public string Direccion 
        { 
            get { return direccion; }
            set { direccion = value; }
        }
        
        // =============================================
        // CONSTRUCTOR DE LA CLASE
        // =============================================
        
        /// <summary>
        /// Constructor principal de la clase Estudiante
        /// Se ejecuta automáticamente al crear un nuevo objeto Estudiante
        /// Inicializa todos los campos básicos y crea el array de teléfonos
        /// </summary>
        /// <param name="id">Identificador único (entero)</param>
        /// <param name="nombres">Cadena con los nombres del estudiante</param>
        /// <param name="apellidos">Cadena con los apellidos del estudiante</param>
        /// <param name="direccion">Cadena con la dirección completa</param>
        public Estudiante(int id, string nombres, string apellidos, string direccion)
        {
            // Inicialización de campos usando la palabra clave 'this'
            // 'this' se refiere a la instancia actual del objeto
            // Diferencia entre parámetros y campos con el mismo nombre
            
            this.id = id;           // Asigna el parámetro id al campo id
            this.nombres = nombres; // Asigna nombres del parámetro al campo
            this.apellidos = apellidos; // Asigna apellidos
            this.direccion = direccion; // Asigna dirección
            
            // =============================================
            // CREACIÓN DEL ARRAY DE TELÉFONOS
            // =============================================
            
            // Creación de un nuevo array de strings con capacidad para 3 elementos
            // 'new string[3]' reserva memoria para 3 strings
            // Todos los elementos se inicializan automáticamente a null
            this.telefonos = new string[3];
            
            // El array ahora existe pero está vacío:
            // telefonos[0] = null
            // telefonos[1] = null
            // telefonos[2] = null
        }
        
        // =============================================
        // MÉTODOS PARA MANEJAR TELÉFONOS
        // =============================================
        
        /// <summary>
        /// Método para asignar los tres números de teléfono al array
        /// Demuestra cómo trabajar con arrays: asignar valores a posiciones específicas
        /// </summary>
        /// <param name="telefono1">Primer número (irá a la posición 0)</param>
        /// <param name="telefono2">Segundo número (posición 1)</param>
        /// <param name="telefono3">Tercer número (posición 2)</param>
        public void EstablecerTelefonos(string telefono1, string telefono2, string telefono3)
        {
            // Asignación directa a cada posición del array
            // Los arrays en C# son de base 0 (el primer índice es 0)
            
            telefonos[0] = telefono1;  // Primer teléfono en índice 0
            telefonos[1] = telefono2;  // Segundo teléfono en índice 1
            telefonos[2] = telefono3;  // Tercer teléfono en índice 2
            
            // El array ahora contiene:
            // [0] = telefono1
            // [1] = telefono2
            // [2] = telefono3
        }
        
        /// <summary>
        /// Método para obtener todos los teléfonos como array completo
        /// Retorna una referencia al array interno (no una copia)
        /// </summary>
        /// <returns>El array completo de teléfonos</returns>
        public string[] ObtenerTelefonos()
        {
            // Retorna el array completo
            // Importante: esto retorna la referencia al array original
            return telefonos;
        }
        
        /// <summary>
        /// Método para obtener un teléfono específico por índice
        /// Incluye validación para evitar errores de índice fuera de rango
        /// </summary>
        /// <param name="indice">Posición del teléfono (0, 1 o 2)</param>
        /// <returns>El número de teléfono en esa posición o mensaje de error</returns>
        public string ObtenerTelefono(int indice)
        {
            // Validación del índice usando operadores lógicos
            // Verifica que el índice esté entre 0 y 2 (inclusive)
            if (indice >= 0 && indice < 3)
                return telefonos[indice];  // Retorna el valor en esa posición
            else
                return "Índice no válido"; // Mensaje de error para índice inválido
        }
        
        // =============================================
        // MÉTODO PARA MOSTRAR INFORMACIÓN
        // =============================================
        
        /// <summary>
        /// Método que muestra toda la información del estudiante en consola
        /// Incluye un bucle para recorrer el array de teléfonos
        /// </summary>
        public void MostrarInformacion()
        {
            // Encabezado visual para separar la información
            Console.WriteLine("\n=== INFORMACIÓN DEL ESTUDIANTE ===");
            
            // Mostrar datos básicos usando interpolación de strings ($"")
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Nombre: {nombres} {apellidos}");
            Console.WriteLine($"Dirección: {direccion}");
            Console.WriteLine("Teléfonos:");
            
            // =============================================
            // BUCLE PARA RECORRER EL ARRAY DE TELÉFONOS
            // =============================================
            
            // Bucle for tradicional para recorrer arrays
            // 'telefonos.Length' retorna el tamaño del array (3 en este caso)
            for (int i = 0; i < telefonos.Length; i++)
            {
                // i+1 muestra números 1,2,3 en vez de 0,1,2 para el usuario
                // telefonos[i] accede al elemento en la posición i
                Console.WriteLine($"  {i + 1}. {telefonos[i]}");
            }
            // El bucle itera 3 veces:
            // i=0 → muestra posición 0 como "1. ..."
            // i=1 → muestra posición 1 como "2. ..."
            // i=2 → muestra posición 2 como "3. ..."
        }
    }
    
    // =============================================
    // CLASE PRINCIPAL DEL PROGRAMA
    // =============================================
    
    /// <summary>
    /// Clase Program que contiene el método Main
    /// Es el punto de entrada de la aplicación
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método Main - Punto de entrada principal del programa
        /// Se ejecuta automáticamente al iniciar la aplicación
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no usados aquí)</param>
        static void Main(string[] args)
        {
            // Título del programa
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE ESTUDIANTES ===\n");
            
            // =============================================
            // CREACIÓN DE ARRAY PARA MÚLTIPLES ESTUDIANTES
            // =============================================
            
            // Creación de un array de objetos Estudiante
            // 'new Estudiante[2]' crea un array con capacidad para 2 estudiantes
            // Los elementos se inicializan a null
            Estudiante[] listaEstudiantes = new Estudiante[2];
            // Estructura del array:
            // [0] = null
            // [1] = null
            
            // =============================================
            // REGISTRO DEL PRIMER ESTUDIANTE
            // =============================================
            
            Console.WriteLine("REGISTRO DEL PRIMER ESTUDIANTE");
            Console.WriteLine("-------------------------------");
            
            // Creación del primer objeto Estudiante usando el constructor
            // Se pasan los valores directamente como argumentos con nombre
            Estudiante estudiante1 = new Estudiante(
                id: 1001,                           // Valor para el parámetro id
                nombres: "María José",              // Valor para nombres
                apellidos: "García Pérez",          // Valor para apellidos
                direccion: "Av. Principal 123, Ciudad" // Valor para dirección
            );
            
            // Llamada al método para establecer los teléfonos
            // Pasa tres strings que se almacenarán en el array interno
            estudiante1.EstablecerTelefonos(
                "0991234567",  // Va a telefonos[0]
                "022345678",   // Va a telefonos[1]
                "022987654"    // Va a telefonos[2]
            );
            
            // Almacenar el estudiante en el array listaEstudiantes
            listaEstudiantes[0] = estudiante1;  // Posición 0 ahora apunta a estudiante1
            
            // =============================================
            // REGISTRO DEL SEGUNDO ESTUDIANTE
            // =============================================
            
            Console.WriteLine("\nREGISTRO DEL SEGUNDO ESTUDIANTE");
            Console.WriteLine("-------------------------------");
            
            // Creación del segundo estudiante
            Estudiante estudiante2 = new Estudiante(
                id: 1002,
                nombres: "Carlos Andrés",
                apellidos: "Rodríguez López",
                direccion: "Calle Secundaria 456, Pueblo"
            );
            
            // Establecer sus teléfonos
            estudiante2.EstablecerTelefonos("0987654321", "022111222", "022333444");
            
            // Almacenar en el array
            listaEstudiantes[1] = estudiante2;  // Posición 1 ahora apunta a estudiante2
            
            // =============================================
            // MOSTRAR TODOS LOS ESTUDIANTES
            // =============================================
            
            Console.WriteLine("\n=== LISTA COMPLETA DE ESTUDIANTES ===");
            
            // Bucle foreach para recorrer el array de estudiantes
            // 'foreach' es ideal para recorrer colecciones cuando no necesitamos modificar
            // o saber el índice actual
            foreach (Estudiante est in listaEstudiantes)
            {
                // Para cada estudiante en el array, llamar a MostrarInformacion()
                est.MostrarInformacion();
            }
            // Este bucle itera 2 veces:
            // 1ra: est = estudiante1 → muestra info de estudiante1
            // 2da: est = estudiante2 → muestra info de estudiante2
            
            // =============================================
            // DEMOSTRACIÓN DE ACCESO INDIVIDUAL A TELÉFONOS
            // =============================================
            
            Console.WriteLine("\n=== DEMOSTRACIÓN DE ACCESO A TELÉFONOS ===");
            
            // Acceder a teléfono específico usando método ObtenerTelefono()
            Console.WriteLine($"Primer teléfono del estudiante 1: {estudiante1.ObtenerTelefono(0)}");
            // estudiante1.ObtenerTelefono(0) retorna el valor en telefonos[0]
            
            Console.WriteLine($"Segundo teléfono del estudiante 2: {estudiante2.ObtenerTelefono(1)}");
            // estudiante2.ObtenerTelefono(1) retorna el valor en telefonos[1]
            
            // =============================================
            // MANEJO DIRECTO DEL ARRAY DE TELÉFONOS
            // =============================================
            
            Console.WriteLine("\n=== TODOS LOS TELÉFONOS DEL ESTUDIANTE 1 ===");
            
            // Obtener el array completo de teléfonos
            string[] telefonosEst1 = estudiante1.ObtenerTelefonos();
            // telefonosEst1 ahora apunta al mismo array que tiene estudiante1.telefonos
            
            // Bucle for tradicional para recorrer el array obtenido
            for (int i = 0; i < telefonosEst1.Length; i++)
            {
                Console.WriteLine($"Teléfono {i + 1}: {telefonosEst1[i]}");
            }
            // Muestra:
            // Teléfono 1: 0991234567
            // Teléfono 2: 022345678
            // Teléfono 3: 022987654
            
            // =============================================
            // FIN DEL PROGRAMA
            // =============================================
            
            Console.WriteLine("\n=== PROGRAMA FINALIZADO ===");
            Console.WriteLine("Presione cualquier tecla para salir...");
            
            // Pausa la ejecución hasta que el usuario presione una tecla
            Console.ReadKey();
        }
    }
}
}