using System;

namespace AdielSoto
{
    class Program
    {
        const int N = 14;

        // Los dos arreglos paralelos: la posición i de ambos es el mismo paciente
        static string[] nombres = new string[N];
        static int[] gravedad = new int[N];

        // Cuántos pacientes hay guardados ahora mismo
        static int total = 0;

        static void Main(string[] args)
        {
            int opcion = 0;

            InicializarValores();

            while (opcion != 6)
            {
                MostrarMenu();
                opcion = int.Parse(Console.ReadLine());

                switch(opcion)
                {
                    case 1:
                        IngresarPaciente();
                        break;
                    case 2:
                        ConsultarLista();
                        break;
                    case 3:
                        ModificarGravedad();
                        break;
                    case 4:
                        EliminarPaciente();
                        break;
                    case 5:
                        Reportes();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void InicializarValores()
        {
            for (int i = 0; i < N; i++)
            {
                nombres[i] = "";
                gravedad[i] = 0;
            }

            nombres[0] = "Maria Lopez";
            gravedad[0] = 4;

            nombres[1] = "Juan Mora";
            gravedad[1] = 2;

            nombres[2] = "Ana Rojas";
            gravedad[2] = 5;

            total = 3;
        }

        static void MostrarMenu()
        {
            Console.WriteLine("----- HOSPITAL SAN RAFAEL -----");
            Console.WriteLine($"Pacientes: {total} de {N}");
            Console.WriteLine("1. Ingresar paciente");
            Console.WriteLine("2. Ver lista de pacientes");
            Console.WriteLine("3. Cambiar gravedad de un paciente");
            Console.WriteLine("4. Dar de alta / eliminar paciente");
            Console.WriteLine("5. Reportes");
            Console.WriteLine("6. Salir");
            Console.Write("Elija una opción: ");
        }

        // Agrega un paciente
        static void IngresarPaciente()
        {
            if (total == N)
            {
                Console.WriteLine("No hay espacio, la lista está llena.");
                return;
            }

            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine();

            int nivel = LeerGravedad();

            nombres[total] = nombre;
            gravedad[total] = nivel;
            total = total + 1;

            Console.WriteLine("Paciente registrado.");
        }

        // Muestra los pacientes con su gravedad
        static void ConsultarLista()
        {
            if (total == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            for (int i = 0; i < total; i++)
            {
                Console.WriteLine((i + 1) + ". " + nombres[i] + " - Gravedad: " + gravedad[i]);
            }
        }

        // Cambia la gravedad de los pacientes
        static void ModificarGravedad()
        {
            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine();

            int pos = Buscar(nombre);

            if (pos == -1)
            {
                Console.WriteLine("Paciente no encontrado.");
            }
            else
            {
                Console.WriteLine("Gravedad actual: " + gravedad[pos]);
                gravedad[pos] = LeerGravedad();
                Console.WriteLine("Gravedad actualizada.");
            }
        }

        // Borra un paciente
        static void EliminarPaciente()
        {
            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine();

            int pos = Buscar(nombre);

            if (pos == -1)
            {
                Console.WriteLine("Paciente no encontrado.");
            }
            else
            {
                for (int i = pos; i < total - 1; i++)
                {
                    nombres[i] = nombres[i + 1];
                    gravedad[i] = gravedad[i + 1];
                }

                total = total - 1;
                nombres[total] = "";
                gravedad[total] = 0;

                Console.WriteLine("Paciente eliminado.");
            }
        }

        // Estadísticas simples de los pacientes
        static void Reportes()
        {
            if (total == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Console.WriteLine("--- REPORTE ---");
            Console.WriteLine("Total de pacientes: " + total);
            Console.WriteLine("Espacios libres: " + (N - total));

            // Cuántos pacientes hay en cada nivel
            for (int nivel = 5; nivel >= 1; nivel--)
            {
                int cantidad = 0;
                for (int i = 0; i < total; i++)
                {
                    if (gravedad[i] == nivel)
                    {
                        cantidad = cantidad + 1;
                    }
                }
                Console.WriteLine("Nivel " + nivel + ": " + cantidad + " paciente(s)");
            }

            // Buscar la gravedad más alta
            int mayor = 0;
            for (int i = 0; i < total; i++)
            {
                if (gravedad[i] > mayor)
                {
                    mayor = gravedad[i];
                }
            }

            Console.WriteLine("Más grave(s) (nivel " + mayor + "):");
            for (int i = 0; i < total; i++)
            {
                if (gravedad[i] == mayor)
                {
                    Console.WriteLine(" - " + nombres[i]);
                }
            }

            // Promedio
            int suma = 0;
            for (int i = 0; i < total; i++)
            {
                suma = suma + gravedad[i];
            }
            double promedio = (double)suma / total;
            Console.WriteLine("Gravedad promedio: " + promedio);
        }

        // Devuelve la posición del paciente, o -1 si no está
        static int Buscar(string nombre)
        {
            for (int i = 0; i < total; i++)
            {
                if (nombres[i].ToLower() == nombre.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        // Pide la gravedad hasta que el usuario escriba un número de 1 a 5
        static int LeerGravedad()
        {
            int nivel = 0;

            while (nivel < 1 || nivel > 5)
            {
                Console.Write("Nivel de gravedad (1 a 5): ");
                nivel = int.Parse(Console.ReadLine());
            }

            return nivel;
        }
    }
}

