using System;
using System.Collections.Generic;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Bfs
{
    public static class AnalisadorBFS
    {
        public static void Analisar(GrafoListaAdjacencia grafo, int origem, int[] distancias, int[] predecessores)
        {
            int V = grafo.Vertices;

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("          BUSCA EM LARGURA (BFS)");
            Console.WriteLine(new string('=', 50));

            Console.WriteLine($"\nOrigem: vértice {origem}\n");

            // Tabela de resultados
            Console.WriteLine("Vértice | Distância | Predecessor | Nível");
            Console.WriteLine("--------|-----------|-------------|-------");
            for (int i = 0; i < V; i++)
            {
                string distStr = (distancias[i] == -1) ? "∞" : distancias[i].ToString();
                string predStr = (predecessores[i] == -1) ? "-" : predecessores[i].ToString();
                string nivel = (distancias[i] == -1) ? "N/A" : (distancias[i] + 1).ToString();
                Console.WriteLine($"  {i,2}    | {distStr,7}   | {predStr,7}     | {nivel,4}");
            }

            // Árvore BFS
            Console.WriteLine("\nÁRVORE BFS (predecessores):");
            Console.WriteLine("  Raiz: " + origem);
            for (int i = 0; i < V; i++)
            {
                if (i == origem) continue;
                if (predecessores[i] != -1)
                    Console.WriteLine($"  {predecessores[i]} → {i}  (distância: {distancias[i]})");
                else if (distancias[i] == -1)
                    Console.WriteLine($"  {i} é inalcançável a partir de {origem}");
            }

            // Níveis (camadas)
            Console.WriteLine("\nNÍVEIS DA BFS:");
            int maxDist = 0;
            for (int i = 0; i < V; i++)
                if (distancias[i] > maxDist) maxDist = distancias[i];

            for (int nivel = 0; nivel <= maxDist; nivel++)
            {
                Console.Write($"  Nível {nivel}: ");
                List<int> vertices = new List<int>();
                for (int i = 0; i < V; i++)
                    if (distancias[i] == nivel)
                        vertices.Add(i);
                Console.WriteLine(string.Join(", ", vertices));
            }

            // Vértices inalcançáveis
            List<int> inalcancaveis = new List<int>();
            for (int i = 0; i < V; i++)
                if (distancias[i] == -1)
                    inalcancaveis.Add(i);

            if (inalcancaveis.Count > 0)
                Console.WriteLine($"\nVértices inalcançáveis: {string.Join(", ", inalcancaveis)}");
            else
                Console.WriteLine("\nTodos os vértices são alcançáveis a partir da origem.");

            Console.WriteLine("\n" + new string('=', 50));
        }
    }
}