# Take Home Assignment - Loft
Take Home Assignment

# O Projeto
O projeto foi uma API utilizando .NET Core. Foi implementado o swagger como documentação.
Foi criado um metodos para: 
- Retornar uma lista com os herois existentes;
- Retornar uma lista com as informações completas dos personagens criados, incluido as caracteristicas da classe;
- Retornar uma lista com as informações simplificadas dos personagens;
- Criar um novo personagem;
- Retornar resultado da batalha entre dois personagens;
- Retornar detalhes de um personagem específico.

Foi criado teste para os metodos de personagens.

# Executando o projeto
Caso queira criar os personagens todos do inicio, será necessario alterar o arquivo "appsettings.json", colocando a tag "UseCharMock": "false";
O diretorio do log tambem é confirado no arquivo "appsettings.json";
Como o projeto ainda não tem um metodo para criação de herois, será necessário deixar a tag "UseHeroMock" no arquivo "appsettings.json" como true;
Setar o projeto "Web.Api.CodeGame" Set as Startup Project
Após as configurações acima, basta executar a solution e passar os parametros dos metodos desejados através do swagger. O retorno será dado pelo proprio swagger, em uma interface amigavel.

# Melhorias Futuras
1. Colocar o Swagger/API para funcionar com "Authorization";
2. Implementar o Docker;
3. Melhoria da classe de log (Utilizar interface);
4. Criação de uma base de dados para não precisar fazer um Mock das classes.
5. Criação de um metodo para criar Herois.
