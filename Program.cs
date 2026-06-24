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
            Console.WriteLine("===== PROJETO GRAFOS - .NET 8 =====\n");
            bool sair = false;
            while (!sair)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Mapa de Cidades (Dijkstra)");
                Console.WriteLine("2 - Desafio de Programação Competitiva");
                Console.WriteLine("3 - Testes BFS, DFS e Bellman-Ford");
                Console.WriteLine("4 - Testar Conectividade, Ciclos e Componentes");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
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
                    case "4":
                        TestarConectividadeCiclosComponentes();
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

        static void TestarConectividadeCiclosComponentes()
        {
            Console.WriteLine("=== TESTE DE CONECTIVIDADE, CICLOS E COMPONENTES ===");

            // Grafo de teste com 6 vértices (não-direcionado) – já usado na opção 3
            var grafo = new GrafoListaAdjacencia(6, direcionado: false);
            grafo.AdicionarAresta(0, 1);
            grafo.AdicionarAresta(0, 2);
            grafo.AdicionarAresta(1, 3);
            grafo.AdicionarAresta(2, 4);
            grafo.AdicionarAresta(3, 5);
            grafo.AdicionarAresta(4, 5);

            Console.WriteLine("\nGrafo de teste (6 vértices):");
            grafo.Imprimir();

            // -------------------- CONECTIVIDADE (BFS) --------------------
            var (dist, _) = BFS.Executar(grafo, 0);
            bool conectado = true;
            for (int i = 0; i < dist.Length; i++)
                if (dist[i] == -1) { conectado = false; break; }

            Console.WriteLine($"\n[1] Conectividade a partir do vértice 0: {(conectado ? "SIM (grafo é conexo)" : "NÃO (há vértices inalcançáveis)")}");

            // -------------------- CICLOS (DFS) --------------------
            // Para detectar ciclos, executamos um DFS e verificamos se há aresta de retorno.
            // Vamos usar o método DFS existente, mas ele não retorna ciclos explicitamente.
            // Podemos fazer uma função simples que usa cores e detecta back edges.
            bool temCiclo = false;
            int V = grafo.Vertices;
            int[] cor = new int[V]; // 0=branco, 1=cinza, 2=preto

            void DfsCiclo(int u)
            {
                cor[u] = 1; // cinza
                foreach (var (v, _) in grafo.ObterVizinhos(u))
                {
                    if (cor[v] == 1) // vizinho cinza -> aresta de retorno -> ciclo
                    {
                        temCiclo = true;
                    }
                    else if (cor[v] == 0)
                    {
                        DfsCiclo(v);
                    }
                }
                cor[u] = 2; // preto
            }

            for (int i = 0; i < V; i++)
                if (cor[i] == 0)
                    DfsCiclo(i);

            Console.WriteLine($"[2] Ciclos no grafo: {(temCiclo ? "SIM (detectado)" : "NÃO")}");

            // -------------------- COMPONENTES CONEXAS (BFS) --------------------
            bool[] visitado = new bool[V];
            int componentes = 0;
            for (int i = 0; i < V; i++)
            {
                if (!visitado[i])
                {
                    componentes++;
                    // Executa BFS a partir de i para marcar toda a componente
                    var fila = new Queue<int>();
                    visitado[i] = true;
                    fila.Enqueue(i);
                    while (fila.Count > 0)
                    {
                        int u = fila.Dequeue();
                        foreach (var (v, _) in grafo.ObterVizinhos(u))
                        {
                            if (!visitado[v])
                            {
                                visitado[v] = true;
                                fila.Enqueue(v);
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"[3] Número de componentes conexas: {componentes}");

            // -------------------- TESTE COM MAPA DE CIDADES (opcional) --------------------
            Console.WriteLine("\n--- Teste com o Mapa de Cidades ---");
            var grafoCidades = new GrafoListaAdjacencia(10, direcionado: false);
            grafoCidades.AdicionarAresta(0, 1, 400);
            grafoCidades.AdicionarAresta(0, 2, 580);
            grafoCidades.AdicionarAresta(0, 5, 400);
            grafoCidades.AdicionarAresta(1, 2, 430);
            grafoCidades.AdicionarAresta(2, 3, 715);
            grafoCidades.AdicionarAresta(3, 4, 200);
            grafoCidades.AdicionarAresta(4, 5, 1100);
            grafoCidades.AdicionarAresta(5, 6, 730);
            grafoCidades.AdicionarAresta(6, 7, 3000);
            grafoCidades.AdicionarAresta(7, 8, 320);
            grafoCidades.AdicionarAresta(7, 9, 1200);
            grafoCidades.AdicionarAresta(8, 9, 750);
            grafoCidades.AdicionarAresta(1, 7, 1600);
            grafoCidades.AdicionarAresta(3, 7, 1450);
            grafoCidades.AdicionarAresta(0, 6, 1130);

            var (distCid, _) = BFS.Executar(grafoCidades, 0);
            bool conectadoCid = true;
            for (int i = 0; i < distCid.Length; i++)
                if (distCid[i] == -1) { conectadoCid = false; break; }
            Console.WriteLine($"Mapa de cidades é conexo? {(conectadoCid ? "SIM" : "NÃO")}");

            // Detectar ciclos no mapa (usando o mesmo método DFS)
            int Vc = grafoCidades.Vertices;
            int[] corC = new int[Vc];
            bool cicloCid = false;
            void DfsCicloCid(int u)
            {
                corC[u] = 1;
                foreach (var (v, _) in grafoCidades.ObterVizinhos(u))
                {
                    if (corC[v] == 1) cicloCid = true;
                    else if (corC[v] == 0) DfsCicloCid(v);
                }
                corC[u] = 2;
            }
            for (int i = 0; i < Vc; i++)
                if (corC[i] == 0) DfsCicloCid(i);
            Console.WriteLine($"Mapa de cidades possui ciclos? {(cicloCid ? "SIM" : "NÃO")}");

            // Componentes do mapa
            bool[] visitadoC = new bool[Vc];
            int compC = 0;
            for (int i = 0; i < Vc; i++)
            {
                if (!visitadoC[i])
                {
                    compC++;
                    var fila = new Queue<int>();
                    visitadoC[i] = true;
                    fila.Enqueue(i);
                    while (fila.Count > 0)
                    {
                        int u = fila.Dequeue();
                        foreach (var (v, _) in grafoCidades.ObterVizinhos(u))
                        {
                            if (!visitadoC[v])
                            {
                                visitadoC[v] = true;
                                fila.Enqueue(v);
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Número de componentes conexas no mapa: {compC}");
        }
    }
}