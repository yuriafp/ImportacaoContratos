#  Desafio Concilig_v2

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

## Como Executar o Projeto

### Pré-requisitos
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (ou superior)
* SQL Server (LocalDB, Express ou Developer)
* Visual Studio 2022 ou VS Code

### Passo a Passo

1. Clone o repositório:
   ```bash
   git clone https://github.com/yuriafp/ImportacaoContratos.git
   cd ImportacaoContratos
2. Verifique e ajuste a DefaultConnection do arquivo *ImportacaoContratos.API/appsettings.json* para apontar para o seu SQL Server local.

3. Abra o Console do Package Manager no Visual Studio e selecione o projeto padrão **ImportacaoContratos.Infrastructure** e executa o comando.
   ```bash
   Update-Database
4. Clique em *Propriedades* na solução do projeto e selecione *Vários projetos de inicialização**, marcando **ImportacaoContratos.API**	e **ImportacaoContratos.BlazorUI** com **Iniciar**.

5. Inicie o projeto.

Qualquer dúvida fico à disposição.

*yuri13_yuri@hotmail.com*
   
