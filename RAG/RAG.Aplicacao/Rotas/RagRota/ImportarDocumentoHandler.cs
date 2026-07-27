using Microsoft.AspNetCore.Http;
using RAG.Aplicacao.Util;
using RAG.Dominio.Entidade;
using RAG.Dominio.InterfaceRepositorio;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ImportarDocumentoHandler : IContratoBaseHandler<ImportarDocumentoRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;
 
        public ImportarDocumentoHandler(IDocumentoCommandRepositorio documentoRepositorio )
        {
            _documentoRepositorio = documentoRepositorio;
 
        }

        public async Task<ResultadoOperacao> Executar(ImportarDocumentoRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                IFormFile arquivo = request.Arquivo;

                if (arquivo == null || arquivo.Length == 0)
                    return ResultadoOperacao.GerarErro("Nenhum arquivo enviado.", 400);

                // Extrair texto do arquivo (exemplo simples para TXT)
                string texto;
                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    texto = await reader.ReadToEndAsync();
                }

                var titulo = Path.GetFileNameWithoutExtension(arquivo.FileName);
                var tipoArquivo = Path.GetExtension(arquivo.FileName).Trim('.');
                var tamanhoArquivo = arquivo.Length;
                Documento? documento = Documento.Criar(titulo, texto: texto, tipoArquivo, tamanhoArquivo);

                await _documentoRepositorio.IncluirAsync(documento, cancellationToken);




                ImportarDocumentoResponse response = ImportarDocumentoResponse.Criar(documento);
                return ResultadoOperacao.GerarSucesso(response);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.GerarErro($"Erro interno: {ex.Message}", 500);
            }
        }
    }
}
