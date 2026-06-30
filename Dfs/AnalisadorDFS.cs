using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Dfs
{
    public static class AnalisadorDFS
    {
        public static void Analisar(GrafoListaAdjacencia grafo, int[] d, int[] f, int[] pred, List<ArestaClassificada> arestas)
        {
            int V = grafo.Vertices;

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("          BUSCA EM PROFUNDIDADE (DFS)");
            Console.WriteLine(new string('=', 50));

            // Tabela de tempos
            Console.WriteLine("\nVértice | Descoberta (d) | Finalização (f) | Predecessor");
            Console.WriteLine("--------|---------------|-----------------|-------------");
            for (int i = 0; i < V; i++)
            {
                string dStr = d[i] == -1 ? "∞" : d[i].ToString();
                string fStr = f[i] == -1 ? "∞" : f[i].ToString();
                string predStr = pred[i] == -1 ? "-" : pred[i].ToString();
                Console.WriteLine($"  {i,2}    | {dStr,12}   | {fStr,13}     | {predStr,6}");
            }

            // Árvore DFS (floresta)
            Console.WriteLine("\nÁRVORE DFS (predecessores):");
            // Encontrar raízes (vértices com pred = -1)
            for (int i = 0; i < V; i++)
                if (pred[i] == -1)
                    Console.WriteLine($"  Raiz: {i}");
            for (int i = 0; i < V; i++)
                if (pred[i] != -1)
                    Console.WriteLine($"  {pred[i]} → {i}");

            // Classificação das arestas
            Console.WriteLine("\nCLASSIFICAÇÃO DAS ARESTAS:");
            if (arestas.Count == 0)
                Console.WriteLine("  (grafo sem arestas)");
            else
            {
                // Agrupar por tipo
                var grupos = arestas.GroupBy(a => a.Tipo);
                foreach (var grupo in grupos)
                {
                    Console.WriteLine($"  {grupo.Key}:");
                    foreach (var aresta in grupo)
                        Console.WriteLine($"    {aresta.Origem} → {aresta.Destino}");
                }
            }

            // Verificar se há ciclos (arestas de retorno)
            bool temCiclo = arestas.Any(a => a.Tipo == "Retorno");
            Console.WriteLine($"\nCICLOS: {(temCiclo ? "SIM (detectado)" : "NÃO")}");

            Console.WriteLine("\n" + new string('=', 50));
           
        }
    }
}