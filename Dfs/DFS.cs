using System;
using System.Collections.Generic;
using ProjetoGrafosGrupo5.Grafos;

namespace ProjetoGrafosGrupo5.Dfs
{
    public class ArestaClassificada
    {
        public int Origem { get; set; }
        public int Destino { get; set; }
        public string Tipo { get; set; } = string.Empty; // "Árvore", "Retorno", "Avanço", "Cruzamento"
    }

    public static class DFS
    {
        public static (int[] d, int[] f, int[] pred, List<ArestaClassificada> arestasClassificadas)
            Executar(GrafoListaAdjacencia grafo)
        {
            int V = grafo.Vertices;
            int[] d = new int[V]; // tempo de descoberta
            int[] f = new int[V]; // tempo de finalização
            int[] pred = new int[V]; // predecessor
            int[] cor = new int[V]; // 0=branco, 1=cinza, 2=preto
            int tempo = 0;
            var arestasClassificadas = new List<ArestaClassificada>();

            for (int i = 0; i < V; i++)
            {
                d[i] = -1; f[i] = -1; pred[i] = -1; cor[i] = 0;
            }

            void Visitar(int u)
            {
                cor[u] = 1; // cinza
                d[u] = ++tempo;
                foreach (var (v, peso) in grafo.ObterVizinhos(u))
                {
                    var aresta = new ArestaClassificada { Origem = u, Destino = v };

                    if (cor[v] == 0) // branco
                    {
                        // Aresta de árvore
                        aresta.Tipo = "Árvore";
                        pred[v] = u;
                        Visitar(v);
                    }
                    else if (cor[v] == 1) // cinza
                    {
                        // Aresta de retorno
                        aresta.Tipo = "Retorno";
                    }
                    else if (cor[v] == 2) // preto
                    {
                        // Verifica se é avanço ou cruzamento
                        // Se d[u] < d[v], então v foi descoberto depois de u (descendente) -> avanço
                        // Caso contrário, é cruzamento
                        if (d[u] < d[v])
                            aresta.Tipo = "Avanço";
                        else
                            aresta.Tipo = "Cruzamento";
                    }
                    arestasClassificadas.Add(aresta);
                }
                cor[u] = 2; // preto
                f[u] = ++tempo;
            }

            for (int u = 0; u < V; u++)
                if (cor[u] == 0)
                    Visitar(u);

            return (d, f, pred, arestasClassificadas);
        }
    }
}