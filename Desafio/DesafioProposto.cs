using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ProjetoGrafos.Desafio
{
    public static class DesafioProposto
    {
        private const int INF = 1000000000;

        public static void Executar()
        {
            Console.Clear();

            string caminho = @"..\..\..\entradaDesafio.txt";

            if (!File.Exists(caminho))
            {
                Console.WriteLine("Arquivo entradaDesafio.txt não encontrado.");
                return;
            }

            using StreamReader leitor = new StreamReader(caminho);

            string[] primeira = leitor.ReadLine()!.Split();
            int V = int.Parse(primeira[0]);
            int A = int.Parse(primeira[1]);

            var grafo = new List<(int destino, int peso)>[V];

            for (int i = 0; i < V; i++)
                grafo[i] = new List<(int, int)>();

            for (int i = 0; i < A; i++)
            {
                string[] linha = leitor.ReadLine()!.Split();
                int origem = int.Parse(linha[0]);
                int destino = int.Parse(linha[1]);
                int peso = int.Parse(linha[2]);

                grafo[origem].Add((destino, peso));
            }

            string[] st = leitor.ReadLine()!.Split();
            int s = int.Parse(st[0]);
            int t = int.Parse(st[1]);

            int[] distOriginal = Dijkstra(grafo, s);
            int menorOriginal = distOriginal[t];

            Console.WriteLine();
            Console.WriteLine("Distância mínima original de " + s + " até " + t + ": " +
                (menorOriginal == INF ? "INFINITO" : menorOriginal.ToString()));

            int K = int.Parse(leitor.ReadLine()!);

            for (int i = 0; i < K; i++)
            {
                string[] bloqueio = leitor.ReadLine()!.Split();
                int u = int.Parse(bloqueio[0]);
                int v = int.Parse(bloqueio[1]);

                var removidas = grafo[u]
                    .Where(a => a.destino == v)
                    .ToList();

                grafo[u].RemoveAll(a => a.destino == v);

                int[] novaDist = Dijkstra(grafo, s);
                int nova = novaDist[t];

                Console.WriteLine();

                if (nova == menorOriginal)
                {
                    Console.WriteLine($"Remover ({u}, {v}) NÃO afeta o menor caminho.");
                }
                else
                {
                    Console.WriteLine($"Remover ({u}, {v}) AFETA o menor caminho.");
                    Console.WriteLine("Nova distância: " +
                        (nova == INF ? "INFINITO" : nova.ToString()));
                }

                foreach (var aresta in removidas)
                    grafo[u].Add(aresta);
            }

            Console.WriteLine();
            Console.WriteLine("Vértices em ordem crescente de distância a partir de " + s + ":");

            var ordem = Enumerable.Range(0, V)
                .OrderBy(v => distOriginal[v])
                .ToList();

            foreach (int v in ordem)
            {
                Console.WriteLine($"Vértice {v} - Distância: " +
                    (distOriginal[v] == INF ? "INFINITO" : distOriginal[v].ToString()));
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        private static int[] Dijkstra(List<(int destino, int peso)>[] grafo, int origem)
        {
            int V = grafo.Length;
            int[] dist = new int[V];

            for (int i = 0; i < V; i++)
                dist[i] = INF;

            dist[origem] = 0;

            var fila = new PriorityQueue<int, int>();
            fila.Enqueue(origem, 0);

            while (fila.Count > 0)
            {
                int atual = fila.Dequeue();

                foreach (var aresta in grafo[atual])
                {
                    int vizinho = aresta.destino;
                    int peso = aresta.peso;

                    if (dist[atual] + peso < dist[vizinho])
                    {
                        dist[vizinho] = dist[atual] + peso;
                        fila.Enqueue(vizinho, dist[vizinho]);
                    }
                }
            }

            return dist;
        }
    }
}