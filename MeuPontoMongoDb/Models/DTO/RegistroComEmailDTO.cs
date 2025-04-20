namespace MeuPontoMongoDb.Models.DTO
{
    public class RegistroComEmailDTO
    {
        public string Email { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime? SaidaAlmoco { get; set; }
        public DateTime? VoltaAlmoco { get; set; }
        public DateTime? Fim { get; set; }
    }
}
