using MeuPontoMongoDb.Models;

namespace MeuPontoMongoDb.Interface
{
    public interface ITokenService
    {
        /// <summary>Gera um JWT assinado para o usuário autenticado.</summary>
        (string Token, DateTime ExpiraEm) GerarToken(Cadastro usuario);
    }
}
