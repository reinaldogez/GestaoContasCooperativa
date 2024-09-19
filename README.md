# GestaoContasCooperativa

![Arquitetura](images/gestao_contas_cooperativa_architecture_diagram.drawio.svg)


## Decisões de Arquitetura

- **Fluent API ao invés de Data Annotations na entidade Cliente:**
  
  Optei por utilizar a **Fluent API** para configurar a entidade **Cliente** em vez de **Data Annotations**. Essa escolha segue as convenções do **DDD**, que orientam que o modelo de domínio deve ser puro e livre de dependências de frameworks. Dessa forma, o modelo de domínio permanece coeso e flexível, facilitando futuras manutenções e evoluções.

- **IClientRepository interface**

  Usei a interface IClienteRepository pois isso promove o desacoplamento entre o código e a implementação, facilitando a substituição e os testes de componentes. Além disso, essa abordagem respeita o Princípio de Inversão de Dependência (D de SOLID), permitindo que se trabalhe com abstrações, tornando o código mais flexível e fácil de manter.

## Possíveis melhorias

- **Utilizar gRPC entre os microserviços e o BFF:**
  
  A vantagem seria um ganho em média de **20 milissegundos** na comunicação entre os microserviços. Porém, isso aumenta a complexidade e a manutenção do sistema.

- **Separação de Leitura e Gravação do Banco de Dados (CQRS):**
  
  Adotar o padrão **CQRS (Command Query Responsibility Segregation)** para otimizar as operações de leitura e escrita, o que possibilitaria uma melhor a escalabilidade e desempenho da aplicação.

- **Testes Unitários e de Integração:**
  
  Desenvolver uma suíte completa de **testes unitários** e **de integração** para garantir a qualidade, confiabilidade e manutenibilidade do código.

- **Utilizar Docker:**
  
  Containerizar a aplicação utilizando **Docker**, facilitando o desenvolvimento, o deploy e a escalabilidade em diferentes ambientes, além de garantir consistência entre os ambientes de desenvolvimento e produção.

- **CPF como Value Object:**
  
  Refatorar o **CPF** para ser um **Value Object**, garantindo a validação e encapsulamento das regras de negócio relacionadas a ele, conforme os princípios do **DDD**.