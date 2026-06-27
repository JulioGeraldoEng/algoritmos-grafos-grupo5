using System;
using ProjetoGrafos.Grafos;
using ProjetoGrafos.Dijkstra;
using System.Collections.Generic;

namespace ProjetoGrafos.MapaCidades
{
    public static class MapaCidades
    {
        public static void ConstruirEExecutar()
        {
            Console.Clear();
            string[] cidades =
            {
                "São Paulo",
                "Rio de Janeiro",
                "Belo Horizonte",
                "Brasília",
                "Goiânia",
                "Curitiba",
                "Porto Alegre",
                "Salvador",
                "Aracaju",
                "Fortaleza"
            };

            var grafo = new GrafoListaAdjacencia(10, direcionado: false);

            grafo.AdicionarAresta(0, 1, 400);
            grafo.AdicionarAresta(0, 2, 580);
            grafo.AdicionarAresta(0, 5, 400);
            grafo.AdicionarAresta(0, 6, 1130);
            grafo.AdicionarAresta(1, 2, 430);
            grafo.AdicionarAresta(1, 7, 1600);
            grafo.AdicionarAresta(2, 3, 715);
            grafo.AdicionarAresta(3, 4, 200);
            grafo.AdicionarAresta(3, 7, 1450);
            grafo.AdicionarAresta(4, 5, 1100);
            grafo.AdicionarAresta(5, 6, 730);
            grafo.AdicionarAresta(7, 8, 320);
            grafo.AdicionarAresta(7, 9, 1200);
            grafo.AdicionarAresta(8, 9, 750);

            MostrarMapaGrafo();

            Console.WriteLine("\n=== CIDADES DISPONÍVEIS ===");
            for (int i = 0; i < cidades.Length; i++)
            {
                Console.WriteLine($"{i} - {cidades[i]}");
            }

            int origem = LerCidade("\nDigite o número da cidade de partida: ", cidades.Length);
            int destino = LerCidade("Digite o número da cidade de destino: ", cidades.Length);

            var (dist, pred) = DijkstraHeap.Executar(grafo, origem);

            MostrarCaminho(origem, destino, dist, pred, cidades);

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        private static int LerCidade(string mensagem, int quantidadeCidades)
        {
            int cidade;

            while (true)
            {
                Console.Write(mensagem);
                bool valido = int.TryParse(Console.ReadLine(), out cidade);

                if (valido && cidade >= 0 && cidade < quantidadeCidades)
                    return cidade;

                Console.WriteLine("Cidade inválida. Digite um número da lista.");
            }
        }

        private static void MostrarCaminho(
            int origem,
            int destino,
            int[] dist,
            int[] pred,
            string[] cidades)
        {
            if (dist[destino] == int.MaxValue)
            {
                Console.WriteLine($"\nNão há caminho entre {cidades[origem]} e {cidades[destino]}.");
                return;
            }

            var caminho = new List<int>();
            int atual = destino;

            while (atual != -1)
            {
                caminho.Add(atual);
                atual = pred[atual];
            }

            caminho.Reverse();

            Console.WriteLine("\n=== MENOR CAMINHO ENCONTRADO ===");
            Console.WriteLine($"Origem: {cidades[origem]}");
            Console.WriteLine($"Destino: {cidades[destino]}");

            Console.WriteLine("\nRota:");
            Console.WriteLine(string.Join(" -> ", caminho.ConvertAll(i => cidades[i])));

            Console.WriteLine($"\nDistância total: {dist[destino]} km");
        }

        private static void MostrarMapaGrafo()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              MAPA DO GRAFO - CIDADES BRASILEIRAS             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            Console.WriteLine("  [Fortaleza]───────750km───────[Aracaju]");
            Console.WriteLine("       │                             │");
            Console.WriteLine("     1200km                       320km");
            Console.WriteLine("       │                             │");
            Console.WriteLine("       └─────────────┌        ┐──────┘");
            Console.WriteLine("       ┌─────────────|Salvador|       ");
            Console.WriteLine("       |             └        ┘       ");
            Console.WriteLine("       │                 │");
            Console.WriteLine("    1600km            1450km");
            Console.WriteLine("       │                 │");
            Console.WriteLine("  [Rio de Janeiro]   [Brasília]────200km────[Goiânia]");
            Console.WriteLine("       │                 │                       │");
            Console.WriteLine("       │                715km                  1100km");
            Console.WriteLine("      430km              │                       │");
            Console.WriteLine("       │            [Belo Horizonte]          [Curitiba]");
            Console.WriteLine("       │                 │                       │");
            Console.WriteLine("      580km──────────────┘                    730km");
            Console.WriteLine("       │                                         │");
            Console.WriteLine("  [São Paulo]────────400km───────────────────────┘");
            Console.WriteLine("       │");
            Console.WriteLine("     1130km");
            Console.WriteLine("       │");
            Console.WriteLine("  [Porto Alegre]");

        }
    }
}