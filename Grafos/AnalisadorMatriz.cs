using System;
using System.Collections.Generic;

namespace ProjetoGrafosGrupo5.Grafos
{
    public static class AnalisadorMatriz
    {
        public static void Analisar(GrafoMatrizAdjacencia grafo)
        {
            int V = grafo.Vertices;
            bool dir = grafo.Direcionado;

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("          ANÁLISE DO GRAFO (MATRIZ)");
            Console.WriteLine(new string('=', 50));

            // 1. Características básicas
            Console.WriteLine("\n[1] CARACTERÍSTICAS BÁSICAS");
            Console.WriteLine($"  Vértices (|V|) = {V}");
            Console.WriteLine($"  Direcionado   = {(dir ? "Sim" : "Não")}");

            int arestas = 0;
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    if (grafo.ExisteAresta(i, j))
                        arestas++;
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
            long espacoMatriz = (long)V * V * sizeof(int);
            long espacoListaAprox = (long)(arestas * 2 * sizeof(int)) + (long)V * 24;
            Console.WriteLine("\n[2] ESPAÇO OCUPADO (memória)");
            Console.WriteLine($"  Matriz: {espacoMatriz / 1024.0:F2} KB  (O(V²))");
            Console.WriteLine($"  Lista : {espacoListaAprox / 1024.0:F2} KB  (O(V + A))");
            double economia = (espacoMatriz - espacoListaAprox) / 1024.0;
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
                    int[] viz = grafo.ObterVizinhos(i);
                    int outDeg = viz.Length;
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
                    int[] viz = grafo.ObterVizinhos(i);
                    Console.WriteLine($"  {i,6}  | {viz.Length,3}");
                }
            }

            // 4. Matriz (se V ≤ 10 mostra toda, senão parcial)
            Console.WriteLine("\n[4] MATRIZ DE ADJACÊNCIA");
            int limite = 10;
            if (V <= limite)
                ImprimirMatrizComCabecalho(grafo);
            else
            {
                Console.WriteLine($"  Matriz muito grande ({V}x{V}). Exibindo apenas os {limite} primeiros:");
                ImprimirMatrizParcial(grafo, limite);
            }

            // 5. Exemplo de consulta
            if (V > 0)
            {
                Console.WriteLine("\n[5] EXEMPLO DE CONSULTA");
                int[] viz0 = grafo.ObterVizinhos(0);
                Console.WriteLine($"  Vizinhos do vértice 0: {(viz0.Length > 0 ? string.Join(", ", viz0) : "nenhum")}");
                Console.WriteLine($"  Grau do vértice 0: {viz0.Length}");
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
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ImprimirMatrizComCabecalho(GrafoMatrizAdjacencia grafo)
        {
            int V = grafo.Vertices;
            Console.Write("     ");
            for (int j = 0; j < V; j++)
                Console.Write($"{j,3} ");
            Console.WriteLine();
            for (int i = 0; i < V; i++)
            {
                Console.Write($"{i,2} |");
                for (int j = 0; j < V; j++)
                {
                    int peso = grafo.ObterPeso(i, j);
                    string val = (peso == 0) ? " ." : $"{peso,2}";
                    Console.Write($"{val,3} ");
                }
                Console.WriteLine();
            }
        }

        private static void ImprimirMatrizParcial(GrafoMatrizAdjacencia grafo, int limite)
        {
            int V = grafo.Vertices;
            int exibir = Math.Min(V, limite);
            Console.Write("     ");
            for (int j = 0; j < exibir; j++)
                Console.Write($"{j,3} ");
            Console.WriteLine(" ...");
            for (int i = 0; i < exibir; i++)
            {
                Console.Write($"{i,2} |");
                for (int j = 0; j < exibir; j++)
                {
                    int peso = grafo.ObterPeso(i, j);
                    string val = (peso == 0) ? " ." : $"{peso,2}";
                    Console.Write($"{val,3} ");
                }
                Console.WriteLine(" ...");
            }
            Console.WriteLine("...");
        }
    }
}