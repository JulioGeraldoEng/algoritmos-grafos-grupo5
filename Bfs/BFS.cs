using System;
using System.Collections.Generic;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Bfs
{
    public static class BFS
    {
        /// <summary>
        /// Executa a Busca em Largura a partir de um vértice origem.
        /// Retorna as distâncias e predecessores.
        /// </summary>
        public static (int[] distancias, int[] predecessores) Executar(GrafoListaAdjacencia grafo, int origem)
        {
            Console.Clear();
            int V = grafo.Vertices;
            int[] dist = new int[V];
            int[] pred = new int[V];
            bool[] visitado = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = -1;
                pred[i] = -1;
            }

            Queue<int> fila = new Queue<int>();
            dist[origem] = 0;
            visitado[origem] = true;
            fila.Enqueue(origem);

            while (fila.Count > 0)
            {
                int u = fila.Dequeue();   // cada vértice sai uma vez → O(V)
                foreach (var (v, _) in grafo.ObterVizinhos(u)) // total de iterações = O(A)
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