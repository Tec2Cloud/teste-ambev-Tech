# API de Vendas

Esta API gerencia o ciclo de vida de vendas, incluindo operações para registrar, atualizar, cancelar e consultar vendas. 
A API foi construída com uma arquitetura em camadas para separar a lógica de negócios, repositórios e publicação de eventos, garantindo uma aplicação modular e fácil de manter.

## Tecnologias Utilizadas

- **.NET Core 7**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM utilizado para acessar o banco de dados.
- **SQLite**: Banco de dados utilizado para testes.
- **FluentAssertions**: Framework para asserções nos testes.
- **NSubstitute**: Biblioteca para criação de mocks nos testes unitários.
- **Bogus**: Biblioteca para geração de dados fictícios nos testes.
- **Serilog**: Ferramenta de logging utilizada para registrar eventos da aplicação.

## Estrutura da Aplicação

A aplicação segue uma arquitetura em camadas para garantir a separação de responsabilidades e facilitar a manutenção e extensibilidade.

### 1. **Camada de Domínio (`Domain`)**
   - Contém as entidades principais do sistema, como `Venda` e `ItemVenda`, assim como suas regras de negócios. Esta camada é independente das outras camadas e encapsula o núcleo da aplicação.

### 2. **Camada de Serviços (`Business`)**
   - Contém a lógica de negócios e os serviços que operam sobre as entidades do domínio. É nesta camada que as operações de `RegistrarVenda`, `AtualizarVenda` e `CancelarVenda` são realizadas. 
   - Também publica eventos como `VendaRegistrada`, `VendaAlterada` e `VendaCancelada` através do `EventPublisher`.
   
### 3. **Camada de Repositórios (`Data`)**
   - Responsável pela persistência e recuperação de dados. Os repositórios utilizam **Entity Framework Core** para interagir com o banco de dados.

### 4. **Camada de Publicação de Eventos (`Business/Events`)**
   - A publicação de eventos é gerenciada através de `IEventPublisher`, que pode ser implementado para diversos fins, como log ou integração com Message Brokers (RabbitMQ, etc.).

### 5. **Camada de API (`API`)**
   - Esta camada expõe os endpoints REST que permitem a interação com os serviços de vendas. Utiliza **Controllers** para interagir com os serviços de negócio.

### 6. **Testes (`Tests`)**
   - Contém os testes unitários para garantir o correto funcionamento dos serviços de vendas e a publicação de eventos.
   - Utiliza **Bogus** para geração de dados, **NSubstitute** para mocks e **FluentAssertions** para asserts.
   
---

## Funcionalidades

A API de vendas possui as seguintes funcionalidades:

### **1. Registrar Venda**
   Endpoint para registrar uma nova venda, associando um cliente, filial e itens.
   
   - **Método**: `POST /api/vendas`
   - **Corpo da Requisição**:
     ```json
     {
       "clienteId": "GUID",
       "nomeCliente": "Nome do Cliente",
       "filialId": "GUID",
       "nomeFilial": "Nome da Filial",
       "itens": [
         {
           "produtoId": "GUID",
           "nomeProduto": "Nome do Produto",
           "valorUnitario": 100.00,
           "quantidade": 2,
           "desconto": 10
         }
       ]
     }
     ```
   - **Resposta**:
     - `201 Created`: Quando a venda é registrada com sucesso.
     - `400 Bad Request`: Quando há erro de validação.

### **2. Atualizar Venda**
   Endpoint para atualizar uma venda existente, modificando os itens ou outras informações.

   - **Método**: `PUT /api/vendas/{vendaId}`
   - **Corpo da Requisição**:
     ```json
     {
       "itens": [
         {
           "produtoId": "GUID",
           "nomeProduto": "Nome do Produto",
           "valorUnitario": 100.00,
           "quantidade": 2,
           "desconto": 10
         }
       ]
     }
     ```
   - **Resposta**:
     - `200 OK`: Quando a venda é atualizada com sucesso.
     - `404 Not Found`: Quando a venda não é encontrada.

### **3. Cancelar Venda**
   Endpoint para cancelar uma venda existente.

   - **Método**: `PUT /api/vendas/{vendaId}/cancelar`
   - **Resposta**:
     - `204 No Content`: Quando a venda é cancelada com sucesso.
     - `404 Not Found`: Quando a venda não é encontrada.

### **4. Obter Venda por ID**
   Endpoint para buscar uma venda específica pelo seu ID.

   - **Método**: `GET /api/vendas/{vendaId}`
   - **Resposta**:
     - `200 OK`: Quando a venda é encontrada.
     - `404 Not Found`: Quando a venda não é encontrada.

### **5. Obter Todas as Vendas**
   Endpoint para buscar todas as vendas registradas.

   - **Método**: `GET /api/vendas`
   - **Resposta**:
     - `200 OK`: Retorna uma lista com todas as vendas.

---

## Como Rodar o Projeto

### 1. **Clonar o Repositório**

```bash
https://github.com/rodka82/availiacao-tecnica-backend-at.git
