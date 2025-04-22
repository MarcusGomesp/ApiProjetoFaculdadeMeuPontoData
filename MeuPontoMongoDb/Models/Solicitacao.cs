using MeuPontoMongoDb.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeuPontoMongoDb.Models
{
    [Table("tab_solicitacao")]
    public class Solicitacao
    {
        [Key]
        [Column("id_solicitacao")]
        public int IdSolicitacao { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }


        [Column("id_registro")]
        public int IdRegistro { get; set; }


        [Column("horario")]
        public TimeSpan Horario { get; set; }

        [Column("status")]
        public StatusSolicitacaoEnum Status { get; set; }


        [Column("observacao")]
        public string? Observacao { get; set; }


        [ForeignKey("UserId")]
        [JsonIgnore]
        public Cadastro? Usuario { get; set; }


        [ForeignKey("IdRegistro")]
        [JsonIgnore]
        public Registro? Registro { get; set; }
    }

}
