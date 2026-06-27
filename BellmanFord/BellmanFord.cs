using System;
using System.Collections.Generic;
using ProjetoGrafos.Grafos;

namespace ProjetoGrafos.BellmanFord
{
    public static class BellmanFordAlgorithm
    {
        public static (int[] dist, int[] pred, bool temCicloNegativo) Executar(GrafoListaAdjacencia grafo, int fonte)
        {
            Console.Clear();
            int V = grafo.Vertices;
            var dist = new int[V];
            var pred = new int[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                pred[i] = -1;
            }
            dist[fonte] = 0;

            var arestas = new List<(int u, int v, int peso)>();
            for (int u = 0; u < V; u++)
                foreach (var (v, peso) in grafo.ObterVizinhos(u))
                    arestas.Add((u, v, peso));

            for (int i = 1; i <= V - 1; i++)
            {
                bool alterou = false;
                foreach (var (u, v, peso) in arestas)
                {
                    if (dist[u] != int.MaxValue && dist[u] + peso < dist[v])
                    {
                        dist[v] = dist[u] + peso;
                        pred[v] = u;
                        alterou = true;
                    }
                }
                if (!alterou) break;
            }

            bool cicloNegativo = false;
            foreach (var (u, v, peso) in arestas)
                if (dist[u] != int.MaxValue && dist[u] + peso < dist[v])
                {
                    cicloNegativo = true;
                    break;
                }

            return (dist, pred, cicloNegativo);
        }
    }
}