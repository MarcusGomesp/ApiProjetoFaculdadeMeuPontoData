using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.DTO;
using MeuPontoMongoDb.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeuPontoMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroService _registroService;

        public RegistroController(IRegistroService registroService)
        {
            _registroService = registroService;
        }


        // GET: api/registro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistros()
        {
            var registros = await _registroService.ListarTodosAsync();
            return Ok(registros);
        }

        // GET: api/registro/id
        [HttpGet("usuario/{userId}")]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistrosPorUsuario(int userId)
        {
            var registros = await _registroService.BuscarTodosPorUsuarioAsync(userId);

            if (registros == null || !registros.Any())
            {
                return NotFound($"Nenhum registro encontrado para o usuário com ID {userId}");
            }

            return Ok(registros);
        }


        [HttpGet("buscar-id-registro-por-email")]
        public async Task<IActionResult> BuscarIdRegistroPorEmail([FromQuery] string email)
        {
            try
            {
                var idRegistro = await _registroService.BuscarIdRegistroPorEmailAsync(email);
                return Ok(new { idRegistro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }




        // POST: api/registro
        [HttpPost]
        public async Task<ActionResult<Registro>> PostRegistro([FromBody] Registro registro)
        {
            try
            {
                var criadoRegistro = await _registroService.CriarAsync(registro);

                return Ok(new { IdRegistro = criadoRegistro.IdRegistro });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message = innerMessage });
            }
        }

        // PUT: api/registro/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Registro>> PutRegistro(int id, [FromBody] Registro registro)
        {
            try
            {
                var atualizadoRegistro = await _registroService.AtualizarAsync(id, registro);
                return Ok(atualizadoRegistro);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message = innerMessage });
            }
        }

        // DELETE: api/registro/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            try
            {
                var resultado = await _registroService.DeletarAsync(id);
                if (resultado)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message = innerMessage });
            }
        }
    }
}
