# GestaoContasCooperativa

![Arquitetura](images/gestao_contas_cooperativa_architecture_diagram.drawio.svg)


## Decisões de Arquitetura

- **Fluent API ao invés de Data Annotations na entidade Cliente:**
  
  Optei por utilizar a **Fluent API** para configurar a entidade **Cliente** em vez de **Data Annotations**. Essa escolha segue as convenções do **DDD**, que orientam que o modelo de domínio deve ser puro e livre de dependências de frameworks. Dessa forma, o modelo de domínio permanece coeso e flexível, facilitando futuras manutenções e evoluções.

- **IClientRepository interface**

  Usei a interface IClienteRepository pois isso promove o desacoplamento entre o código e a implementação, facilitando a substituição e os testes de componentes. Além disso, essa abordagem respeita o Princípio de Inversão de Dependência (D de SOLID), permitindo que se trabalhe com abstrações, tornando o código mais flexível e fácil de manter.

- **Classe abstrata ContaBancaria:**

  Escolhi usar uma classe abstrata **ContaBancaria** para representar o conceito genérico de uma conta bancária, pois ela encapsula atributos e métodos comuns a todos os tipos de contas (como número, agência, saldo, depósito e saque). As classes derivadas **ContaCorrente** e **ContaPoupanca** podem então especializar o comportamento, implementando regras específicas, como limites de saque e cálculo de juros, aproveitando o polimorfismo e a herança para evitar duplicação de código.

- **Exceptions nas contas:**

  As exceptions relacionadas às contas foram salvas na camada de domínio, pois estão relacionadas a regras de negócio que pertencem à lógica de domínio.

- **Validações nos DTOs:**

  As validações foram implementadas diretamente nos **DTOs** utilizando **DataAnnotations**, garantindo a integridade dos dados na entrada da aplicação.

- **Orquestração de validação de juros e rendimentos:**

  A responsabilidade de orquestrar operações como validar se o cliente e a conta coincidem antes de calcular os juros e rendimentos ficará a cargo do serviço de **scheduling/agendamento**.

- **Agendamento de Cálculo de Rendimentos**

O serviço que calcula os rendimentos da **ContaPoupança** pode ser implementado de várias formas:

- **Azure Logic Apps**: Para disparar uma requisição HTTP mensalmente de forma simples e gerenciada.
- **Kubernetes CronJob**: Alternativa em ambientes Kubernetes que permite agendar tarefas com maior controle.
- **Quartz.NET**: Ideal se a prioridade for baixa complexidade, implementado diretamente no microserviço, sem depender de infraestrutura externa.

## Camada BFF (Backend for Frontend)

A implementação do microserviço **BFF (Backend for Frontend)**, que consolidaria e simplificaria as interações entre o frontend e os microserviços existentes (**ClientesService** e **ContasService**), não foi possível nesta iteração devido às restrições de tempo. O objetivo desse microserviço seria intermediar e simplificar a comunicação entre o frontend e nossos microserviços já existentes, **ClientesService** e **ContasService**. O **BFF** seria responsável por consolidar dados dessas diferentes APIs, retornando ao frontend uma resposta otimizada e contendo apenas as informações necessárias, como o saldo da conta, dados do cliente e resultados de cálculos de rendimentos ou juros.

Por exemplo, quando o frontend precisasse de dados sobre um cliente e suas contas, o **BFF** faria chamadas ao **ClientesService** para obter as informações do cliente e ao **ContasService** para acessar o status das contas. Ele então agregaria essas respostas, eliminando a necessidade de o frontend gerenciar múltiplas chamadas HTTP.

Na prática, essa camada poderia ser implementada usando **HTTP** para chamadas aos microserviços, mantendo a consistência com o que já foi desenvolvido, mas também seria possível adotar **gRPC** se houvesse uma demanda por menor latência. Além disso, o **BFF** também poderia implementar as validações de negócio e autenticação, centralizando a lógica que atualmente está distribuída entre os microserviços.

O BFF também facilitaria a integração com futuras melhorias, como a implementação de **schedulers** para cálculos automáticos de juros e rendimentos, garantindo que o frontend receba os dados mais recentes sem sobrecarregar os microserviços diretamente.

- **Débito técnico:**

  Existe um débito técnico a ser resolvido: o **CPFCliente** está retornando `null` no corpo da resposta ao criar contas no endpoint `Contas/Criar` com o código correto `201`.


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