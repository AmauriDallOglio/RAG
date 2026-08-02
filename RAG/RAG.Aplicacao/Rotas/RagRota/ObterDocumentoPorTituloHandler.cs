using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorTituloHandler : IContratoBaseHandler<ObterDocumentoPorTituloRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ObterDocumentoPorTituloHandler(IDocumentoCommandRepositorio documentoRepositorio)
        {
            _documentoRepositorio = documentoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterDocumentoPorTituloRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Titulo))
                    return ResultadoOperacao.GerarErro("O título deve ser informado.", 400);

                var documentos = await _documentoRepositorio.ObterTodosComEstruturaAsync(cancellationToken);
                var resultado = documentos
                    .Where(d => d.Titulo.Contains(request.Titulo, StringComparison.OrdinalIgnoreCase))
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
