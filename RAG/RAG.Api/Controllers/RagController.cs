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
        private readonly IContratoBaseHandler<ObterDocumentoPorTituloRequest, ResultadoOperacao> _ObterDocumentoPorTituloHandler;
        private readonly IContratoBaseHandler<ObterDocumentoPorTipoArquivoRequest, ResultadoOperacao> _ObterDocumentoPorTipoArquivoHandler;
        private readonly IContratoBaseHandler<ObterDocumentoPorTrechoRequest, ResultadoOperacao> _ObterDocumentoPorTrechoHandler;
        private readonly IContratoBaseHandler<ObterDocumentoPorPalavraRequest, ResultadoOperacao> _ObterDocumentoPorPalavraHandler;
        public RagController(
            IDocumentoCommandRepositorio iDocumentoCommandRepositorio,
            IContratoBaseHandler<ObterTodosDocumentoRequest, ResultadoOperacao> obterTodosDocumentoHandler,
            IContratoBaseHandler<ImportarDocumentoRequest, ResultadoOperacao> importarDocumentoHandler,
            IContratoBaseHandler<ObterDocumentoPorTituloRequest, ResultadoOperacao> obterDocumentoPorTituloHandler,
            IContratoBaseHandler<ObterDocumentoPorTipoArquivoRequest, ResultadoOperacao> obterDocumentoPorTipoArquivoHandler,
            IContratoBaseHandler<ObterDocumentoPorTrechoRequest, ResultadoOperacao> obterDocumentoPorTrechoHandler,
            IContratoBaseHandler<ObterDocumentoPorPalavraRequest, ResultadoOperacao> obterDocumentoPorPalavraHandler
            )

        {
            _IDocumentoCommandRepositorio = iDocumentoCommandRepositorio;
            _ObterTodosDocumentoHandler = obterTodosDocumentoHandler;
            _ImportarDocumentoHandler = importarDocumentoHandler;
            _ObterDocumentoPorTituloHandler = obterDocumentoPorTituloHandler;
            _ObterDocumentoPorTipoArquivoHandler = obterDocumentoPorTipoArquivoHandler;
            _ObterDocumentoPorTrechoHandler = obterDocumentoPorTrechoHandler;
            _ObterDocumentoPorPalavraHandler = obterDocumentoPorPalavraHandler;
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

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo([FromQuery] ObterDocumentoPorTituloRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _ObterDocumentoPorTituloHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorTipoArquivo")]
        public async Task<IActionResult> ObterPorTipoArquivo([FromQuery] ObterDocumentoPorTipoArquivoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _ObterDocumentoPorTipoArquivoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorTrecho")]
        public async Task<IActionResult> ObterPorTrecho([FromQuery] ObterDocumentoPorTrechoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _ObterDocumentoPorTrechoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorPalavra")]
        public async Task<IActionResult> ObterPorPalavra([FromQuery] ObterDocumentoPorPalavraRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _ObterDocumentoPorPalavraHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        private async Task<string> LerTextoArquivoAsync(IFormFile arquivo)
        {
            using var reader = new StreamReader(arquivo.OpenReadStream());
            return await reader.ReadToEndAsync();
        }
    }
}
