using System.Text;
using RAG.Aplicacao.Util;
using RAG.Dominio.Entidade;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ImportarTextoHandler : IContratoBaseHandler<ImportarTextoRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ImportarTextoHandler(IDocumentoCommandRepositorio documentoRepositorio)
        {
            _documentoRepositorio = documentoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ImportarTextoRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Texto))
                {
                    return ResultadoOperacao.GerarErro("Nenhum texto foi informado.", 400);
                }

                var titulo = string.IsNullOrWhiteSpace(request.Titulo)
                    ? $"texto-{DateTime.Now:yyyyMMddHHmmss}"
                    : request.Titulo.Trim();

                var tamanhoArquivo = Encoding.UTF8.GetByteCount(request.Texto);
                Documento documento = Documento.Criar(titulo, request.Texto, "txt", tamanhoArquivo);

                await _documentoRepositorio.IncluirAsync(documento, cancellationToken);

                ImportarDocumentoResponse response = ImportarDocumentoResponse.Criar(documento);
                return ResultadoOperacao.GerarSucesso(response, "Texto importado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.GerarErro($"Erro interno: {ex.Message}", 500);
            }
        }
    }
}
