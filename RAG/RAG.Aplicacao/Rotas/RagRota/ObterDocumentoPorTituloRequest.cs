using RAG.Aplicacao.Util;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorTituloRequest : IRequest<ResultadoOperacao>
    {
        public string Titulo { get; set; } = string.Empty;
    }
}
