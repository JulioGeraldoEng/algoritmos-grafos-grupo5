using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Dijkstra
{
    public static class AnalisadorDijkstra
    {
        public static void Analisar(GrafoListaAdjacencia grafo, int origem, int[] distancias, int[] predecessores)
        {
            int V = grafo.Vertices;

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("          DIJKSTRA – CAMINHO MÍNIMO");
            Console.WriteLine(new string('=', 50));

            Console.WriteLine($"\nOrigem: vértice {origem}\n");

            // Tabela de distâncias e predecessores
            Console.WriteLine("Vértice | Distância | Predecessor");
            Console.WriteLine("--------|-----------|-------------");
            for (int i = 0; i < V; i++)
            {
                string distStr = (distancias[i] == int.MaxValue) ? "∞" : distancias[i].ToString();
                string predStr = (predecessores[i] == -1) ? "-" : predecessores[i].ToString();
                Console.WriteLine($"  {i,2}    | {distStr,7}    | {predStr,7}");
            }

            // Árvore de caminhos mínimos (predecessores)
            Console.WriteLine("\nÁRVORE DE CAMINHOS MÍNIMOS (predecessores):");
            Console.WriteLine("  Raiz: " + origem);
            for (int i = 0; i < V; i++)
            {
                if (i == origem) continue;
                if (predecessores[i] != -1)
                    Console.WriteLine($"  {predecessores[i]} → {i}  (distância: {distancias[i]})");
                else if (distancias[i] == int.MaxValue)
                    Console.WriteLine($"  {i} é inalcançável a partir de {origem}");
            }

            // Reconstruir e exibir o caminho mínimo para cada vértice (opcional)
            Console.WriteLine("\nCAMINHOS MÍNIMOS A PARTIR DA ORIGEM:");
            for (int i = 0; i < V; i++)
            {
                if (i == origem) continue;
                if (distancias[i] == int.MaxValue) continue;

                var caminho = new List<int>();
                int atual = i;
                while (atual != -1)
                {
                    caminho.Add(atual);
                    atual = predecessores[atual];
                }
                caminho.Reverse();
                Console.WriteLine($"  {origem} → {i}: {string.Join(" → ", caminho)} (distância: {distancias[i]})");
            }

            // Vértices inalcançáveis
            var inalcancaveis = new List<int>();
            for (int i = 0; i < V; i++)
                if (distancias[i] == int.MaxValue)
                    inalcancaveis.Add(i);

            if (inalcancaveis.Count > 0)
                Console.WriteLine($"\nVértices inalcançáveis: {string.Join(", ", inalcancaveis)}");
            else
                Console.WriteLine("\nTodos os vértices são alcançáveis a partir da origem.");

            Console.WriteLine("\n" + new string('=', 50));
            
        }
    }
}