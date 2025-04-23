using MeuPontoMongoDb.Models.Enum;
using MeuPontoMongoDb.Models;
using Microsoft.EntityFrameworkCore;
using MeuPontoMongoDb.Database;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models.DTO;

namespace MeuPontoMongoDb.Service
{
    public class SolicitacaoService : ISolicitacaoService
    {
        private readonly AppDbContext _context;

        public SolicitacaoService(AppDbContext context)
        {
            _context = context;
        }

        // Criar solicitações
        public async Task<Solicitacao> CriarAsync(Solicitacao solicitacao)
        {
            solicitacao.Status = StatusSolicitacaoEnum.Pendente;
            _context.Solicitacoes.Add(solicitacao);
            await _context.SaveChangesAsync();
            return solicitacao;
        }

        // Listar todas as solicitações
        public async Task<IEnumerable<Solicitacao>> ListarTodasAsync()
        {
            return await _context.Solicitacoes
                .Include(s => s.Usuario)
                .Include(s => s.Registro)
                .ToListAsync();
        }

        // Listar solicitações por usuárioId
        public async Task<IEnumerable<Solicitacao>> ListarPorUsuarioAsync(int userId)
        {
            return await _context.Solicitacoes
                .Where(s => s.UserId == userId)
                .Include(s => s.Registro)
                .ToListAsync();
        }

        // Listar solicitações para gestão
        public async Task<IEnumerable<SolicitarGestaoDTO>> ListarGestaoAsync()
        {
            return await _context.Solicitacoes
                .Include(s => s.Usuario)
                .Select(s => new SolicitarGestaoDTO
                {
                    IdSolicitacao = s.IdSolicitacao,
                    NomeUsuario = s.Usuario != null ? s.Usuario.Nome : "Desconhecido",
                    Horario = s.Horario,
                    Observacao = s.Observacao,
                    Status = s.Status
                })
                .ToListAsync();
        } //

        // Atualizar status da solicitação
        public async Task AtualizarStatusAsync(int id, StatusSolicitacaoEnum novoStatus)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
                throw new Exception("Solicitação não encontrada.");

            solicitacao.Status = novoStatus;
            _context.Entry(solicitacao).State = EntityState.Modified;

            if (novoStatus == StatusSolicitacaoEnum.Aprovado)
            {
                if (solicitacao.Horario < TimeSpan.Zero || solicitacao.Horario >= TimeSpan.FromHours(24))
                    throw new Exception($"O horário informado está fora do intervalo permitido (00:00 até 23:59). Valor: {solicitacao.Horario}");

                var registro = await _context.Registros.FindAsync(solicitacao.IdRegistro);
                if (registro != null)
                {
                    if (registro.DataInicio == DateTime.MinValue)
                        throw new Exception("Data de início do registro está inválida.");

                    var novaData = registro.DataInicio.Date.Add(solicitacao.Horario);
                    registro.DataInicio = novaData;

                    registro.CalcularHorasExtras();
                    _context.Entry(registro).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
        }

        // Atualizar solicitação
        public async Task<Solicitacao> AtualizarSolicitacaoAsync(int id, Solicitacao solicitacaoAtualizada)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
                throw new Exception("Solicitação não encontrada.");

            if (solicitacao.Status != StatusSolicitacaoEnum.Pendente)
                throw new Exception("Apenas solicitações pendentes podem ser editadas.");

            solicitacao.Horario = solicitacaoAtualizada.Horario;
            solicitacao.Observacao = solicitacaoAtualizada.Observacao;

            _context.Entry(solicitacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return solicitacao;
        }

        // Deletar solicitação
        public async Task<bool> DeletarAsync(int id)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao == null) return false;

            if (solicitacao.Status != StatusSolicitacaoEnum.Pendente)
                throw new Exception("Apenas solicitações pendentes podem ser excluídas.");

            _context.Solicitacoes.Remove(solicitacao);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
