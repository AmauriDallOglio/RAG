using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterTodosDocumentoHandler : IContratoBaseHandler<ObterTodosDocumentoRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ObterTodosDocumentoHandler(IDocumentoCommandRepositorio documentoRepositorio)
        {
            _documentoRepositorio = documentoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterTodosDocumentoRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                int page = request.Page < 1 ? 1 : request.Page;
                int pageSize = request.PageSize < 1 ? 20 : request.PageSize;
                var documentos = await _documentoRepositorio.ObterPaginadoAsync(page, pageSize, cancellationToken);
                var response = ObterTodosDocumentoResponse.CriarLista(documentos, page, pageSize);

                return ResultadoOperacao.GerarSucesso(response);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.GerarErro($"Erro interno: {ex.Message}", 500);
            }
        }
    }
}
