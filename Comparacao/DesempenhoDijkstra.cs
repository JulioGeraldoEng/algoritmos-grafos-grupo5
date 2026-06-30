using System;
using System.Diagnostics;
using ProjetoGrafosGrupo5.Grafos;
using ProjetoGrafosGrupo5.Dijkstra;

namespace ProjetoGrafosGrupo5.Comparacao
{
    public static class DesempenhoDijkstra
    {
        public static void Executar()
        {
            Console.WriteLine("\n=== COMPARAÇÃO DE DESEMPENHO: HEAP vs. VETOR ===");
            Console.WriteLine("Gerando grafos conexos e medindo tempos (média de 5 execuções)...\n");

            var cenarios = new (int V, int A)[]
            {
                (500, 5000),
                (1000, 10000),
                (2000, 40000),
                (5000, 50000),
                (500, 50000)
            };

            Console.WriteLine("| Vértices (V) | Arestas (A) | Heap (ms) | Vetor (ms) | Ganho do Heap |");
            Console.WriteLine("|--------------|-------------|-----------|------------|---------------|");

            foreach (var (V, A) in cenarios)
            {
                var grafo = GerarGrafoConexo(V, A);

                // Aquecimento
                AlgoritmoDijkstra.Executar(grafo, 0);
                DijkstraVetor.Executar(grafo, 0);

                int repeticoes = 5;
                double somaHeap = 0, somaVetor = 0;

                for (int i = 0; i < repeticoes; i++)
                {
                    var swHeap = Stopwatch.StartNew();
                    AlgoritmoDijkstra.Executar(grafo, 0);
                    swHeap.Stop();
                    somaHeap += swHeap.Elapsed.TotalMilliseconds;

                    var swVetor = Stopwatch.StartNew();
                    DijkstraVetor.Executar(grafo, 0);
                    swVetor.Stop();
                    somaVetor += swVetor.Elapsed.TotalMilliseconds;
                }

                double mediaHeap = somaHeap / repeticoes;
                double mediaVetor = somaVetor / repeticoes;
                double ganho = (mediaVetor - mediaHeap) / mediaVetor * 100;

                Console.WriteLine($"| {V,12} | {A,11} | {mediaHeap,9:F2} | {mediaVetor,10:F2} | {ganho,13:F1}% |");
            }

            Console.WriteLine("\nObs: Média de 5 execuções. Ganho positivo = Heap é mais rápido.");
        }

        private static GrafoListaAdjacencia GerarGrafoConexo(int V, int A)
        {
            if (A < V - 1) A = V - 1;
            var rand = new Random();
            var grafo = new GrafoListaAdjacencia(V, false);

            for (int i = 1; i < V; i++)
            {
                int peso = rand.Next(1, 101);
                grafo.AdicionarAresta(i - 1, i, peso);
            }

            int arestasAtuais = V - 1;
            int tentativas = 0;
            while (arestasAtuais < A && tentativas < A * 10)
            {
                int u = rand.Next(V);
                int v = rand.Next(V);
                if (u != v && !grafo.ExisteAresta(u, v))
                {
                    int peso = rand.Next(1, 101);
                    grafo.AdicionarAresta(u, v, peso);
                    arestasAtuais++;
                }
                tentativas++;
            }
            return grafo;
        }
    }
}