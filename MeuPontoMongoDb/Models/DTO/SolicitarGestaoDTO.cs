using MeuPontoMongoDb.Models.Enum;

namespace MeuPontoMongoDb.Models.DTO
{
    public class SolicitarGestaoDTO
    {
        public int IdSolicitacao { get; set; }
        public string NomeUsuario { get; set; } = "";
        public TimeSpan Horario { get; set; }
        public string? Observacao { get; set; }
        public StatusSolicitacaoEnum Status { get; set; }
    }
}
