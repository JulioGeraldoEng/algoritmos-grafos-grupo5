using System;
using System.Collections.Generic;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Dijkstra
{
    public static class AlgoritmoDijkstra
    {
        public static (int[] distancias, int[] predecessores) Executar(GrafoListaAdjacencia grafo, int origem)
        {
            int V = grafo.Vertices;
            int[] dist = new int[V];
            int[] pred = new int[V];
            bool[] processado = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                pred[i] = -1;
            }
            dist[origem] = 0;

            var pq = new PriorityQueue<int, int>();
            pq.Enqueue(origem, 0);   // O(1) ou O(log V) – desprezível

            while (pq.Count > 0)
            {
                int u = pq.Dequeue();   // EXTRACT-MIN: O(log V) → V vezes
                if (processado[u]) continue;
                processado[u] = true;

                foreach (var (v, peso) in grafo.ObterVizinhos(u))  // A arestas no total
                {
                    if (!processado[v] && dist[u] + peso < dist[v])
                    {
                        dist[v] = dist[u] + peso;
                        pred[v] = u;
                        pq.Enqueue(v, dist[v]);  // DECREASE-KEY: O(log V) → A vezes
                    }
                }
            }

            return (dist, pred);
        }
    }
}