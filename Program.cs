using ComplejidadTPF;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido al juego Who is Who!");
        // Datos simulados para el juego Who is Who
        var datos = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>() { { "Es rubio", "si" }, { "Tiene barba", "si" }, { "clase", "Pedro" } },
            new Dictionary<string, string>() { { "Es rubio", "si" }, { "Tiene barba", "si" }, { "clase", "Pedro" } },
            new Dictionary<string, string>() { { "Es rubio", "si" }, { "Tiene barba", "no" }, { "clase", "Lucas" } },
            new Dictionary<string, string>() { { "Es rubio", "no" }, { "Tiene barba", "no" }, { "clase", "Ana" } },
            new Dictionary<string, string>() { { "Es rubio", "no" }, { "Tiene barba", "no" }, { "clase", "Ana" } },
            new Dictionary<string, string>() { { "Es rubio", "no" }, { "Tiene barba", "no" }, { "clase", "Juan" } }
        };
        if (datos.Count == 0)
        {
            Console.WriteLine("No hay datos disponibles para construir el árbol.");
            return;
        }
        // Crear clasificador raíz
        Clasificador clasificador = new Clasificador(datos, "Es rubio", "si");

        // Crear estrategia y árbol de decisión
        Estrategia estrategia = new Estrategia();
        ArbolBinario<DecisionData> arbol = estrategia.CrearArbol(clasificador);

        // Consultas solicitadas por el TP
        Console.WriteLine("Consulta 1 - Predicciones:");
        Console.WriteLine(estrategia.Consulta1(arbol));
        Console.WriteLine();

        Console.WriteLine("Consulta 2 - Caminos:");
        Console.WriteLine(estrategia.Consulta2(arbol));
        Console.WriteLine();

        Console.WriteLine("Consulta 3 - Datos por nivel:");
        Console.WriteLine(estrategia.Consulta3(arbol));

        Console.WriteLine("\nPresione una tecla para salir...");
        Console.ReadKey();
    }
}