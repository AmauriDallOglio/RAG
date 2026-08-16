using Microsoft.AspNetCore.Mvc;
using RAG.Aplicacao.Rotas.RagRota;
using RAG.Infraestrutura.Repositorio;

namespace RAG.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RagController : ControllerBase
    {

        private readonly DocumentoCommandRepositorio _documentoCommandRepositorio;
        private readonly ObterTodosDocumentoHandler _obterTodosDocumentoHandler;
        private readonly ImportarDocumentoHandler _importarDocumentoHandler;
        private readonly ImportarTextoHandler _importarTextoHandler;
        private readonly ObterDocumentoPorTituloHandler _obterDocumentoPorTituloHandler;
        private readonly ObterDocumentoPorTipoArquivoHandler _obterDocumentoPorTipoArquivoHandler;
        private readonly ObterDocumentoPorTrechoHandler _obterDocumentoPorTrechoHandler;
        private readonly ObterDocumentoPorPalavraHandler _obterDocumentoPorPalavraHandler;

        public RagController(
            DocumentoCommandRepositorio documentoCommandRepositorio,
            ObterTodosDocumentoHandler obterTodosDocumentoHandler,
            ImportarDocumentoHandler importarDocumentoHandler,
            ImportarTextoHandler importarTextoHandler,
            ObterDocumentoPorTituloHandler obterDocumentoPorTituloHandler,
            ObterDocumentoPorTipoArquivoHandler obterDocumentoPorTipoArquivoHandler,
            ObterDocumentoPorTrechoHandler obterDocumentoPorTrechoHandler,
            ObterDocumentoPorPalavraHandler obterDocumentoPorPalavraHandler)
        {
            _documentoCommandRepositorio = documentoCommandRepositorio;
            _obterTodosDocumentoHandler = obterTodosDocumentoHandler;
            _importarDocumentoHandler = importarDocumentoHandler;
            _importarTextoHandler = importarTextoHandler;
            _obterDocumentoPorTituloHandler = obterDocumentoPorTituloHandler;
            _obterDocumentoPorTipoArquivoHandler = obterDocumentoPorTipoArquivoHandler;
            _obterDocumentoPorTrechoHandler = obterDocumentoPorTrechoHandler;
            _obterDocumentoPorPalavraHandler = obterDocumentoPorPalavraHandler;
        }

        //[Authorize(Policy = "ollama.write")]
        [HttpPost("ImportarDocumento")]
        public async Task<IActionResult> ImportarDocumento([FromForm] ImportarDocumentoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _importarDocumentoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpPost("ImportarTexto")]
        public async Task<IActionResult> ImportarTexto([FromBody] ImportarTextoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _importarTextoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }


        //[Authorize(Policy = "ollama.read")]
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos([FromQuery] ObterTodosDocumentoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _obterTodosDocumentoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo([FromQuery] ObterDocumentoPorTituloRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _obterDocumentoPorTituloHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorTipoArquivo")]
        public async Task<IActionResult> ObterPorTipoArquivo([FromQuery] ObterDocumentoPorTipoArquivoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _obterDocumentoPorTipoArquivoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorTrecho")]
        public async Task<IActionResult> ObterPorTrecho([FromQuery] ObterDocumentoPorTrechoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _obterDocumentoPorTrechoHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("ObterPorPalavra")]
        public async Task<IActionResult> ObterPorPalavra([FromQuery] ObterDocumentoPorPalavraRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _obterDocumentoPorPalavraHandler.Executar(request, cancellationToken);
            return Ok(resultado);
        }

        private async Task<string> LerTextoArquivoAsync(IFormFile arquivo)
        {
            using var reader = new StreamReader(arquivo.OpenReadStream());
            return await reader.ReadToEndAsync();
        }
    }
}
