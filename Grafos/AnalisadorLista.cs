using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoGrafosGrupo5.Grafos
{
    public static class AnalisadorLista
    {
        public static void Analisar(GrafoListaAdjacencia grafo)
        {
            int V = grafo.Vertices;
            bool dir = grafo.Direcionado;

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("          ANÁLISE DO GRAFO (LISTA)");
            Console.WriteLine(new string('=', 50));

            // 1. Características básicas
            Console.WriteLine("\n[1] CARACTERÍSTICAS BÁSICAS");
            Console.WriteLine($"  Vértices (|V|) = {V}");
            Console.WriteLine($"  Direcionado   = {(dir ? "Sim" : "Não")}");

            int arestas = 0;
            for (int i = 0; i < V; i++)
                arestas += grafo.ObterVizinhos(i).Count();
            if (!dir) arestas /= 2;

            Console.WriteLine($"  Arestas (|A|) = {arestas}");

            int maxArestas = dir ? V * (V - 1) : V * (V - 1) / 2;
            double densidade = (maxArestas > 0) ? (double)arestas / maxArestas : 0;
            Console.WriteLine($"  Densidade     = {densidade:F4} ({densidade * 100:F2}%)");

            string classificacao;
            if (densidade > 0.5)
                classificacao = "DENSO  (recomendado: MATRIZ)";
            else if (densidade > 0.2)
                classificacao = "MEDIANAMENTE DENSO";
            else
                classificacao = "ESPARSO (recomendado: LISTA)";
            Console.WriteLine($"  Classificação = {classificacao}");

            // 2. Espaço ocupado
            long espacoLista = (long)V * 24; // overhead das listas
            for (int i = 0; i < V; i++)
                espacoLista += grafo.ObterVizinhos(i).Count() * 8; // cada tupla ~8 bytes
            long espacoMatriz = (long)V * V * sizeof(int);

            Console.WriteLine("\n[2] ESPAÇO OCUPADO (memória)");
            Console.WriteLine($"  Lista : {espacoLista / 1024.0:F2} KB  (O(V + A))");
            Console.WriteLine($"  Matriz: {espacoMatriz / 1024.0:F2} KB  (O(V²))");
            double economia = (espacoMatriz - espacoLista) / 1024.0;
            if (economia > 0)
                Console.WriteLine($"  Economia (lista vs. matriz): {economia:F2} KB");
            else
                Console.WriteLine($"  Economia (matriz vs. lista): {-economia:F2} KB");

            // 3. Graus
            Console.WriteLine("\n[3] GRAUS DOS VÉRTICES");
            if (dir)
            {
                Console.WriteLine("  Vértice | Saída | Entrada | Total");
                Console.WriteLine("  --------|-------|---------|------");
                for (int i = 0; i < V; i++)
                {
                    int outDeg = grafo.ObterVizinhos(i).Count();
                    int inDeg = 0;
                    for (int j = 0; j < V; j++)
                        if (grafo.ExisteAresta(j, i))
                            inDeg++;
                    Console.WriteLine($"  {i,6}  | {outDeg,5} | {inDeg,7} | {outDeg + inDeg,4}");
                }
            }
            else
            {
                Console.WriteLine("  Vértice | Grau");
                Console.WriteLine("  --------|-----");
                for (int i = 0; i < V; i++)
                {
                    int grau = grafo.ObterVizinhos(i).Count();
                    Console.WriteLine($"  {i,6}  | {grau,3}");
                }
            }

            // 4. Lista de adjacência (se V ≤ 15 mostra completa)
            Console.WriteLine("\n[4] LISTA DE ADJACÊNCIA");
            if (V <= 15)
                grafo.ImprimirLista();
            else
            {
                Console.WriteLine($"  Lista muito grande ({V} vértices). Exibindo apenas os 5 primeiros:");
                for (int i = 0; i < Math.Min(V, 5); i++)
                {
                    Console.Write($"{i}: ");
                    foreach (var (dest, peso) in grafo.ObterVizinhos(i))
                        Console.Write($"->({dest},{peso}) ");
                    Console.WriteLine(" ...");
                }
                Console.WriteLine("...");
            }

            // 5. Exemplo de consulta
            if (V > 0)
            {
                Console.WriteLine("\n[5] EXEMPLO DE CONSULTA");
                var viz0 = grafo.ObterVizinhos(0);
                Console.Write($"  Vizinhos do vértice 0: ");
                if (viz0.Any())
                    Console.WriteLine(string.Join(", ", viz0.Select(v => $"{v.destino}({v.peso})")));
                else
                    Console.WriteLine("nenhum");
                Console.WriteLine($"  Grau do vértice 0: {viz0.Count()}");
                if (V > 1)
                {
                    bool existe = grafo.ExisteAresta(0, 1);
                    int peso = grafo.ObterPeso(0, 1);
                    Console.WriteLine($"  Aresta 0→1: {(existe ? $"existe (peso {peso})" : "não existe")}");
                }
            }

            // 6. Recomendação
            Console.WriteLine("\n[6] RECOMENDAÇÃO");
            if (densidade > 0.5)
                Console.WriteLine("  ✅ Use MATRIZ DE ADJACÊNCIA: grafos densos, consultas O(1).");
            else
                Console.WriteLine("  ✅ Use LISTA DE ADJACÊNCIA: grafos esparsos, economiza memória.");

            Console.WriteLine("\n" + new string('=', 50));
        }
    }
}