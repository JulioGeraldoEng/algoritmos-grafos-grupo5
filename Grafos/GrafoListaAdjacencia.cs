using System;
using System.Collections.Generic;
using System.IO;

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

        public static void Executar()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine(" REPRESENTAÇÃO - LISTA DE ADJACÊNCIA");
            Console.WriteLine("==========================================\n");

            string caminho = @"..\..\..\lista.txt";

            if (!File.Exists(caminho))
            {
                Console.WriteLine("Arquivo lista.txt não encontrado.");
                Console.ReadKey();
                return;
            }

            string[] linhas = File.ReadAllLines(caminho);

            if (linhas.Length == 0)
            {
                Console.WriteLine("O arquivo lista.txt está vazio.");
                Console.ReadKey();
                return;
            }

            GrafoListaAdjacencia grafo = new GrafoListaAdjacencia(linhas.Length);

            foreach (string linha in linhas)
            {
                string[] partes = linha.Split(':');

                if (partes.Length != 2)
                {
                    Console.WriteLine($"Linha inválida no arquivo: {linha}");
                    Console.ReadKey();
                    return;
                }

                if (!int.TryParse(partes[0].Trim(), out int origem) ||
                    origem < 0 ||
                    origem >= grafo.Vertices)
                {
                    Console.WriteLine($"Vértice de origem inválido na linha: {linha}");
                    Console.ReadKey();
                    return;
                }

                string[] vizinhos = partes[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string vizinho in vizinhos)
                {
                    if (!int.TryParse(vizinho, out int destino) ||
                        destino < 0 ||
                        destino >= grafo.Vertices)
                    {
                        Console.WriteLine($"Vértice destino inválido na linha: {linha}");
                        Console.ReadKey();
                        return;
                    }

                    if (!grafo.ExisteAresta(origem, destino))
                    {
                        grafo.AdicionarAresta(origem, destino);
                    }
                }
            }

            Console.WriteLine("Lista de Adjacência:\n");
            grafo.Imprimir();

            Console.WriteLine("\n=== Informações do Grafo ===");

            int quantidadeArestas = 0;

            for (int i = 0; i < grafo.Vertices; i++)
            {
                foreach (var _ in grafo.ObterVizinhos(i))
                    quantidadeArestas++;
            }

            if (!grafo.Direcionado)
                quantidadeArestas /= 2;

            Console.WriteLine($"Número de vértices: {grafo.Vertices}");
            Console.WriteLine($"Número de arestas : {quantidadeArestas}");

            Console.WriteLine("\n=== Consulta de existência de aresta ===");

            int origemConsulta;

            while (true)
            {
                Console.Write($"Informe o vértice de origem (0 a {grafo.Vertices - 1}): ");

                if (int.TryParse(Console.ReadLine(), out origemConsulta) &&
                    origemConsulta >= 0 &&
                    origemConsulta < grafo.Vertices)
                    break;

                Console.WriteLine("Vértice inválido! Tente novamente.\n");
            }

            int destinoConsulta;

            while (true)
            {
                Console.Write($"Informe o vértice de destino (0 a {grafo.Vertices - 1}): ");

                if (int.TryParse(Console.ReadLine(), out destinoConsulta) &&
                    destinoConsulta >= 0 &&
                    destinoConsulta < grafo.Vertices)
                    break;

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
                    break;

                Console.WriteLine("Vértice inválido! Tente novamente.\n");
            }

            bool possuiVizinhos = false;

            Console.Write($"Vizinhos do vértice {verticeConsulta}: ");

            foreach (var (destino, peso) in grafo.ObterVizinhos(verticeConsulta))
            {
                Console.Write($"({destino}, peso={peso}) ");
                possuiVizinhos = true;
            }

            if (!possuiVizinhos)
                Console.Write("Nenhum");

            Console.WriteLine();

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}