using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetoGrafos.Desafio
{
    public static class DesafioDijkstra
    {
        private const int INF = 1000000000;

        public static void Executar()
        {
            Console.Clear();
            Console.WriteLine("\n=== DESAFIO BEECROWD 1148 – PAÍSES EM GUERRA ===");

            // Pergunta ao usuário qual modo de entrada deseja
            Console.WriteLine("Como deseja fornecer os dados?");
            Console.WriteLine("1 - Ler de um arquivo .txt");
            Console.WriteLine("2 - Digitar manualmente");
            Console.Write("Opção (1 ou 2): ");
            string? opcao = Console.ReadLine();

            if (opcao == "1")
            {
                // Modo arquivo
                Console.Write("Digite o caminho do arquivo (ou pressione Enter para usar 'entrada.txt'): ");
                string? caminho = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(caminho))
                    caminho = @"..\..\..\entrada.txt";
                if (!File.Exists(caminho))
                {
                    Console.WriteLine($"Arquivo '{caminho}' não encontrado.");
                    Console.WriteLine("Voltando ao menu...");
                    return;
                }

                Console.WriteLine($"Lendo dados do arquivo: {caminho}\n");
                using (var sr = new StreamReader(caminho))
                {
                    Console.SetIn(sr);
                    ProcessarEntrada();
                }
                // Restaura a entrada padrão
                Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            }
            else if (opcao == "2")
            {
                // Modo manual
                Console.WriteLine("Digite os dados manualmente (0 0 para encerrar):");
                ProcessarEntrada();
            }
            else
            {
                Console.WriteLine("Opção inválida. Voltando ao menu...");
                return;
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ProcessarEntrada()
        {
            while (true)
            {
                string? linha = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(linha)) break;
                string[] primeira = linha.Split();
                if (primeira.Length < 2) break;
                int N = int.Parse(primeira[0]);
                int E = int.Parse(primeira[1]);
                if (N == 0 && E == 0) break;

                // Listas de adjacência
                var adj = new List<List<int>>(N);
                var peso = new List<List<int>>(N);
                var adjT = new List<List<int>>(N);
                var pesoT = new List<List<int>>(N);
                for (int i = 0; i < N; i++)
                {
                    adj.Add(new List<int>());
                    peso.Add(new List<int>());
                    adjT.Add(new List<int>());
                    pesoT.Add(new List<int>());
                }

                for (int i = 0; i < E; i++)
                {
                    string[] aresta = Console.ReadLine().Split();
                    int X = int.Parse(aresta[0]) - 1;
                    int Y = int.Parse(aresta[1]) - 1;
                    int H = int.Parse(aresta[2]);
                    adj[X].Add(Y);
                    peso[X].Add(H);
                    adjT[Y].Add(X);
                    pesoT[Y].Add(H);
                }

                // Kosaraju - primeira DFS
                var visitado = new bool[N];
                var pilha = new Stack<int>();

                void Dfs1(int u)
                {
                    visitado[u] = true;
                    for (int i = 0; i < adj[u].Count; i++)
                    {
                        int v = adj[u][i];
                        if (!visitado[v]) Dfs1(v);
                    }
                    pilha.Push(u);
                }

                for (int i = 0; i < N; i++)
                    if (!visitado[i]) Dfs1(i);

                // Kosaraju - segunda DFS
                var comp = new int[N];
                for (int i = 0; i < N; i++) comp[i] = -1;
                int idComp = 0;

                void Dfs2(int u, int id)
                {
                    comp[u] = id;
                    for (int i = 0; i < adjT[u].Count; i++)
                    {
                        int v = adjT[u][i];
                        if (comp[v] == -1) Dfs2(v, id);
                    }
                }

                while (pilha.Count > 0)
                {
                    int u = pilha.Pop();
                    if (comp[u] == -1)
                    {
                        Dfs2(u, idComp);
                        idComp++;
                    }
                }

                // Condensa grafo
                var menorPeso = new int[idComp, idComp];
                for (int i = 0; i < idComp; i++)
                    for (int j = 0; j < idComp; j++)
                        menorPeso[i, j] = INF;

                for (int u = 0; u < N; u++)
                {
                    int cu = comp[u];
                    for (int i = 0; i < adj[u].Count; i++)
                    {
                        int v = adj[u][i];
                        int cv = comp[v];
                        int p = peso[u][i];
                        if (cu != cv && p < menorPeso[cu, cv])
                            menorPeso[cu, cv] = p;
                    }
                }

                var condAdj = new List<List<int>>(idComp);
                var condPeso = new List<List<int>>(idComp);
                for (int i = 0; i < idComp; i++)
                {
                    condAdj.Add(new List<int>());
                    condPeso.Add(new List<int>());
                }

                for (int i = 0; i < idComp; i++)
                    for (int j = 0; j < idComp; j++)
                        if (menorPeso[i, j] != INF)
                        {
                            condAdj[i].Add(j);
                            condPeso[i].Add(menorPeso[i, j]);
                        }

                // Dijkstra com vetor simples
                int DijkstraVetor(int origem, int destino)
                {
                    int C = condAdj.Count;
                    var dist = new int[C];
                    var processado = new bool[C];
                    for (int i = 0; i < C; i++) dist[i] = INF;
                    dist[origem] = 0;

                    for (int count = 0; count < C; count++)
                    {
                        int u = -1;
                        int menor = INF;
                        for (int i = 0; i < C; i++)
                            if (!processado[i] && dist[i] < menor)
                            {
                                menor = dist[i];
                                u = i;
                            }
                        if (u == -1 || u == destino) break;
                        processado[u] = true;

                        for (int i = 0; i < condAdj[u].Count; i++)
                        {
                            int v = condAdj[u][i];
                            int p = condPeso[u][i];
                            if (!processado[v] && dist[u] + p < dist[v])
                                dist[v] = dist[u] + p;
                        }
                    }
                    return dist[destino];
                }

                int K = int.Parse(Console.ReadLine());
                for (int q = 0; q < K; q++)
                {
                    string[] consulta = Console.ReadLine().Split();
                    int O = int.Parse(consulta[0]) - 1;
                    int D = int.Parse(consulta[1]) - 1;
                    int co = comp[O];
                    int cd = comp[D];

                    if (co == cd)
                        Console.WriteLine("0");
                    else
                    {
                        int ans = DijkstraVetor(co, cd);
                        if (ans >= INF)
                            Console.WriteLine("Nao e possivel entregar a carta");
                        else
                            Console.WriteLine(ans);
                    }
                }
                Console.WriteLine(); // linha em branco
            }
        }
    }
}