using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGrafos.Grafos;
using ProjetoGrafos.Dijkstra;

namespace ProjetoGrafos.Desafio
{
    public static class DesafioDijkstra
    {
        public static void Executar()
        {
            Console.WriteLine("\n=== DESAFIO DE PROGRAMAÇÃO COMPETITIVA ===");

            // Lê a primeira linha com V e A, tratando possíveis nulos
            Console.Write("Digite o número de Vértices (V) e Arestas (A): ");
            string? linha = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(linha))
            {
                Console.Write("Entrada inválida. Digite novamente: ");
                linha = Console.ReadLine();
            }
            var partes = linha.Split();
            if (partes.Length < 2)
            {
                Console.WriteLine("Formato inválido. Use: V A");
                return;
            }
            int V = int.Parse(partes[0]);
            int A = int.Parse(partes[1]);

            var grafo = new GrafoListaAdjacencia(V, direcionado: true);

            Console.WriteLine($"Digite as {A} arestas no formato: origem destino peso");
            for (int i = 0; i < A; i++)
            {
                string? linhaAresta = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(linhaAresta))
                {
                    Console.Write("Entrada inválida. Digite novamente: ");
                    linhaAresta = Console.ReadLine();
                }
                var partesAresta = linhaAresta.Split();
                if (partesAresta.Length < 3)
                {
                    Console.WriteLine("Formato inválido. Use: origem destino peso");
                    i--; // repete a iteração
                    continue;
                }
                int u = int.Parse(partesAresta[0]);
                int v = int.Parse(partesAresta[1]);
                int w = int.Parse(partesAresta[2]);
                grafo.AdicionarAresta(u, v, w);
            }

            Console.Write("Digite o vértice fonte: ");
            string? linhaFonte = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(linhaFonte))
            {
                Console.Write("Entrada inválida. Digite novamente: ");
                linhaFonte = Console.ReadLine();
            }
            int fonte = int.Parse(linhaFonte);

            var (dist, _) = DijkstraHeap.Executar(grafo, fonte);

            var ordenados = Enumerable.Range(0, V)
                                      .OrderBy(v => dist[v])
                                      .Select(v => (v, dist[v]))
                                      .ToList();

            Console.WriteLine("\nVértices ordenados por distância (menor para maior):");
            foreach (var (v, d) in ordenados)
            {
                string distStr = (d == int.MaxValue) ? "INF" : d.ToString();
                Console.WriteLine($"Vértice {v}: {distStr}");
            }
        }
    }
}