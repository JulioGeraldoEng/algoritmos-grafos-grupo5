using System;
using ProjetoGrafosGrupo5.Grafos;
using ProjetoGrafosGrupo5.Dijkstra;

namespace ProjetoGrafosGrupo5.MapaCidades
{
    public static class MapaCidades
    {
        public static void ConstruirEExecutar()
        {
            Console.WriteLine("\n=== MAPA DE CIDADES BRASILEIRAS (Dijkstra) ===");
            Console.WriteLine("Grafo com 10 cidades e 15 rodovias.\n");

            var grafo = new GrafoListaAdjacencia(10, direcionado: false);

            // Adiciona arestas
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

            int origem = 0;

            Console.WriteLine("Cidades (vértices):");
            Console.WriteLine("  0 - São Paulo (SP)");
            Console.WriteLine("  1 - Rio de Janeiro (RJ)");
            Console.WriteLine("  2 - Belo Horizonte (MG)");
            Console.WriteLine("  3 - Brasília (DF)");
            Console.WriteLine("  4 - Goiânia (GO)");
            Console.WriteLine("  5 - Curitiba (PR)");
            Console.WriteLine("  6 - Porto Alegre (RS)");
            Console.WriteLine("  7 - Salvador (BA)");
            Console.WriteLine("  8 - Aracaju (SE)");
            Console.WriteLine("  9 - Fortaleza (CE)");
            Console.WriteLine($"\nCalculando distâncias mínimas a partir de São Paulo (vértice {origem})...\n");

            var resultado = AlgoritmoDijkstra.Executar(grafo, origem);
            int[] distancias = resultado.distancias;
            int[] predecessores = resultado.predecessores;

            AnalisadorDijkstra.Analisar(grafo, origem, distancias, predecessores);
        }
    }
}