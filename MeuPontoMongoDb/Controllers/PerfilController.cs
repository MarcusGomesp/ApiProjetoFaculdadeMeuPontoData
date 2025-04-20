using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models.Imagem;
using MeuPontoMongoDb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeuPontoMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly IPerfilService _perfilService;
        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }


        [HttpGet]
        public async Task<IActionResult> ObterPerfil()
        {
            try
            {
                var perfil = await _perfilService.ListarTodosAsync();
                return Ok(perfil);
            }
            catch (Exception e)
            {
                return BadRequest(new { mensagem = $"Erro ao obter perfil: {e.Message}" });
            }
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> ObterPerfilPorId(int userId)
        {
            try
            {
                var perfil = await _perfilService.BuscarPorIdAsync(userId);
                if (perfil == null)
                    return NotFound(new { mensagem = "Perfil não encontrado." });
                return Ok(perfil);
            }
            catch (Exception e)
            {
                return BadRequest(new { mensagem = $"Erro ao obter perfil: {e.Message}" });
            }
        }

        [HttpPost("Imagem/{userId}")]
        public async Task<IActionResult> CriarOuAtualizarImagemPerfil(int userId, [FromBody] ImagemUploadDto dto)
        {
            try
            {
                var url = await _perfilService.CriarOuAtualizarImagemPerfilAsync(userId, dto.urlImagem);
                return Ok(new { mensagem = "Imagem de perfil salva com sucesso.", url });

            }
            catch (Exception e)
            {

                return BadRequest(new { mensagem = $"Erro ao salvar imagem: {e.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarImagemPerfil(int id)
        {
            try
            {
                var resultado = await _perfilService.DeletarImagemPerfilAsync(id);
                if (resultado)
                    return Ok(new { mensagem = "Imagem de perfil deletada com sucesso." });
                return NotFound(new { mensagem = "Perfil não encontrado." });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensagem = $"Erro ao deletar imagem: {e.Message}" });
            }
        }
    }
}
