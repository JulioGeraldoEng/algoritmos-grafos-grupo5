```
# Projeto Grafos – Análise e Complexidade de Algoritmos

Este projeto foi desenvolvido para a disciplina Análise e Complexidade de Algoritmos e implementa, em C# (.NET 8), os principais algoritmos sobre grafos: representações (matriz e lista de adjacência), BFS, DFS, Dijkstra (com heap e com vetor), além de um mapa de cidades brasileiras para demonstração prática e uma comparação de desempenho entre as versões com heap e vetor.

---

## 🚀 Tecnologias

- .NET 8.0 (ou superior)
- C# (Console Application)

---

📦 Pré‑requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) instalado.

Para verificar:
```bash
dotnet --version
```

---

## 📥 Como baixar e executar

```
1. Clone o repositório:
```bash
git clone https://github.com/JulioGeraldoEng/algoritmos-grafos-grupo5.git
```

2. Entre na pasta:
```bash
cd algoritmos-grafos-grupo5/ProjetoGrafos
```

3. Execute o programa:
```bash
dotnet run
```

O menu será exibido automaticamente.

---

## 📋 Estrutura do projeto

```
ProjetoGrafos/
├── Program.cs                     # Menu principal
├── Comparacao/                    # Comparação Heap vs Vetor
│   └── DesempenhoDijkstra.cs
├── Bfs/                           # Busca em Largura (BFS)
│   ├── BFS.cs
│   └── AnalisadorBFS.cs
├── Dfs/                           # Busca em Profundidade (DFS)
│   ├── DFS.cs
│   └── AnalisadorDFS.cs
├── Dijkstra/                      # Dijkstra com Heap e com Vetor
│   ├── AlgoritmoDijkstra.cs
│   ├── DijkstraVetor.cs
│   └── AnalisadorDijkstra.cs
├── Grafos/                        # Representações (matriz e lista)
│   ├── GrafoMatrizAdjacencia.cs
│   ├── GrafoListaAdjacencia.cs
│   ├── AnalisadorMatriz.cs
│   └── AnalisadorLista.cs
├── MapaCidades/                   # Exemplo prático com 10 cidades
│   └── MapaCidades.cs
└── ProjetoGrafos.csproj           # Arquivo do projeto .NET 8
```

---

## 🧭 Menu de opções

| Opção | Descrição |
|-------|-----------|
| **1** | **Matriz de Adjacência** – Analisa um grafo carregado de arquivo `.txt` e exibe estatísticas (densidade, graus, espaço, matriz). |
| **2** | **Lista de Adjacência** – Mesma análise, mas com a representação por listas de vizinhos. |
| **3** | **Busca em Largura (BFS)** – Executa BFS a partir de um vértice origem, exibe distâncias, níveis e árvore BFS. |
| **4** | **Busca em Profundidade (DFS)** – Executa DFS, exibe tempos de descoberta/finalização e classifica as arestas (árvore, retorno, avanço, cruzamento). |
| **5** | **Dijkstra (caminho mínimo)** – Calcula caminhos mínimos a partir de uma origem usando heap mínimo. |
| **6** | **Mapa de Cidades (Dijkstra)** – Aplica Dijkstra ao grafo de 10 cidades brasileiras, mostrando distâncias e caminhos a partir de São Paulo. |
| **7** | **Comparar desempenho (Heap vs Vetor)** – Gera grafos aleatórios e mede tempos das duas versões do Dijkstra, exibindo ganhos. |
| **0** | **Sair** – Encerra o programa. |

---

## 📂 Como escolher o arquivo de entrada

Para as opções que exigem um grafo (1 a 5), o programa lista 12 arquivos de exemplo na pasta `Grafos/`:

1. `1_grafo_denso.txt`
2. `2_grafo_esparso.txt`
3. `3_grafo_direcionado.txt`
4. `4_grafo_nao_direcionado.txt`
5. `5_grafo_com_ciclos.txt`
6. `6_grafo_sem_ciclos.txt`
7. `7_grafo_pesos_negativos.txt`
8. `8_grafo_para_dijkstra.txt`
9. `9_grafo_desconexo.txt`
10. `10_grafo_para_bfs_dfs.txt`
11. `11_grafo_grande_denso.txt`
12. `12_grafo_direcionado_com_ciclo.txt`

Também é possível digitar o caminho de um arquivo personalizado (opção 13).

**Formato do arquivo:**

```
V Direcionado (0 ou 1)
origem destino peso
origem destino peso
...
```

**Exemplo:**

```
4 0
0 1 5
0 2 3
1 3 2
2 3 7
```

---

## 🧪 Casos de teste

Os casos de teste para a opção 5 (Dijkstra) estão disponíveis nos arquivos:

- `Grafos/teste1_entrada.txt`
- `Grafos/teste2_entrada.txt`
- `Grafos/teste3_entrada.txt`

Eles cobrem grafos conectados, desconexos e com múltiplos caminhos.

---

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos.

---

## 👥 Autores

- Igor Jacomossi Riberto – BI300094X
- Jorge Luiz Marques Bom Junior – BI3012018
- Júlio Cesar de Oliveira Geraldo – BI300399X
- Lucas Alves De Souza – BI3003698
- Vitor da Silva Campos – BI3005577

---