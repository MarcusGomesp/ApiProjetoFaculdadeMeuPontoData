using MeuPontoMongoDb.Database;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuPontoMongoDb.Service
{
    public class PerfilService : IPerfilService
    {
        private readonly AppDbContext _context;

        public PerfilService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Perfil>> ListarTodosAsync()
        {
            return await _context.Perfis.ToListAsync();
        }

        public Task<Perfil?> BuscarPorIdAsync(int userId)
        {
            var perfil = _context.Perfis.FirstOrDefaultAsync(p => p.UserId == userId);
            if (perfil == null)
                throw new Exception($"Perfil não encontrado para o usuario {userId}");
            return perfil;
        }


        public async Task<string> CriarOuAtualizarImagemPerfilAsync(int userId, string imagemBase64)
        {
            var perfil = await _context.Perfis.FirstOrDefaultAsync(p => p.UserId == userId);

            if (perfil == null)
            {
                perfil = new Perfil
                {
                    UserId = userId,
                    UrlProfilePic = imagemBase64
                };
                _context.Perfis.Add(perfil);
            }
            else
            {
                perfil.UrlProfilePic = imagemBase64;
                _context.Perfis.Update(perfil);
            }

            await _context.SaveChangesAsync();
            return perfil.UrlProfilePic;
        }

        public async Task<bool> DeletarImagemPerfilAsync(int userId)
        {
            var perfil = await _context.Perfis.FindAsync(userId);
            
            if (perfil == null)
                return false;

            _context.Perfis.Remove(perfil);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
