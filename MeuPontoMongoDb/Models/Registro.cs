using MeuPontoMongoDb.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeuPontoMongoDb.Models
{
    [Table("tab_registro")]
    public class Registro
    {
        [Key]
        [Column("id_registro")]
        public int IdRegistro { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; } = CalculoTrabalho.ObterHorarioBrasilia();

        [Column("saida_almoco")]
        public DateTime? SaidaAlmoco { get; set; }

        [Column("volta_almoco")]
        public DateTime? VoltaAlmoco { get; set; }

        [Column("fim")]
        public DateTime? Fim { get; set; }
        
        [Column("total_hora")]
        public TimeSpan? TotalHora { get; set; } //total de horas trabalhadas no dia
       
        [Column("hora_extra")]
        public TimeSpan? HorarioExtra { get; set; }

        [Column("qtde_batidas")]
        public int QtdeBatidas { get; set; } //batidas 
        
        [ForeignKey("UserId")]
        [JsonIgnore]
        public Cadastro? Usuario { get; set; }

        [JsonIgnore]
        public ICollection<Solicitacao>? Solicitacoes { get; set; }


        public void CalcularHorasExtras()
        {
            if (DataInicio != null && Fim != null)
            {
                TimeSpan total;

                if (SaidaAlmoco.HasValue && VoltaAlmoco.HasValue)
                {
                    total = (Fim.Value - DataInicio) - (VoltaAlmoco.Value - SaidaAlmoco.Value);
                }
                else
                {
                    total = Fim.Value -DataInicio; 
                }

                TotalHora = total;

                TimeSpan jornadaNormal = TimeSpan.FromHours(8);
                HorarioExtra = total > jornadaNormal ? total - jornadaNormal : TimeSpan.Zero;
            }
        }
    }

}
