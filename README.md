# SegImob API
API para o front end consumir

Link para [Tempos](https://docs.google.com/document/d/14MHKs-f7WTNHJFaAgIUIJEYfe-_xvy7LXGnuOFtVSGw/edit?usp=drive_link)
Link para [Manual de Utilização](https://docs.google.com/document/d/1m53BzTNF4rq19SMJ8OFnOc8K9Y7Gq8zh3uX00pZbUdY/edit?usp=drive_link)

# ENDPOINTS

Tem a coleção de endpoints no postman,  na pasta Auxiliares/Postman. Consultar [Aqui](https://github.com/DinisSimoes/segImob_API/blob/main/TransportAPI/Auxiliares/Postman/SegImob%20Transport.postman_collection.json)

## Serviços
| Rota   | Descrição   |
|------------|------------|
|  GET  https://localhost:7068/api/Servico   | retorna todos os servicos   |
|  GET  https://localhost:7068/api/Servico/<id>   | retorna o servico com o id   |
|  POST  https://localhost:7068/api/Servico   | cria o servico que é passado no body  |
|  PUT  https://localhost:7068/api/Servico/<id>   | atualiza o objeto com o id com o servico que é passado no body da chamada  |
|  DELETE  https://localhost:7068/api/Servico/<id>   | elimina o servico com o id   |

## Transporte
| Rota   | Descrição   |
|------------|------------|
|  GET  https://localhost:7068/api/Transporte   | retorna todos os transportes   |
|  GET  https://localhost:7068/api/Transporte/<id>   | retorna o transporte com o id   |
|  POST  https://localhost:7068/api/Transporte   | cria o transporte que é passado no body  |
