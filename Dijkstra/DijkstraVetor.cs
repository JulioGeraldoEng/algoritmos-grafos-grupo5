using System;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Dijkstra
{
    public static class DijkstraVetor
    {
        public static (int[] distancias, int[] predecessores) 
        Executar(GrafoListaAdjacencia grafo, int origem)
        {
            // Inicialização
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

            for (int count = 0; count < V - 1; count++)
            {
                int u = -1;
                int menor = int.MaxValue;
                for (int i = 0; i < V; i++)
                    if (!processado[i] && dist[i] < menor)
                    {
                        menor = dist[i];
                        u = i;
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