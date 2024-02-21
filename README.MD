# TCC - EQUIPE 1

## EXERCICIOS

####  1. A Logistics possuirá dois ambientes de Dynamics. Vamos chama-los de Dynamics 1 e Dynamics 2. O Dynamics 1 deverá receber o cadastro de produtos. Você deverá integrar a tabela de produtos, enviando o cadastro para o Dynamics 2 (Plugin).

#### 2.	Um produto não pode ser cadastrado diretamente no Dynamics 2 (Plugin).

#### 3.	A Logistics deseja uma maneira de poder enviar as cotações para o Cliente, sempre que uma Cotação for ativada (Power Automate).

#### 4.	A Logistics precisa que sempre que um CEP for digitado, os demais campos sejam preenchidos automaticamente (logradouro, complemento, bairro, localidade, UF, Código IBGE e DDD. ViaCEP - Webservice CEP e IBGE gratuito. JavaScript + Action)

#### 5.	Os usuários em campo utilizarão um aplicativo que deverá conter um FAQ, uma lista de clientes e oportunidades. O FAQ servirá para ajudar o vendedor com as principais perguntas do cliente. (Canvas App).

#### 6.	A Logístics precisa saber qual foi a última data de visita para um determinado cliente. (Power Automate)

#### 7.	A Logistics deseja reaproveitar algumas Oportunidades antigas para não ter sempre que adicionar os mesmos produtos em Oportunidades novas, por isso ela deseja ter a possibilidade de Clonar uma Proposta (Javascript + Action + Ribbon).

#### 8.	Cada oportunidade deverá ter um número alfa numérico que será o identificador único dela. Você deve garantir através de customizações que esse número não esteja duplicado no sistema. O identificador deverá conter o seguinte padrão: OPP-12365-A1A2 (Plugin).

#### 9.	A Logistics precisa que os campos CNPJ sejam formatados e válidos (JavaScript).

#### 10.	A Logístics deseja que o nome da conta seja sempre cadastrada com o padrão “Nome Da Conta” e nunca “nome da conta” ou “NOME DA CONTA” (JavaScript).

#### 11.	A Logistics deseja integrar as oportunidades do Dynamics 1 para o Dynamics 2 Porém se a oportunidade tiver vindo de integração ela não pode ser editada (JavaScript + Plugin).

#### 12.	A Logistics precisa que o cadastro de CPF em contatos seja válido.(JavaScript)

#### 13.	Não pode haver no sistema um contato com o mesmo CPF, nem uma conta com o mesmo CNPJ (Plugin).


- Desafio: Esse desafio poderá ser realizado mas não é obrigatórios para a apresentação do Projeto. Porém é considerado Plus. A Logistics vai integrar um outro sistema de controle de estoque no Dynamics CE chamado “My Warehouse”. O problema encontrado é que existem cadastros de cliente sendo realizados no Dynamics CE e no My Warehouse. A necessidade então é que sempre que uma conta for criada, modificada ou excluída no sistema de estoque, essa ação precisa ser refletida no Dynamics CE (Azure Function).