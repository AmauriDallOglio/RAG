using RAG.Aplicacao.Util;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorPalavraRequest : IRequest<ResultadoOperacao>
    {
        public string Palavra { get; set; } = string.Empty;
    }
}
