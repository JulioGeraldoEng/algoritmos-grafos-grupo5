using System;
using ProjetoGrafos.MapaCidades;
using ProjetoGrafos.Desafio;
using ProjetoGrafos.Grafos;
using ProjetoGrafos.Dijkstra;
using ProjetoGrafos.Bfs;
using ProjetoGrafos.Dfs;
using ProjetoGrafos.BellmanFord;

namespace ProjetoGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            bool sair = false;
            while (!sair)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                 MENU PRINCIPAL");
                Console.ResetColor();

                Console.WriteLine("────────────────────────────────────────────────────────");
                Console.WriteLine("  Escolha uma das opções abaixo:");
                Console.WriteLine("  1  -> Mapa de Cidades (Dijkstra)");
                Console.WriteLine("  2  -> Desafio de Programação Competitiva");
                Console.WriteLine("  3  -> Testes BFS, DFS e Bellman-Ford");
                Console.WriteLine("────────────────────────────────────────────────────────");


                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  9  -> Limpar Console");
                Console.ResetColor();

                Console.Write(new string(' ', 20));

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0  -> Sair");
                Console.ResetColor();

                Console.WriteLine("────────────────────────────────────────────────────────");
                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MapaCidades.MapaCidades.ConstruirEExecutar();
                        break;
                    case "2":
                        DesafioDijkstra.Executar();
                        break;
                    case "3":
                        TestesAdicionais();
                        break;
                    case "0":
                        sair = true;
                        Console.WriteLine("Saindo...");
                        break;
                    case "9":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void TestesAdicionais()
        {
            Console.WriteLine("=== TESTES BFS, DFS E BELLMAN-FORD ===");
            var grafoTeste = new GrafoListaAdjacencia(6, direcionado: false);
            grafoTeste.AdicionarAresta(0, 1, 1);
            grafoTeste.AdicionarAresta(0, 2, 1);
            grafoTeste.AdicionarAresta(1, 3, 1);
            grafoTeste.AdicionarAresta(2, 4, 1);
            grafoTeste.AdicionarAresta(3, 5, 1);
            grafoTeste.AdicionarAresta(4, 5, 1);

            var (distBFS, predBFS) = BFS.Executar(grafoTeste, 0);
            Console.WriteLine($"BFS - Distâncias de 0: [{string.Join(", ", distBFS)}]");

            var (d, f, predDFS) = DFS.Executar(grafoTeste);
            Console.WriteLine($"DFS - Descoberta (d): [{string.Join(", ", d)}]");
            Console.WriteLine($"DFS - Finalização (f): [{string.Join(", ", f)}]");

            // Agora usando o nome corrigido da classe
            var (distBF, predBF, ciclo) = BellmanFordAlgorithm.Executar(grafoTeste, 0);
            Console.WriteLine($"Bellman-Ford - Distâncias: [{string.Join(", ", distBF)}]");
            Console.WriteLine($"Ciclo negativo? {ciclo}");
        }
    }
}