using RAG.Aplicacao.Util;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterDocumentoPorTipoArquivoRequest : IRequest<ResultadoOperacao>
    {
        public string TipoArquivo { get; set; } = string.Empty;
    }
}
