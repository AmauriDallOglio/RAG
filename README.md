# Projeto RAG - Microserviço de Ingestão de Documentos

## Sobre RAG

**Retrieval-Augmented Generation (RAG)** é uma abordagem que combina modelos de linguagem com mecanismos de busca sobre uma base de conhecimento externa. Em vez de depender apenas do conhecimento embutido no modelo, o sistema recupera trechos relevantes de documentos previamente processados e os utiliza como contexto adicional para gerar respostas mais precisas e fundamentadas. Este microserviço atua justamente na etapa de **preparação e organização** desses documentos, deixando-os prontos para consulta por sistemas de recuperação e geração.

Microserviço desenvolvido em **.NET**, com arquitetura em camadas, voltado para a preparação e armazenamento de documentos que podem ser utilizados em uma solução de **Retrieval-Augmented Generation (RAG)**.

O projeto disponibiliza uma API REST capaz de receber documentos externos, extrair seu conteúdo textual, organizar esse conteúdo em trechos e palavras relevantes, e persistir essas informações em banco de dados para posterior consulta e uso por aplicações consumidoras ou modelos de linguagem.

## Função Principal

A principal função do projeto é atuar como uma **base de ingestão e organização de conhecimento documental** para uma arquitetura RAG. Ele prepara documentos externos para que possam ser consultados posteriormente e utilizados como fonte de informação por sistemas inteligentes, reduzindo o acoplamento entre as aplicações consumidoras e a camada de armazenamento/processamento dos documentos.

## Endpoints

A aplicação expõe endpoints para:

- **Importação de documentos** — recebe um arquivo externo para processamento e persistência.
- **Consulta paginada** — lista os documentos já cadastrados no sistema.

## Fluxo de Importação

Ao importar um documento, o sistema executa as seguintes etapas:

1. Lê o arquivo enviado.
2. Registra metadados como título, tipo e tamanho do arquivo.
3. Sanitiza o texto extraído.
4. Divide o conteúdo em frases.
5. Identifica palavras significativas, ignorando termos comuns por meio de uma lista de **stopwords**.
6. Persiste as informações organizadas no banco de dados.

Essa estrutura facilita etapas futuras de busca, recuperação de contexto e enriquecimento de respostas geradas por inteligência artificial.

## Arquitetura

O projeto é organizado em camadas, separando claramente as responsabilidades:

- **API** — exposição dos endpoints REST.
- **Aplicação** — execução dos casos de uso.
- **Domínio** — regras de negócio e entidades centrais.
- **Infraestrutura** — repositórios, persistência de dados e integrações externas.

## Tecnologias e Configurações

- .NET
- Entity Framework Core
- SQL Server
- Swagger
- CORS
- Injeção de dependência
- Padrão CQRS
- Rate limiting (controle de limite de requisições)

