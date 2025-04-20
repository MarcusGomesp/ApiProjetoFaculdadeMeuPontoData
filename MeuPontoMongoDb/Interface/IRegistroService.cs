using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.DTO;

namespace MeuPontoMongoDb.Interface
{
    public interface IRegistroService
    {
        Task<IEnumerable<Registro>> ListarTodosAsync();
        Task<List<Registro>> BuscarTodosPorUsuarioAsync(int id);
        Task<int?> BuscarIdRegistroPorEmailAsync(string email);
        Task<Registro> CriarAsync(Registro registro);
        Task<Registro> AtualizarAsync(int id, Registro registro);
        Task<bool> DeletarAsync(int id);
    }
}
