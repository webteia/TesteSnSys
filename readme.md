execute o download em uma pasta do projeto no github:
 git clone https://github.com/webteia/TesteSnSys.git

Acesse por terminal a pasta que contém o docker-compose

execute: docker compose  up --build

acesse no navegador o PGAdmin: http://localhost:5050/browser/
Nos server clique com o botão direito e Register-Server

na guia General no campo name coloque:postgres
na guia Connection no campo Host/name coloque: postgres 
Port: 5432
Maintenance: postgres
Username: postgres
password: changeme

clique em save

acesse o novo server e crie o database com nome myDataBase.

acesse a solution:

no console do gerenciador de pacotes executaremos o migration pelo comando: dotnet ef database update

usando postman requisições poderão ser feitas para o endereço: http://localhost:49155/api

para testar o login basta chamar o http://localhost:49155/api/Authenticacao/login 
passando o json:
 {
"nomeLogin":"admin",
"password": "12345"
}

será retornado o token que deverá ser passado para as requisições da tabela de clientes!
