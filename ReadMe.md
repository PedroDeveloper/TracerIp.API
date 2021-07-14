Api para tracerip =P

As configurações desta API podem ser gerenciadas do arquivo appsettings.json

Configurações:

{
  "ConnectionStrings": {
    "ConnectionBase": Conexão do banco da aplicação
  },
  "Blob": {
    "Connection": 'Cadeia de conexão' com o Azure Blob, encontrada na Aba de Configurações / Chaves de Acesso,

    "Folder": pasta criada dentro do Blob para armazenar os arquivos,

    "BaseURL": URL base do Blob, para composição de Url a partir do nome dos arquivos
  }
}

Métodos:

GET
Traz todos os registros da Tabela de AcessWebSite

GetByGroup
Traz um select count agrupado pelo IP

POST
Comando para cadastro do IP





Formato de Mensagem

{
    
  "ip": "string",
  "dateAcess": "2021-07-14T01:48:02.112Z" datetime
}

