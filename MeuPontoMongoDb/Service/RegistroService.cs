using MeuPontoMongoDb.Database;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.DTO;
using MeuPontoMongoDb.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TimeZoneConverter;

namespace MeuPontoMongoDb.Service
{
    public class RegistroService : IRegistroService
    {
        private readonly AppDbContext _context;
        public RegistroService(AppDbContext context)
        {
            _context = context;
        }


        // Listar todos os registros
        public async Task<IEnumerable<Registro>> ListarTodosAsync()
        {
            return await _context.Registros
                    .Include(r => r.Usuario)
                    .ThenInclude(u => u.Perfil)
                    .ToListAsync();
        }

        // Listar por ID
        public async Task<List<Registro>> BuscarTodosPorUsuarioAsync(int userId)
        {
            return await _context.Registros
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.DataInicio)
                .ToListAsync();
        }

        // Buscar ID do registro por e-mail
        public async Task<int?> BuscarIdRegistroPorEmailAsync(string email)
        {
            var usuario = await _context.Cadastros.FirstOrDefaultAsync(c => c.Email == email);

            if (usuario == null)
                throw new Exception("Usuário não encontrado com esse e-mail.");

            var registro = await _context.Registros
                .Where(r => r.UserId == usuario.UserId)
                .OrderByDescending(r => r.DataInicio) 
                .FirstOrDefaultAsync();

            if (registro == null)
                throw new Exception("Registro não encontrado para esse usuário.");

            return registro.IdRegistro;
        }  

        // Criar um novo registro
        public async Task<Registro> CriarAsync(Registro registro)
        {

            registro.DataInicio = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.DataInicio);
            if (registro.SaidaAlmoco.HasValue)
                registro.SaidaAlmoco = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.SaidaAlmoco.Value);

            if (registro.VoltaAlmoco.HasValue)
                registro.VoltaAlmoco = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.VoltaAlmoco.Value);

            if (registro.Fim.HasValue)
                registro.Fim = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.Fim.Value);

            registro.CalcularHorasExtras(); 

            await _context.Registros.AddAsync(registro);
            await _context.SaveChangesAsync();

            return registro;

        }

        public async Task<TimeSpan> CalcularTotalHorasExtrasPorUsuarioAsync(int userId)
        {
            var registros = await _context.Registros
                .Where(r => r.UserId == userId && r.HorarioExtra.HasValue)
                .ToListAsync();

            var total = TimeSpan.Zero;

            foreach (var registro in registros)
            {
                total += registro.HorarioExtra ?? TimeSpan.Zero;
            }

            return total;
        }


        // Atualizar um registro existente
        public async Task<Registro> AtualizarAsync(int id, Registro registro)
        {
            var pontoExistente = await _context.Registros.FindAsync(id);
            if (pontoExistente == null)
                throw new Exception("Registro não encontrado.");

            pontoExistente.DataInicio = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.DataInicio);

            if (registro.SaidaAlmoco.HasValue)
                pontoExistente.SaidaAlmoco = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.SaidaAlmoco.Value);

            if (registro.VoltaAlmoco.HasValue)
                pontoExistente.VoltaAlmoco = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.VoltaAlmoco.Value);

            if (registro.Fim.HasValue)
            {
                pontoExistente.Fim = CalculoTrabalho.ConverterParaHorarioBrasilia(registro.Fim.Value);
                pontoExistente.CalcularHorasExtras();
            }

            pontoExistente.QtdeBatidas = registro.QtdeBatidas;

            _context.Entry(pontoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return pontoExistente;
        }

        // Deletar um registro
        public async Task<bool> DeletarAsync(int id)
        {
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
                return false;

            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}