using System;
using ProjetoGrafos.Grafos;
using ProjetoGrafos.Dijkstra;

namespace ProjetoGrafos.MapaCidades
{
    public static class MapaCidades
    {
        private const int Destino = 3;

        public static void ConstruirEExecutar()
        {
            // Cria grafo com 10 vértices (0 a 9) e não direcionado.
            var grafo = new GrafoListaAdjacencia(10, direcionado: false);

            // Adiciona arestas com distâncias reais (km) entre as cidades brasileiras.
            grafo.AdicionarAresta(0, 1, 400);   // São Paulo -> Rio de Janeiro
            grafo.AdicionarAresta(0, 2, 580);   // São Paulo -> Belo Horizonte
            grafo.AdicionarAresta(0, 5, 400);   // São Paulo -> Curitiba
            grafo.AdicionarAresta(1, 2, 430);   // Rio de Janeiro -> Belo Horizonte
            grafo.AdicionarAresta(2, 3, 715);   // Belo Horizonte -> Brasília
            grafo.AdicionarAresta(3, 4, 200);   // Brasília -> Goiânia
            grafo.AdicionarAresta(4, 5, 1100);  // Goiânia -> Curitiba
            grafo.AdicionarAresta(5, 6, 730);   // Curitiba -> Porto Alegre
            grafo.AdicionarAresta(6, 7, 3000);  // Porto Alegre -> Salvador (BR-116)
            grafo.AdicionarAresta(7, 8, 320);   // Salvador -> Aracaju
            grafo.AdicionarAresta(7, 9, 1200);  // Salvador -> Fortaleza
            grafo.AdicionarAresta(8, 9, 750);   // Aracaju -> Fortaleza
            grafo.AdicionarAresta(1, 7, 1600);  // Rio de Janeiro -> Salvador
            grafo.AdicionarAresta(3, 7, 1450);  // Brasília -> Salvador
            grafo.AdicionarAresta(0, 6, 1130);  // São Paulo -> Porto Alegre

            Console.WriteLine("=== MAPA DE CIDADES ===");
            grafo.Imprimir();

            int fonte = 0;
            var (dist, pred) = DijkstraHeap.Executar(grafo, fonte);

            Console.WriteLine($"\nDistâncias mínimas a partir da cidade {fonte}:");
            for (int i = 0; i < grafo.Vertices; i++)
            {
                string d = (dist[i] == int.MaxValue) ? "INF" : dist[i].ToString();
                Console.WriteLine($"Cidade {i}: {d} km");
            }

            void MostrarCaminho(int destino)
            {
                if (dist[destino] == int.MaxValue)
                {
                    Console.WriteLine($"Não há caminho para {destino}.");
                    return;
                }
                var caminho = new System.Collections.Generic.List<int>();
                int atual = destino;
                while (atual != -1)
                {
                    caminho.Add(atual);
                    atual = pred[atual];
                }
                caminho.Reverse();
                Console.WriteLine($"Caminho para {destino}: {string.Join(" -> ", caminho)} (distância {dist[destino]} km)");
            }

            Console.WriteLine("\nExemplos de Rotas:");
            MostrarCaminho(9);
            MostrarCaminho(6);
            MostrarCaminho(Destino);
        }
    }
}