using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorPalavraHandler : IContratoBaseHandler<ObterDocumentoPorPalavraRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ObterDocumentoPorPalavraHandler(IDocumentoCommandRepositorio documentoRepositorio)
        {
            _documentoRepositorio = documentoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterDocumentoPorPalavraRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Palavra))
                    return ResultadoOperacao.GerarErro("A palavra deve ser informada.", 400);

                var documentos = await _documentoRepositorio.ObterTodosComEstruturaAsync(cancellationToken);
                var resultado = documentos
                    .SelectMany(d => d.Trechos
                        .SelectMany(t => t.Palavras
                            .Where(p => p.Palavra.Contains(request.Palavra, StringComparison.OrdinalIgnoreCase))
                            .Select(p => new DocumentoPalavraResponse
                            {
                                Titulo = d.Titulo,
                                TipoArquivo = d.TipoArquivo,
                                DataImportacao = d.DataImportacao,
                                Trecho = t.Frase,
                                Palavra = p.Palavra,
                                Quantidade = p.Quantidade
                            })))
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
