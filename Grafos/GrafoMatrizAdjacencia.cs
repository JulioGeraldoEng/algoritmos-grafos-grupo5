using System;

namespace ProjetoGrafos.Grafos
{
    
    /// Grafo representado por MATRIZ DE ADJACÊNCIA.
    /// Complexidade de espaço: O(V²).
    /// Consulta de aresta: O(1). Listagem de vizinhos: O(V).
    /// Ideal para grafos densos.
    /// 
    public class GrafoMatrizAdjacencia
    {
        private int[,] matriz;
        public int Vertices { get; private set; }
        public bool Direcionado { get; private set; }

        public GrafoMatrizAdjacencia(int vertices, bool direcionado = false)
        {
            Vertices = vertices;
            Direcionado = direcionado;
            matriz = new int[vertices, vertices];
        }

        public void AdicionarAresta(int origem, int destino, int peso = 1)
        {
            if (origem < 0 || origem >= Vertices || destino < 0 || destino >= Vertices)
                throw new ArgumentOutOfRangeException("Vértice inválido.");
            matriz[origem, destino] = peso;
            if (!Direcionado)
                matriz[destino, origem] = peso;
        }

        public bool ExisteAresta(int origem, int destino)
        {
            return matriz[origem, destino] != 0;
        }

        public int ObterPeso(int origem, int destino)
        {
            return matriz[origem, destino];
        }

        public int[] ObterVizinhos(int vertice)
        {
            int[] vizinhos = new int[Vertices];
            int count = 0;
            for (int i = 0; i < Vertices; i++)
                if (matriz[vertice, i] != 0)
                    vizinhos[count++] = i;
            Array.Resize(ref vizinhos, count);
            return vizinhos;
        }

        public void Imprimir()
        {
            for (int i = 0; i < Vertices; i++)
            {
                for (int j = 0; j < Vertices; j++)
                    Console.Write($"{matriz[i, j],3} ");
                Console.WriteLine();
            }
        }
    }
}