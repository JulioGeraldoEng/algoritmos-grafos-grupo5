using System;
using System.Collections.Generic;
using ProjetoGrafos.Grafos;

namespace ProjetoGrafos.Dijkstra
{
    public static class DijkstraHeap
    {
        public static (int[] dist, int[] pred) Executar(GrafoListaAdjacencia grafo, int fonte)
        {
            int V = grafo.Vertices;
            var dist = new int[V];
            var pred = new int[V];
            var processado = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                pred[i] = -1;
            }
            dist[fonte] = 0;

            var pq = new PriorityQueue<int, int>();
            pq.Enqueue(fonte, 0);

            while (pq.Count > 0)
            {
                int u = pq.Dequeue();
                if (processado[u]) continue;
                processado[u] = true;

                foreach (var (v, peso) in grafo.ObterVizinhos(u))
                {
                    if (!processado[v] && dist[u] + peso < dist[v])
                    {
                        dist[v] = dist[u] + peso;
                        pred[v] = u;
                        pq.Enqueue(v, dist[v]);
                    }
                }
            }
            return (dist, pred);
        }
    }
}