using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeuPontoMongoDb.Models
{
    [Table("tab_cadastro")]
    public class Cadastro
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("cargo")]
        public string Cargo { get; set; }

        [Column("departamento")]
        public string Departamento { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("cpf")]
        public long CPF { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [JsonIgnore]
        public Perfil? Perfil { get; set; }

        [JsonIgnore]
        public ICollection<Registro>? Registros { get; set; }

        [JsonIgnore]
        public ICollection<Solicitacao>? Solicitacoes { get; set; }

        [JsonIgnore]
        public BancoHoras? BancoHoras { get; set; }

    }

}
