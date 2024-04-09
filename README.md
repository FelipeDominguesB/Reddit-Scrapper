# Reddit Scrapper


Esse projeto é um web-scrapper para a plataforma/rede-social Reddit. O objetivo dessa aplicação é poder baixar de maneira rápida e prática as mídias dos posts de um subreddit, dando suporte também para que o usuário gerencie os arquivos. No momento, essa aplicação conta com 4 artefatos, sendo eles:
- Uma API, onde é possível criar e consultar rotinas existentes.
- Um service worker responsável por executar as rotinas e postar o caminho dos arquivos associados à elas numa fila
- Um service worker responsável por ler o caminho dos arquivos e baixa-los no file system
- Um banco de dados relacional, onde toda persistência de dados é realizado

Esse projeto também tem a dependência de uma instância do RabbitMQ, componente essencial para comunicação entre os workers.
A API e os dois service workers foram desenvolvidos (e, por tanto, necessitam) do .NET 8. O banco de dados utilizado foi o SQL Server, você pode encontrar o script de criação do banco [aqui](https://github.com/FelipeDominguesB/Reddit-Scrapper/blob/main/create_database.sql).
