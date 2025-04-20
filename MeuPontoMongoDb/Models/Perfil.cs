using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeuPontoMongoDb.Models
{
    [Table("tab_perfil")]
    public class Perfil
    {
        [Key, ForeignKey("Usuario")]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("url_profile_pic")]
        public string UrlProfilePic { get; set; } = string.Empty;

        [JsonIgnore]
        public Cadastro? Usuario { get; set; }
    }

}
