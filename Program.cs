using System;
using ProjetoGrafosGrupo5.Grafos;
using ProjetoGrafosGrupo5.Bfs;
using ProjetoGrafosGrupo5.Dfs;
using ProjetoGrafosGrupo5.Dijkstra;
using ProjetoGrafosGrupo5.MapaCidades;
using ProjetoGrafosGrupo5.Comparacao;

namespace ProjetoGrafosGrupo5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== PROJETO GRAFOS - .NET 8 =====\n");
            bool sair = false;
            while (!sair)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Matriz de Adjacência");
                Console.WriteLine("2 - Lista de Adjacência");
                Console.WriteLine("3 - Busca em Largura (BFS)");
                Console.WriteLine("4 - Busca em Profundidade (DFS)");
                Console.WriteLine("5 - Dijkstra (caminho mínimo)");
                Console.WriteLine("6 - Mapa de Cidades (Dijkstra)");   
                Console.WriteLine("7 - Comparar desempenho (Heap vs Vetor)");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ExecutarMatriz();
                        break;
                    case "2":
                        ExecutarLista();
                        break;
                    case "3":
                        ExecutarBFS();
                        break;
                    case "4":
                        ExecutarDFS();
                        break;
                    case "5":
                        ExecutarDijkstra();
                        break;
                    case "6":
                        ProjetoGrafosGrupo5.MapaCidades.MapaCidades.ConstruirEExecutar();
                        break;
                    case "7":
                        DesempenhoDijkstra.Executar();
                        break;
                    case "0":
                        sair = true;
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // ============================================================
        // Função para escolher o arquivo (lista fixa com 12 arquivos)
        // ============================================================
       static string EscolherArquivo()
        {
            Console.WriteLine("\nArquivos disponíveis na pasta 'Grafos/':");
            Console.WriteLine("  1  - 1_grafo_denso.txt");
            Console.WriteLine("  2  - 2_grafo_esparso.txt");
            Console.WriteLine("  3  - 3_grafo_direcionado.txt");
            Console.WriteLine("  4  - 4_grafo_nao_direcionado.txt");
            Console.WriteLine("  5  - 5_grafo_com_ciclos.txt");
            Console.WriteLine("  6  - 6_grafo_sem_ciclos.txt");
            Console.WriteLine("  7  - 7_grafo_pesos_negativos.txt");
            Console.WriteLine("  8  - 8_grafo_para_dijkstra.txt");
            Console.WriteLine("  9  - 9_grafo_desconexo.txt");
            Console.WriteLine("  10 - 10_grafo_para_bfs_dfs.txt");
            Console.WriteLine("  11 - 11_grafo_grande_denso.txt");
            Console.WriteLine("  12 - 12_grafo_direcionado_com_ciclo.txt");
            Console.WriteLine("  13 - teste1_entrada.txt");
            Console.WriteLine("  14 - teste2_entrada.txt");
            Console.WriteLine("  15 - teste3_entrada.txt");
            Console.WriteLine("  16 - Outro (digitar caminho)");
            Console.Write("Escolha o número (1-16): ");
            string? escolha = Console.ReadLine();

            string caminho = "";
            switch (escolha)
            {
                case "1": caminho = "Grafos/1_grafo_denso.txt"; break;
                case "2": caminho = "Grafos/2_grafo_esparso.txt"; break;
                case "3": caminho = "Grafos/3_grafo_direcionado.txt"; break;
                case "4": caminho = "Grafos/4_grafo_nao_direcionado.txt"; break;
                case "5": caminho = "Grafos/5_grafo_com_ciclos.txt"; break;
                case "6": caminho = "Grafos/6_grafo_sem_ciclos.txt"; break;
                case "7": caminho = "Grafos/7_grafo_pesos_negativos.txt"; break;
                case "8": caminho = "Grafos/8_grafo_para_dijkstra.txt"; break;
                case "9": caminho = "Grafos/9_grafo_desconexo.txt"; break;
                case "10": caminho = "Grafos/10_grafo_para_bfs_dfs.txt"; break;
                case "11": caminho = "Grafos/11_grafo_grande_denso.txt"; break;
                case "12": caminho = "Grafos/12_grafo_direcionado_com_ciclo.txt"; break;
                case "13": caminho = "Grafos/teste1_entrada.txt"; break;
                case "14": caminho = "Grafos/teste2_entrada.txt"; break;
                case "15": caminho = "Grafos/teste3_entrada.txt"; break;
                case "16":
                    Console.Write("Digite o caminho completo do arquivo: ");
                    string? digitado = Console.ReadLine();
                    caminho = string.IsNullOrWhiteSpace(digitado) ? "Grafos/matrizadjacencia.txt" : digitado;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Usando padrão 'Grafos/1_grafo_denso.txt'.");
                    caminho = "Grafos/1_grafo_denso.txt";
                    break;
            }
            return caminho;
        }

        // ============================================================
        // Opção 1 – Matriz de Adjacência 
        // ============================================================
        static void ExecutarMatriz()
        {
            string caminho = EscolherArquivo();
            try
            {
                var grafo = GrafoMatrizAdjacencia.LerDoArquivo(caminho);
                AnalisadorMatriz.Analisar(grafo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        // ============================================================
        // Opção 2 – Lista de Adjacência
        // ============================================================
        static void ExecutarLista()
        {
            string caminho = EscolherArquivo();
            try
            {
                var grafo = GrafoListaAdjacencia.LerDoArquivo(caminho);
                AnalisadorLista.Analisar(grafo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        // ============================================================
        // Opção 3 – BFS
        // ============================================================
        static void ExecutarBFS()
        {
            string caminho = EscolherArquivo();
            try
            {
                var grafo = GrafoListaAdjacencia.LerDoArquivo(caminho);
                Console.Write("Digite o vértice de origem (0 a {0}): ", grafo.Vertices - 1);
                string? entrada = Console.ReadLine();
                if (!int.TryParse(entrada, out int origem) || origem < 0 || origem >= grafo.Vertices)
                {
                    Console.WriteLine("Origem inválida. Usando 0.");
                    origem = 0;
                }

                var (distancias, predecessores) = BFS.Executar(grafo, origem);
                AnalisadorBFS.Analisar(grafo, origem, distancias, predecessores);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        // ============================================================
        // Opção 4 – DFS
        // ============================================================
        static void ExecutarDFS()
        {
            string caminho = EscolherArquivo();
            try
            {
                var grafo = GrafoListaAdjacencia.LerDoArquivo(caminho);
                var (d, f, pred, arestas) = DFS.Executar(grafo);
                AnalisadorDFS.Analisar(grafo, d, f, pred, arestas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        // ============================================================
        // Opção 5 – Dijkstra
        // ============================================================
        static void ExecutarDijkstra()
        {
            string caminho = EscolherArquivo();
            try
            {
                var grafo = GrafoListaAdjacencia.LerDoArquivo(caminho);
                Console.Write("Digite o vértice de origem (0 a {0}): ", grafo.Vertices - 1);
                string? entrada = Console.ReadLine();
                if (!int.TryParse(entrada, out int origem) || origem < 0 || origem >= grafo.Vertices)
                {
                    Console.WriteLine("Origem inválida. Usando 0.");
                    origem = 0;
                }

                // Usando variável intermediária para evitar CS8130
                var resultado = ProjetoGrafosGrupo5.Dijkstra.AlgoritmoDijkstra.Executar(grafo, origem);
                int[] distancias = resultado.distancias;
                int[] predecessores = resultado.predecessores;

                AnalisadorDijkstra.Analisar(grafo, origem, distancias, predecessores);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

    }
}