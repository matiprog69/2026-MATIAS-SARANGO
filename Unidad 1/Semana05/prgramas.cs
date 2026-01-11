
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EjerciciosPOO
{
    // Interfaz común para todas las soluciones
    public interface ISolution
    {
        string Title { get; }
        void Run();
    }

    // Repositorio de asignaturas (reutilizable por varios ejercicios)
    public class SubjectRepository
    {
        private readonly List<string> _subjects = new()
        {
            "Matemáticas", "Física", "Química", "Historia", "Lengua"
        };

        public IReadOnlyList<string> GetAll() => _subjects;
    }

    // Ejercicio 1: Mostrar asignaturas
    public class SubjectListSolution : ISolution
    {
        private readonly SubjectRepository _repo;
        public string Title => "1) Mostrar asignaturas del curso";
        public SubjectListSolution(SubjectRepository repo) => _repo = repo;

        public void Run()
        {
            Console.WriteLine("Asignaturas del curso:");
            foreach (var s in _repo.GetAll())
            {
                Console.WriteLine($"- {s}");
            }
        }
    }

    // Ejercicio 2: "Yo estudio <asignatura>"
    public class StudyPrinterSolution : ISolution
    {
        private readonly SubjectRepository _repo;
        public string Title => "2) Mensaje 'Yo estudio <asignatura>'";
        public StudyPrinterSolution(SubjectRepository repo) => _repo = repo;

        public void Run()
        {
            foreach (var s in _repo.GetAll())
            {
                Console.WriteLine($"Yo estudio {s}");
            }
        }
    }

    // Modelo de nota por asignatura
    public class GradeEntry
    {
        public string Subject { get; }
        public double Grade { get; }

        public GradeEntry(string subject, double grade)
        {
            Subject = subject;
            Grade = grade;
        }

        public override string ToString() => $"En {Subject} has sacado {Grade}";
    }

    // Servicio de captura de notas
    public class GradeService
    {
        public List<GradeEntry> CaptureGrades(IReadOnlyList<string> subjects)
        {
            var list = new List<GradeEntry>();
            Console.WriteLine("Introduce tu nota para cada asignatura (usa punto decimal si es necesario).");
            foreach (var s in subjects)
            {
                var grade = ReadDoubleSafe($"{s}: ");
                list.Add(new GradeEntry(s, grade));
            }
            return list;
        }

        private static double ReadDoubleSafe(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                    return value;
                Console.WriteLine("Entrada no válida. Intenta nuevamente (usa dígitos y punto decimal).");
            }
        }
    }

    // Ejercicio 3: Notas por asignatura
    public class GradeBookSolution : ISolution
    {
        private readonly SubjectRepository _repo;
        private readonly GradeService _service = new();
        public string Title => "3) Capturar y mostrar notas por asignatura";
        public GradeBookSolution(SubjectRepository repo) => _repo = repo;

        public void Run()
        {
            var grades = _service.CaptureGrades(_repo.GetAll());
            Console.WriteLine("\nResultados:");
            foreach (var entry in grades)
            {
                Console.WriteLine(entry);
            }
        }
    }

    // Ejercicio 4: Lotería primitiva (lectura + orden)
    public class LotterySolution : ISolution
    {
        public string Title => "4) Lotería: leer números y mostrarlos ordenados";

        public void Run()
        {
            const int amount = 6;
            var numbers = new List<int>();

            Console.WriteLine($"Introduce {amount} números ganadores (enteros, sin repetir):");
            while (numbers.Count < amount)
            {
                Console.Write($"Número {numbers.Count + 1}: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var n))
                {
                    if (!numbers.Contains(n))
                        numbers.Add(n);
                    else
                        Console.WriteLine("Ese número ya fue introducido. Intenta con otro.");
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Debe ser un entero.");
                }
            }

            numbers.Sort();
            Console.WriteLine("\nNúmeros ganadores (menor a mayor):");
            Console.WriteLine(string.Join(", ", numbers));
        }
    }

    // Ejercicio 5: 1..10 en orden inverso, separados por comas
    public class NumberReverseSolution : ISolution
    {
        public string Title => "5) Números del 1 al 10 en orden inverso";

        public void Run()
        {
            var list = Enumerable.Range(1, 10).ToList();
            list.Reverse();
            Console.WriteLine(string.Join(", ", list));
        }
    }

    // Menú de la aplicación
    public class AppMenu
    {
        private readonly List<ISolution> _solutions;

        public AppMenu(List<ISolution> solutions)
        {
            _solutions = solutions;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n=== Ejercicios en C# (POO) ===");
                for (int i = 0; i < _solutions.Count; i++)
                    Console.WriteLine($"{i + 1}. {_solutions[i].Title}");
                Console.WriteLine("0. Salir");
                Console.Write("Elige una opción: ");

                var choice = Console.ReadLine();
                if (choice == "0") break;

                if (int.TryParse(choice, out var index) &&
                    index >= 1 && index <= _solutions.Count)
                {
                    Console.WriteLine();
                    _solutions[index - 1].Run();
                }
                else
                {
                    Console.WriteLine("Opción no válida.");
                }
            }
        }
    }

    // Punto de entrada
    public class Program
    {
        public static void Main()
        {
            var repo = new SubjectRepository();
            var solutions = new List<ISolution>
            {
                new SubjectListSolution(repo),
                new StudyPrinterSolution(repo),
                new GradeBookSolution(repo),
                new LotterySolution(),
                new NumberReverseSolution()
            };

            new AppMenu(solutions).Show();
        }
    }
}
