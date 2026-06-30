using System;
using System.IO;

namespace ProjetoGrafosGrupo5.Grafos
{
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

        public void ImprimirMatriz()
        {
            for (int i = 0; i < Vertices; i++)
            {
                for (int j = 0; j < Vertices; j++)
                    Console.Write($"{matriz[i, j],3} ");
                Console.WriteLine();
            }
        }

        public static GrafoMatrizAdjacencia LerDoArquivo(string caminho)
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

            var grafo = new GrafoMatrizAdjacencia(V, direcionado);

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