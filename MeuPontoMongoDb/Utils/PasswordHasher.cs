using BCrypt.Net;

namespace MeuPontoMongoDb.Utils
{
    public class PasswordHasher
    {


        //criptografia de Senha
        public static string HashPassword(string password)
        {
          return BCrypt.Net.BCrypt.HashPassword(password);  
        }

        //verifica se a senha é igual a senha criptografada
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
