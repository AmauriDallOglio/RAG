using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorTrechoHandler : IContratoBaseHandler<ObterDocumentoPorTrechoRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ObterDocumentoPorTrechoHandler(IDocumentoCommandRepositorio documentoRepositorio)
        {
            _documentoRepositorio = documentoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterDocumentoPorTrechoRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Trecho))
                    return ResultadoOperacao.GerarErro("O trecho deve ser informado.", 400);

                var documentos = await _documentoRepositorio.ObterTodosComEstruturaAsync(cancellationToken);
                var resultado = documentos
                    .SelectMany(d => d.Trechos
                        .Where(t => t.Frase.Contains(request.Trecho, StringComparison.OrdinalIgnoreCase))
                        .Select(t => new DocumentoTrechoResponse
                        {
                            Titulo = d.Titulo,
                            TipoArquivo = d.TipoArquivo,
                            DataImportacao = d.DataImportacao,
                            Trecho = t.Frase
                        }))
                    .ToList();

                return ResultadoOperacao.GerarSucesso(resultado);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.GerarErro($"Erro interno: {ex.Message}", 500);
            }
        }
    }
}
