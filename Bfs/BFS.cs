using System;
using System.Collections.Generic;
using ProjetoGrafos.Grafos;

namespace ProjetoGrafos.Bfs
{
    public static class BFS
    {
        public static (int[] distancias, int[] predecessores) Executar(GrafoListaAdjacencia grafo, int fonte)
        {
            int V = grafo.Vertices;
            var dist = new int[V];
            var pred = new int[V];
            var visitado = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = -1;
                pred[i] = -1;
            }

            var fila = new Queue<int>();
            dist[fonte] = 0;
            visitado[fonte] = true;
            fila.Enqueue(fonte);

            while (fila.Count > 0)
            {
                int u = fila.Dequeue();
                foreach (var (v, _) in grafo.ObterVizinhos(u))
                {
                    if (!visitado[v])
                    {
                        visitado[v] = true;
                        dist[v] = dist[u] + 1;
                        pred[v] = u;
                        fila.Enqueue(v);
                    }
                }
            }
            return (dist, pred);
        }
    }
}