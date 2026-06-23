using System;
using System.Collections.Generic;

namespace ProjetoGrafos.Grafos
{
    
    /// Grafo representado por LISTA DE ADJACÊNCIA.
    /// Espaço: O(V + A). Consulta de aresta: O(grau(v)). Listagem: O(grau(v)).
    /// Ideal para grafos esparsos.
    /// 
    public class GrafoListaAdjacencia
    {
        private List<(int destino, int peso)>[] adjacencias;
        public int Vertices { get; private set; }
        public bool Direcionado { get; private set; }

        public GrafoListaAdjacencia(int vertices, bool direcionado = false)
        {
            Vertices = vertices;
            Direcionado = direcionado;
            adjacencias = new List<(int, int)>[vertices];
            for (int i = 0; i < vertices; i++)
                adjacencias[i] = new List<(int, int)>();
        }

        public void AdicionarAresta(int origem, int destino, int peso = 1)
        {
            if (origem < 0 || origem >= Vertices || destino < 0 || destino >= Vertices)
                throw new ArgumentOutOfRangeException("Vértice inválido.");
            adjacencias[origem].Add((destino, peso));
            if (!Direcionado)
                adjacencias[destino].Add((origem, peso));
        }

        public bool ExisteAresta(int origem, int destino)
        {
            foreach (var (dest, _) in adjacencias[origem])
                if (dest == destino) return true;
            return false;
        }

        public int ObterPeso(int origem, int destino)
        {
            foreach (var (dest, peso) in adjacencias[origem])
                if (dest == destino) return peso;
            throw new ArgumentException("Aresta não encontrada.");
        }

        public IEnumerable<(int destino, int peso)> ObterVizinhos(int vertice)
        {
            return adjacencias[vertice];
        }

        public void Imprimir()
        {
            for (int i = 0; i < Vertices; i++)
            {
                Console.Write($"{i}: ");
                foreach (var (dest, peso) in adjacencias[i])
                    Console.Write($"->({dest},{peso}) ");
                Console.WriteLine();
            }
        }
    }
}