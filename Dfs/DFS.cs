using System;
using ProjetoGrafos.Grafos;

namespace ProjetoGrafos.Dfs
{
    public static class DFS
    {
        public static (int[] d, int[] f, int[] pred) Executar(GrafoListaAdjacencia grafo)
        {
            int V = grafo.Vertices;
            var d = new int[V];
            var f = new int[V];
            var pred = new int[V];
            var cor = new int[V];
            int tempo = 0;

            for (int i = 0; i < V; i++)
            {
                d[i] = -1; f[i] = -1; pred[i] = -1; cor[i] = 0;
            }

            void Visitar(int u)
            {
                cor[u] = 1;
                d[u] = ++tempo;
                foreach (var (v, _) in grafo.ObterVizinhos(u))
                {
                    if (cor[v] == 0)
                    {
                        pred[v] = u;
                        Visitar(v);
                    }
                }
                cor[u] = 2;
                f[u] = ++tempo;
            }

            for (int u = 0; u < V; u++)
                if (cor[u] == 0)
                    Visitar(u);

            return (d, f, pred);
        }
    }
}