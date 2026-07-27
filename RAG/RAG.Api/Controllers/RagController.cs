using Microsoft.AspNetCore.Mvc;
using RAG.Aplicacao.Rotas.RagRota;
using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RagController : ControllerBase
    {
        private readonly IDocumentoCommandRepositorio _IDocumentoCommandRepositorio;
        private readonly IContratoBaseHandler<ObterTodosDocumentoRequest, ResultadoOperacao> _ObterTodosDocumentoHandler;
        private readonly IContratoBaseHandler<ImportarDocumentoRequest, ResultadoOperacao> _ImportarDocumentoHandler;
        public RagController(
            IDocumentoCommandRepositorio iDocumentoCommandRepositorio,
            IContratoBaseHandler<ObterTodosDocumentoRequest, ResultadoOperacao> obterTodosDocumentoHandler,
            IContratoBaseHandler<ImportarDocumentoRequest, ResultadoOperacao> importarDocumentoHandler
            )

        {
            _IDocumentoCommandRepositorio = iDocumentoCommandRepositorio;
            _ObterTodosDocumentoHandler = obterTodosDocumentoHandler;
            _ImportarDocumentoHandler = importarDocumentoHandler;
        }

        //[Authorize(Policy = "ollama.write")]
        [HttpPost("ImportarDocumento")]
        public async Task<IActionResult> ImportarDocumento([FromForm] ImportarDocumentoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _ImportarDocumentoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }


        //[Authorize(Policy = "ollama.read")]
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos([FromQuery] ObterTodosDocumentoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _ObterTodosDocumentoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        private async Task<string> LerTextoArquivoAsync(IFormFile arquivo)
        {
            using var reader = new StreamReader(arquivo.OpenReadStream());
            return await reader.ReadToEndAsync();
        }
    }
}
