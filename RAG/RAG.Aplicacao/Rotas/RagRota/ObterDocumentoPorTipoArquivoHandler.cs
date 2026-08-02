using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorTipoArquivoHandler : IContratoBaseHandler<ObterDocumentoPorTipoArquivoRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ObterDocumentoPorTipoArquivoHandler(IDocumentoCommandRepositorio documentoRepositorio)
        {
            _documentoRepositorio = documentoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterDocumentoPorTipoArquivoRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.TipoArquivo))
                    return ResultadoOperacao.GerarErro("O tipo de arquivo deve ser informado.", 400);

                var documentos = await _documentoRepositorio.ObterTodosComEstruturaAsync(cancellationToken);
                var resultado = documentos
                    .Where(d => d.TipoArquivo != null && d.TipoArquivo.Contains(request.TipoArquivo, StringComparison.OrdinalIgnoreCase))
                    .Select(d => new DocumentoResumoResponse
                    {
                        Titulo = d.Titulo,
                        TipoArquivo = d.TipoArquivo,
                        DataImportacao = d.DataImportacao
                    })
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
