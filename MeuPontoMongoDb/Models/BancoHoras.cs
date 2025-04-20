using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace MeuPontoMongoDb.Models
{

    [Table("tab_banco_horas")]
    public class BancoHoras
    {
        [Key]
        [Column("user_id")]
        public int UserId{ get; set; }

        [Column("data")]
        public DateTime Data { get; set; }

        [Column("total_horas_trabalhadas")]
        public TimeSpan? TotalHorasTrabalhadas{ get; set; }

        [Column("qtd_solicitacoes_pendentes")]
        public int QtdSolicitacoesPendentes { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public Cadastro? Usuario { get; set; }
    }
}
