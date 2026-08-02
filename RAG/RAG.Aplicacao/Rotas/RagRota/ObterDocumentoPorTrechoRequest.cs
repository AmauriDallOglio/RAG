using RAG.Aplicacao.Util;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorTrechoRequest : IRequest<ResultadoOperacao>
    {
        public string Trecho { get; set; } = string.Empty;
    }
}
