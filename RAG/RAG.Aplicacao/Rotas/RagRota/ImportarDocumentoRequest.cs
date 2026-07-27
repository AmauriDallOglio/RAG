using RAG.Aplicacao.Util;
using Microsoft.AspNetCore.Http;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ImportarDocumentoRequest : IRequest<ResultadoOperacao>
    {
        public IFormFile Arquivo { get; set; } = default!;

    }
}
