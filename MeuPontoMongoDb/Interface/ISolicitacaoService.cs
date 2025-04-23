using MeuPontoMongoDb.Models.Enum;
using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.DTO;

namespace MeuPontoMongoDb.Interface
{
    public interface ISolicitacaoService
    {
        Task<Solicitacao> CriarAsync(Solicitacao solicitacao);
        Task<IEnumerable<Solicitacao>> ListarTodasAsync();
        Task<IEnumerable<Solicitacao>> ListarPorUsuarioAsync(int userId);
        Task<IEnumerable<SolicitarGestaoDTO>> ListarGestaoAsync(); //
        Task AtualizarStatusAsync(int id, StatusSolicitacaoEnum novoStatus);
        Task<Solicitacao> AtualizarSolicitacaoAsync(int id, Solicitacao solicitacao);
        Task<bool> DeletarAsync(int id);

    }
}
