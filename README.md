# ParkingControl -- Controle de estacionamento
![image](https://github.com/user-attachments/assets/0f43d5ee-49e1-4f7b-86e3-40b88f6960b0)

https://github.com/user-attachments/assets/f2d17cf7-ebf3-48bb-a682-57fa8da794f7

Tecnologias utilizadas no desenvolvimento da API: C# - dotnet, banco de dados SQLite usando ORM Entity Framework.

# Requisitos mínimos
```sh
## Instalar .NET 8.0 SDK
Baixe através do link: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.401-windows-x64-installer 

## Instalar Entity Framework
Instale através deste comando
dotnet tool install --global dotnet-ef --version 8.*

```

Para executar a aplicação:
```sh
 git clone https://github.com/alinebarbosasilva/parking-control-api.git
 cd parking-control-api
 dotnet restore
 dotnet ef database update
 dotnet run
```
Desenvolvido um aplicativo para controle de estacionamento onde o usuário poderá registrar a entrada e saída dos veículos. 

Os valores praticados pelo estacionamento foram parametrizados em uma tabela de preços com controle vigência. Exemplo: Valores válidos para o período de 01/01/2024 até 31/12/2024.

Utilizado a data de entrada do veículo como referência para buscar a tabela de preços.

A tabela de preço contemplou o valor da hora inicial e valor para as horas adicionais.

Foi cobrado metade do valor da hora inicial quando o tempo total de permanência no estacionamento for igual ou inferior a 30 minutos.

O valor da hora adicional possui uma tolerância de 10 minutos para cada 1 hora. Exemplo: 30 minutos valor R$ 1,00 | 1 hora valor R$ 2,00 | 1 hora 10 minutos valor R$ 2,00 | 1 hora e 15 minutos valor R$ 3,00 | 2 horas e 5 minutos valor R$ 3,00 | 2 horas e 15 minutos valor R$ 4,00.

Utilizado a placa do veículo como chave de busca. 

