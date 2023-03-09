# Apresentação do teste

## Para rodar o projeto localmente

**(importante) ter o docker instalado na maquina**

Faça o clone do projeto

Dentro da pasta "docker" rode os seguintes comandos:

- **docker-compose build** (para buildar o docker-compose e verificar se está tudo ok)
- **docker-compose up -d** (para rodar o docker-compose em background)

O docker-compose irá subir o **MongoDB** e o **MongoExpress** em um workspace chamado test-safra como na imagem abaixo:

Para acessar o **MongoExpress** basta inserir no browser o seguinte endereço: http://localhost:8081

O MongoDB vai estar rodando na porta 27017 padrão.

## Para rodar os projetos de API .NET CORE CORE (V.6)

Temos dois projetos:

ExpenseManagement.Api - na pasta "back", este é responsável for fornecer os dados de gasto do cliente e operações de update e insert de dados

ExpenseManagement.Authentication.Api - na pasta "auth", este é responsável por fornecer autenticação para o projeto, temos dois perfis aqui cadastrados:

- role "system" para sistemas que podem cadastrar gastos para um cliente
- role "client" para clientes aptos a visualizar gastos e alterar categorias

O sistema de autenticação fornece um token JWT que deve ser inserido nas chamadas a API de backend.

Todas as informações da API de autenticação nesta primeira versão estão "mockadas" na aplicação.

Colocando as duas aplicações para rodar teremos as duas API´s nos seguintes endereços:

- http://localhost:5226/swagger/index.html (API principal)
- http://localhost:5167/swagger/index.html (API autenticação)

## Para rodar o projeto de front Angular (V.15)

Dentro da pasta "front" acesse a pasta "spent-app" e rode os comandos:

- npm install (para instalar as dependências)
- ng serve (para rodar a aplicação)

A aplicação deverá ser consumida no endereço: http://localhost:4200/log-in

## Collection do Postman

Deixei dentro da pasta "others" as coleções do Postman utilizado neste projeto:

- API - ExpenseManagement - Auth.postman_collection.json
- API - ExpenseManagement.postman_collection.json

Basta abrir o Postman e importar a collection

## Telas da aplicação

![Alt text](https://github.com/mdcarmo/TestBackJava/blob/master/others/img1.png?raw=true)
![Alt text](https://github.com/mdcarmo/TestBackJava/blob/master/others/img2.png?raw=true)
![Alt text](https://github.com/mdcarmo/TestBackJava/blob/master/others/img3.png?raw=true)
![Alt text](https://github.com/mdcarmo/TestBackJava/blob/master/others/img4.png?raw=true)
![Alt text](https://github.com/mdcarmo/TestBackJava/blob/master/others/img5.png?raw=true)


# Show me the code

### # DESAFIO:

API REST para Gestão de Gastos!

```
Funcionalidade: Integração de gastos por cartão
  Apenas sistemas credenciados poderão incluir novos gastos
  É esperado um volume de 100.000 inclusões por segundo
  Os gastos, serão informados atraves do protoloco JSON, seguindo padrão:
    { "descricao": "alfanumerico", "valor": double americano, "codigousuario": numerico, "data": Data dem formato UTC }
```

```
Funcionalidade: Listagem de gastos*
  Dado que acesso como um cliente autenticado que pode visualizar os gastos do cartão
  Quando acesso a interface de listagem de gastos
  Então gostaria de ver meus gastos mais atuais.

*Para esta funcionalidade é esperado 2.000 acessos por segundo.
*O cliente espera ver gastos realizados a 5 segundos atrás.
```

```
Funcionalidade: Filtro de gastos
  Dado que acesso como um cliente autenticado
  E acessei a interface de listagem de gastos
  E configure o filtro de data igual a 27/03/1992
  Então gostaria de ver meus gastos apenas deste dia.
```

```
Funcionalidade: Categorização de gastos
  Dado que acesso como um cliente autenticado
  Quando acesso o detalhe de um gasto
  E este não possui uma categoria
  Então devo conseguir incluir uma categoria para este
```

```
Funcionalidade: Sugestão de categoria
  Dado que acesso como um cliente autenticado
  Quando acesso o detalhe do gasto que não possui categoria
  E começo a digitar a categoria que desejo
  Então uma lista de sugestões de categoria deve ser exibida, estas baseadas em categorias já informadas por outro usuários.
```

```
Funcionalidade: Categorização automatica de gasto
  No processo de integração de gastos, a categoria deve ser incluida automaticamente
  caso a descrição de um gasto seja igual a descrição de qualquer outro gasto já categorizado pelo cliente
  o mesmo deve receber esta categoria no momento da inclusão do mesmo
```

### # Avaliação

Você será avaliado pela usabilidade, por respeitar o design e pela arquitetura da API.
É esperado que você consiga explicar as decisões que tomou durante o desenvolvimento através de commits.

- Springboot - Java - Maven (preferêncialmente) ([https://projects.spring.io/spring-boot/](https://projects.spring.io/spring-boot/))
- RESTFul ([https://blog.mwaysolutions.com/2014/06/05/10-best-practices-for-better-restful-api/](https://blog.mwaysolutions.com/2014/06/05/10-best-practices-for-better-restful-api/))
- DDD ([https://airbrake.io/blog/software-design/domain-driven-design](https://airbrake.io/blog/software-design/domain-driven-design))
- Microservices ([https://martinfowler.com/microservices/](https://martinfowler.com/microservices/))
- Testes unitários, teste o que achar importante (De preferência JUnit + Mockito). Mas pode usar o que você tem mais experiência, só nos explique o que ele tem de bom.
- SOAPUI para testes de carga ([https://www.soapui.org/load-testing/concept.html](https://www.soapui.org/load-testing/concept.html))
- Uso de diferentes formas de armazenamento de dados (REDIS, Cassandra, Solr/Lucene)
- Uso do git
- Diferencial: Criptografia de comunicação, com troca de chaves. ([http://noiseprotocol.org/](http://noiseprotocol.org/))
- Diferencial: CQRS ([https://martinfowler.com/bliki/CQRS.html](https://martinfowler.com/bliki/CQRS.html))
- Diferencial: Docker File + Docker Compose (com dbs) para rodar seus jars.

### # Observações gerais

Adicione um arquivo [README.md](http://README.md) com os procedimentos para executar o projeto.
Pedimos que trabalhe sozinho e não divulgue o resultado na internet.

Faça um fork desse desse repositório em seu Github e nos envie um Pull Request com o resultado, por favor informe por qual empresa você esta se candidatando.

### # Importante: não há prazo de entrega, faça com qualidade!

# BOA SORTE!
