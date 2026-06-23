using System;
using ProjetoGrafos.Grafos;

namespace ProjetoGrafos.Dijkstra
{
    public static class DijkstraVetor
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

            for (int count = 0; count < V - 1; count++)
            {
                int u = -1;
                int menor = int.MaxValue;
                for (int i = 0; i < V; i++)
                {
                    if (!processado[i] && dist[i] < menor)
                    {
                        menor = dist[i];
                        u = i;
                    }
                }
                if (u == -1) break;

                processado[u] = true;
                foreach (var (v, peso) in grafo.ObterVizinhos(u))
                {
                    if (!processado[v] && dist[u] + peso < dist[v])
                    {
                        dist[v] = dist[u] + peso;
                        pred[v] = u;
                    }
                }
            }
            return (dist, pred);
        }
    }
}