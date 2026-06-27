Projeto Grafos – Análise e Complexidade de Algoritmos

Este projeto foi desenvolvido para a disciplina **Análise e Complexidade de Algoritmos** e implementa, em C# (.NET 8), os principais algoritmos sobre grafos: representações (matriz e lista de adjacência), BFS, DFS, Dijkstra (com heap e com vetor), Bellman-Ford, e uma solução para o problema **Beecrowd 1148 – Países em Guerra**.

🚀 Tecnologias

- .NET 8.0 (ou superior)
- C# (Console Application)

📦 Pré‑requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) instalado.

📥 Como baixar e executar
1. Clone o repositório
git clone https://github.com/JulioGeraldoEng/algoritmos-grafos-grupo5.git

2. Entre na pasta
cd algoritmos-grafos-grupo5

3. Execute o programa
dotnet run
O menu será exibido automaticamente.

📋 Estrutura do projeto
text
ProjetoGrafos/
├── BellmanFord/            # Algoritmo de Bellman-Ford
├── Bfs/                    # Busca em Largura (BFS)
├── Desafio/                # Solução do Beecrowd 1148
├── Dfs/                    # Busca em Profundidade (DFS)
├── Dijkstra/               # Dijkstra com Heap e com Vetor
├── Grafos/                 # Representações (matriz e lista)
├── MapaCidades/            # Exemplo prático com 10 cidades
├── Program.cs              # Menu principal
└── ProjetoGrafos.csproj    # Arquivo do projeto .NET 8

🧭 Menu de opções
Opção	Descrição
1	Mapa de Cidades – Mostra o grafo de 10 cidades brasileiras e calcula distâncias mínimas a partir de São Paulo (Dijkstra com heap).
2	Desafio Beecrowd 1148 – Resolve o problema "Países em Guerra". Permite ler dados de um arquivo .txt ou digitar manualmente.
3	Testes BFS, DFS e Bellman-Ford – Executa os algoritmos em um grafo de teste e exibe os resultados.
4	Conectividade, Ciclos e Componentes – Verifica se o grafo é conexo, detecta ciclos e conta componentes (usando BFS e DFS).
5	Comparação de Desempenho – Compara Dijkstra com Heap vs. Vetor em grafos aleatórios, exibindo tempos médios.
0	Sair – Encerra o programa.

📂 Como usar a opção 2 (Desafio Beecrowd 1148)
Ao escolher a opção 2, o programa perguntará:

Como deseja fornecer os dados?
1 - Ler de um arquivo .txt
2 - Digitar manualmente
Opção 1: O programa pedirá o caminho do arquivo. Se pressionar Enter, usará entrada.txt na pasta do projeto.
Opção 2: Você poderá digitar os dados diretamente no console (no formato do Beecrowd). Para encerrar, digite 0 0.

Exemplo de arquivo entrada.txt
txt
4 5
1 2 5
2 1 10
3 4 8
4 3 7
2 3 6
5
1 2
1 3
1 4
4 3
4 1
0 0

📄 Licença
Este projeto foi desenvolvido para fins acadêmicos.

👥 Autores
Igor Jacomossi Riberto – BI300094X
Jorge Luiz Marques Bom Junior – BI3012018
Júlio Cesar de Oliveira Geraldo – BI300399X
Lucas Alves De Souza – BI3003698
Vitor da Silva Campos – BI3005577

🔗 Link do repositório
https://github.com/JulioGeraldoEng/algoritmos-grafos-grupo5
