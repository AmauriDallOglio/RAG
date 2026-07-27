using RAG.Aplicacao.Util;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterTodosDocumentoRequest : IRequest<ResultadoOperacao>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}