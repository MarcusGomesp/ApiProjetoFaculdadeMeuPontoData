using MeuPontoMongoDb.Database;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.Login;
using MeuPontoMongoDb.Utils;
using Microsoft.EntityFrameworkCore;

namespace MeuPontoMongoDb.Service
{
    public class CadastroService : ICadastroService
    {
        private readonly AppDbContext _context;

        public CadastroService(AppDbContext context)
        {
            _context = context;
        }

        // Listar todos os cadastros
        public async Task<IEnumerable<Cadastro>> ListarTodosAsync()
        {
            return await _context.Cadastros.ToListAsync();
        }

        // Buscar cadastro por ID
        public async Task<Cadastro?> BuscarPorIdAsync(int userId)
        {
            var cadastro = await _context.Cadastros.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cadastro == null)
                throw new Exception($"Cadastro não encontrado para o usuario {userId}");

            return cadastro;
        }

        public async Task<object> BuscarCadastroComUltimoRegistroPorEmailAsync(string email)
        {
            var cadastro = await _context.Cadastros.FirstOrDefaultAsync(c => c.Email == email);
            if (cadastro == null)
                throw new Exception("Cadastro não encontrado.");

            var ultimoRegistro = await _context.Registros
                .Where(r => r.UserId == cadastro.UserId)
                .OrderByDescending(r => r.DataInicio)
                .FirstOrDefaultAsync();

            return new
            {
                cadastro.UserId,
                cadastro.Nome,
                cadastro.Email,
                IdRegistro = ultimoRegistro?.IdRegistro
            };
        }


        // Criar um novo cadastro
        public async Task<Cadastro> CriarAsync(Cadastro cadastro)
        {
            var cadastroExistente = await _context.Cadastros
                .FirstOrDefaultAsync(c => c.CPF == cadastro.CPF || c.Email == cadastro.Email);

            if (cadastroExistente != null)
                throw new Exception("Cadastro já existe com o mesmo CPF ou Email.");

            // Verificar e corrigir o 'Kind' da data antes de salvar no banco
            if (cadastro.BancoHoras != null)
            {
                cadastro.BancoHoras.Data = cadastro.BancoHoras.Data.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(cadastro.BancoHoras.Data, DateTimeKind.Utc) // Garantir que seja UTC
                    : cadastro.BancoHoras.Data.ToUniversalTime();
            }
            cadastro.Senha = PasswordHasher.HashPassword(cadastro.Senha); // Hash da senha antes de salvar

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            // Continuar com a criação do perfil e banco de horas...
            return cadastro;
        }


        //login do usuario
        public async Task<object> LoginAsync(Login login)
        {
            var user = await _context.Cadastros
                .Include(c => c.Perfil) // importante para trazer a imagem!
                .FirstOrDefaultAsync(c => c.Email == login.Email);

            if (user == null)
                throw new Exception("Usuário não encontrado.");

            bool senhaCorreta = PasswordHasher.VerifyPassword(login.Senha, user.Senha);
            if (!senhaCorreta)
                throw new Exception("Senha incorreta.");

            var ultimoRegistro = await _context.Registros
                .Where(r => r.UserId == user.UserId)
                .OrderByDescending(r => r.DataInicio)
                .FirstOrDefaultAsync();

            return new
            {
                mensagem = "Login realizado com sucesso.",
                email = user.Email,
                nome = user.Nome,
                cadastroId = user.UserId,
                idRegistro = ultimoRegistro?.IdRegistro,
                urlProfilePic = user.Perfil?.UrlProfilePic 
            };
        }



        // Deletar cadastro
        public async Task<bool> DeletarAsync(int userId)
        {
            var cadastro = await _context.Cadastros.FindAsync(userId);

            if (cadastro == null)
                return false;

            _context.Cadastros.Remove(cadastro);
            await _context.SaveChangesAsync();

            return true;
        }

      
    }
}
