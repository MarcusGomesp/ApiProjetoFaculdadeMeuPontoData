using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.Login;

namespace MeuPontoMongoDb.Interface
{
    public interface ICadastroService
    {
        Task<IEnumerable<Cadastro>> ListarTodosAsync();

        Task<Cadastro?> BuscarPorIdAsync(int userId);

        Task<object> BuscarCadastroComUltimoRegistroPorEmailAsync(string email);

        Task<Cadastro> CriarAsync(Cadastro cadastro);

        Task<object> LoginAsync(Login login);

        Task<bool> DeletarAsync(int userId);

    }
}
