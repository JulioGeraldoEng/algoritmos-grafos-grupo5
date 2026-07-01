using System;
using System.Collections.Generic;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.BellmanFord
{
    public static class BellmanFord
    {
        public static (int[] distancias, int[] predecessores) Executar(GrafoListaAdjacencia grafo, int origem)
        {
            int V = grafo.Vertices;
            int[] dist = new int[V];
            int[] pred = new int[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                pred[i] = -1;
            }

            dist[origem] = 0;

            // Relaxa todas as arestas V - 1 vezes
            for (int i = 0; i < V - 1; i++)
            {
                for (int u = 0; u < V; u++)
                {
                    foreach (var (v, peso) in grafo.ObterVizinhos(u))
                    {
                        if (dist[u] != int.MaxValue && dist[u] + peso < dist[v])
                        {
                            dist[v] = dist[u] + peso;
                            pred[v] = u;
                        }
                    }
                }
            }

            return (dist, pred);
        }
    }
}