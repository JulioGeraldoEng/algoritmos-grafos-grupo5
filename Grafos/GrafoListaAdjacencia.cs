using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetoGrafosGrupo5.Grafos
{
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

        public void ImprimirLista()
        {
            for (int i = 0; i < Vertices; i++)
            {
                Console.Write($"{i}: ");
                foreach (var (dest, peso) in adjacencias[i])
                    Console.Write($"->({dest},{peso}) ");
                Console.WriteLine();
            }
        }

        public static GrafoListaAdjacencia LerDoArquivo(string caminho)
        {
            if (!File.Exists(caminho))
                throw new FileNotFoundException($"Arquivo não encontrado: {caminho}");

            string[] linhas = File.ReadAllLines(caminho);
            if (linhas.Length < 2)
                throw new InvalidDataException("Arquivo vazio ou formato inválido.");

            string[] primeira = linhas[0].Split();
            if (primeira.Length < 2)
                throw new InvalidDataException("Primeira linha deve conter: V Direcionado (0 ou 1)");

            int V = int.Parse(primeira[0]);
            bool direcionado = int.Parse(primeira[1]) == 1;

            var grafo = new GrafoListaAdjacencia(V, direcionado);

            for (int i = 1; i < linhas.Length; i++)
            {
                string linha = linhas[i].Trim();
                if (string.IsNullOrEmpty(linha)) continue;
                string[] partes = linha.Split();
                if (partes.Length < 3)
                    throw new InvalidDataException($"Linha {i+1} inválida: esperado 'origem destino peso'");

                int origem = int.Parse(partes[0]);
                int destino = int.Parse(partes[1]);
                int peso = int.Parse(partes[2]);

                grafo.AdicionarAresta(origem, destino, peso);
            }

            return grafo;
        }
    }
}