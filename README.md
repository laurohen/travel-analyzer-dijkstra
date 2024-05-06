# travel-analyzer-dijkstra



## Tecnologias utilizadas:

- .NET
- MSSQL
- Docker



# Gerenciamento de Rotas de Viagem

Este projeto consiste em uma API que permite o gerenciamento de rotas de viagem, incluindo um endpoint para encontrar a rota mais barata entre dois pontos, independente da quantidade de conexões.

## Funcionalidades

### CRUD de Rotas

A API oferece endpoints para criar, ler, atualizar e excluir rotas de viagem. Cada rota possui uma origem, destino e valor associado.

### Encontrar Rota Mais Barata

A API possui um endpoint que recebe a origem e o destino desejados e retorna a rota de viagem mais barata entre esses pontos, levando em consideração todas as possíveis conexões.

## Exemplo de Uso

Suponha que temos as seguintes rotas cadastradas:

- Origem: GRU, Destino: BRC, Valor: 10
- Origem: BRC, Destino: SCL, Valor: 5
- Origem: GRU, Destino: CDG, Valor: 75
- Origem: GRU, Destino: SCL, Valor: 20
- Origem: GRU, Destino: ORL, Valor: 56
- Origem: ORL, Destino: CDG, Valor: 5
- Origem: SCL, Destino: ORL, Valor: 20

Podemos utilizar o endpoint de busca da rota mais barata da seguinte forma:

```http

- GET /api/Grafo/GRU/CDG

```

#### Exemplos de Rotas

- Criar Rota: `POST /api/routes`
  - Corpo da Requisição: `{ "origin": "GRU", "destination": "BRC", "value": 10 }`

- Listar Rotas: `GET /api/routes`

- Buscar Rota por ID: `GET /api/routes/{id}`

- Atualizar Rota: `PUT /api/routes/{id}`
  - Corpo da Requisição: `{ "origin": "GRU", "destination": "BRC", "value": 15 }`

- Excluir Rota: `DELETE /api/routes/{id}`


## Documentação da API

A documentação da API pode ser encontrada em [Swagger](https://localhost:5001/swagger/index.html).

## Features

### Banco de dados

Foi escolhida a versão 2019 do MSSQL e a migração inicial é feita via script dentro do docker-compose.


## Docker

Foi utilizado docker-compose para subir servidor de MSSQL e a aplicacao executando insert de dados inicias na tabela 



## Algoritmo aplicado: Dijkstra

O algoritmo de Dijkstra é um algoritmo clássico em teoria dos grafos, utilizado para encontrar o caminho mais curto em um grafo ponderado, direcionado ou não direcionado, com pesos não negativos.

Ele funciona mantendo uma lista de vértices cujas distâncias do vértice inicial são conhecidas. Inicialmente, a distância do vértice inicial a si mesmo é 0, e a distância para todos os outros vértices é considerada infinita. Em cada passo do algoritmo, ele seleciona o vértice não marcado com a menor distância conhecida, marca esse vértice como visitado e atualiza as distâncias dos seus vizinhos, caso uma rota através desse vértice seja mais curta. Esse processo é repetido até que todos os vértices tenham sido visitados.

O algoritmo de Dijkstra garante a corretude do caminho mais curto se os pesos das arestas forem todos não negativos. No entanto, se houverem pesos negativos, o algoritmo não é garantido para funcionar corretamente, pois pode entrar em um ciclo de atualizações infinitas. Para grafos com pesos negativos, é necessário usar algoritmos como o algoritmo de Bellman-Ford.
