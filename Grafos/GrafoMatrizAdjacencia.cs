using System;
using System.IO;

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

        public static void Executar()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine(" REPRESENTAÇÃO - MATRIZ DE ADJACÊNCIA");
            Console.WriteLine("==========================================\n");

            GrafoMatrizAdjacencia grafo = new GrafoMatrizAdjacencia(5);

            // Criação do grafo
            string caminho = @"..\..\..\matriz.txt";

            if (!File.Exists(caminho))
            {
                Console.WriteLine("Arquivo matriz.txt não encontrado.");
                Console.ReadKey();
                return;
            }

            string[] linhas = File.ReadAllLines(caminho);

            for (int i = 0; i < linhas.Length; i++)
            {
                string[] valores = linhas[i]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < valores.Length; j++)
                {
                    int peso = int.Parse(valores[j]);

                    // Evita adicionar a mesma aresta duas vezes
                    if (peso != 0 && j > i)
                    {
                        grafo.AdicionarAresta(i, j, peso);
                    }
                }
            }

            Console.WriteLine("Matriz de Adjacência:\n");
            grafo.Imprimir();

            Console.WriteLine("\n=== Informações do Grafo ===");

            int quantidadeVertices = grafo.Vertices;
            int quantidadeArestas = 0;

            for (int i = 0; i < grafo.Vertices; i++)
            {
                quantidadeArestas += grafo.ObterVizinhos(i).Length;
            }

            if (!grafo.Direcionado)
            {
                quantidadeArestas /= 2;
            }

            Console.WriteLine($"Número de vértices: {quantidadeVertices}");
            Console.WriteLine($"Número de arestas : {quantidadeArestas}");

            Console.WriteLine("\n=== Consulta de existência de aresta ===");

            int origemConsulta;

            while (true)
            {
                Console.Write($"Informe o vértice de origem (0 a {grafo.Vertices - 1}): ");

                if (int.TryParse(Console.ReadLine(), out origemConsulta) &&
                    origemConsulta >= 0 &&
                    origemConsulta < grafo.Vertices)
                {
                    break;
                }

                Console.WriteLine("Vértice inválido! Tente novamente.\n");
            }

            int destinoConsulta;

            while (true)
            {
                Console.Write($"Informe o vértice de destino (0 a {grafo.Vertices - 1}): ");

                if (int.TryParse(Console.ReadLine(), out destinoConsulta) &&
                    destinoConsulta >= 0 &&
                    destinoConsulta < grafo.Vertices)
                {
                    break;
                }

                Console.WriteLine("Vértice inválido! Tente novamente.\n");
            }

            Console.WriteLine(
                $"Existe aresta entre {origemConsulta} e {destinoConsulta}? " +
                $"{(grafo.ExisteAresta(origemConsulta, destinoConsulta) ? "Sim" : "Não")}"
            );

            Console.WriteLine("\n=== Vizinhos de um vértice ===");

            int verticeConsulta;

            while (true)
            {
                Console.Write($"Informe o vértice para listar os vizinhos (0 a {grafo.Vertices - 1}): ");

                if (int.TryParse(Console.ReadLine(), out verticeConsulta) &&
                    verticeConsulta >= 0 &&
                    verticeConsulta < grafo.Vertices)
                {
                    break;
                }

                Console.WriteLine("Vértice inválido! Tente novamente.\n");
            }

            int[] vizinhos = grafo.ObterVizinhos(verticeConsulta);

            if (vizinhos.Length == 0)
            {
                Console.WriteLine($"O vértice {verticeConsulta} não possui vizinhos.");
            }
            else
            {
                Console.Write($"Vizinhos do vértice {verticeConsulta}: ");

                foreach (int vizinho in vizinhos)
                {
                    Console.Write(vizinho + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}