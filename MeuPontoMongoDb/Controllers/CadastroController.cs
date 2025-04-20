using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models;
using MeuPontoMongoDb.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPontoMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroService _cadastroService;

        public CadastroController(ICadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        // GET: api/cadastro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadastro>>> GetCadastros()
        {
            var cadastros = await _cadastroService.ListarTodosAsync();
            return Ok(cadastros);
        }

        // GET: api/cadastro/5
        [HttpGet("usuario/{userId}")]
        public async Task<ActionResult<Cadastro>> GetCadastro(int userId)
        {
            var cadastro = await _cadastroService.BuscarPorIdAsync(userId);

            if (cadastro == null)
            {
                return NotFound();
            }

            return Ok(cadastro);
        }


        [HttpGet("usuario")]
        public async Task<ActionResult> GetCadastroPorEmail([FromQuery] string email)
        {
            try
            {
                var resultado = await _cadastroService.BuscarCadastroComUltimoRegistroPorEmailAsync(email);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao carregar cadastro: {e.Message}");
            }
        }


        //POST: api/cadastro
        [HttpPost]
        public async Task<ActionResult<Cadastro>> PostCadastro([FromBody] Cadastro cadastro)
        {
            try
            {
                var criadoCadastro = await _cadastroService.CriarAsync(cadastro);
                return CreatedAtAction(nameof(GetCadastro), new { userId = criadoCadastro.UserId }, criadoCadastro);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;

                return BadRequest(new { message = innerMessage });
            }
        }

        // POST: api/cadastro/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            try
            {
                var result = await _cadastroService.LoginAsync(login);

                if (result == null)
                {
                    throw new Exception("Login ou senha inválidos.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro ao logar: {e.Message}");
            }
        }



        //// PUT: api/cadastro/5
        //[HttpPut("{userId}")]
        //public async Task<IActionResult> PutCadastro(int userId, Cadastro cadastro)
        //{
        //    if (userId != cadastro.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    var atualizado = await _cadastroService.AtualizarAsync(cadastro);

        //    if (!atualizado)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        // DELETE: api/cadastro/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCadastro([FromRoute] int userId)
        {
            var deletado = await _cadastroService.DeletarAsync(userId);

            if (!deletado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

