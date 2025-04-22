using MeuPontoMongoDb.Models.Enum;
using MeuPontoMongoDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeuPontoMongoDb.Interface;

namespace MeuPontoMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoController : ControllerBase
    {
        private readonly ISolicitacaoService _solicitacaoService;

        public SolicitacaoController(ISolicitacaoService solicitacaoService)
        {
            _solicitacaoService = solicitacaoService;
        }

        
        // POST: api/solicitacao
        [HttpPost]
        public async Task<IActionResult> CriarSolicitacao([FromBody] Solicitacao solicitacao)
        {
            try
            {
                var novaSolicitacao = await _solicitacaoService.CriarAsync(solicitacao);
                return Ok(novaSolicitacao);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message });
            }
        }

        // GET: api/solicitacao
        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var solicitacoes = await _solicitacaoService.ListarTodasAsync();
            return Ok(solicitacoes);
        }

        // GET: api/solicitacao/usuario/5
        [HttpGet("usuario/{userId}")]
        public async Task<IActionResult> ListarPorUsuario(int userId)
        {
            var solicitacoes = await _solicitacaoService.ListarPorUsuarioAsync(userId);
            return Ok(solicitacoes);
        }

        // PUT: api/solicitacao/aprovar/5
        [HttpPut("aprovar/{id}")]
        public async Task<IActionResult> Aprovar(int id)
        {
            try
            {
                await _solicitacaoService.AtualizarStatusAsync(id, StatusSolicitacaoEnum.Aprovado);
                return Ok(new { message = "Solicitação aprovada com sucesso." });
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message });
            }
        }

        // PUT: api/solicitacao/rejeitar/5
        [HttpPut("rejeitar/{id}")]
        public async Task<IActionResult> Rejeitar(int id)
        {
            try
            {
                await _solicitacaoService.AtualizarStatusAsync(id, StatusSolicitacaoEnum.Rejeitado);
                return Ok(new { message = "Solicitação rejeitada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/solicitacao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Solicitacao solicitacao)
        {
            try
            {
                var atualizada = await _solicitacaoService.AtualizarSolicitacaoAsync(id, solicitacao);
                return Ok(atualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/solicitacao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var sucesso = await _solicitacaoService.DeletarAsync(id);
                if (!sucesso)
                    return NotFound(new { message = "Solicitação não encontrada." });

                return Ok(new { message = "Solicitação excluída com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
