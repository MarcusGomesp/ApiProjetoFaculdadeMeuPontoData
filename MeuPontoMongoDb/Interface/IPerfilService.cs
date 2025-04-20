using MeuPontoMongoDb.Models;

namespace MeuPontoMongoDb.Interface
{
    public interface IPerfilService
    {
        Task<IEnumerable<Perfil>> ListarTodosAsync();

        Task<Perfil?> BuscarPorIdAsync(int userId);

        Task<string> CriarOuAtualizarImagemPerfilAsync(int userId, string imagemBase64);

        Task<bool> DeletarImagemPerfilAsync(int userId);
    }
}
