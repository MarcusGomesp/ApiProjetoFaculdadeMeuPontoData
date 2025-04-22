using MeuPontoMongoDb.Models.Enum;
using MeuPontoMongoDb.Models;

namespace MeuPontoMongoDb.Interface
{
    public interface ISolicitacaoService
    {
        Task<Solicitacao> CriarAsync(Solicitacao solicitacao);
        Task<IEnumerable<Solicitacao>> ListarTodasAsync();
        Task<IEnumerable<Solicitacao>> ListarPorUsuarioAsync(int userId);
        Task AtualizarStatusAsync(int id, StatusSolicitacaoEnum novoStatus);
        Task<Solicitacao> AtualizarSolicitacaoAsync(int id, Solicitacao solicitacao);
        Task<bool> DeletarAsync(int id);

    }
}
