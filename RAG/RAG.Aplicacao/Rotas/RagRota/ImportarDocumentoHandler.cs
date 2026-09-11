using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using RAG.Aplicacao.Util;
using RAG.Dominio.Entidade;
using RAG.Dominio.InterfaceRepositorio;
using UglyToad.PdfPig;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ImportarDocumentoHandler : IContratoBaseHandler<ImportarDocumentoRequest, ResultadoOperacao>
    {
        private readonly IDocumentoCommandRepositorio _documentoRepositorio;

        public ImportarDocumentoHandler(IDocumentoCommandRepositorio documentoRepositorio)
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

                var titulo = Path.GetFileNameWithoutExtension(arquivo.FileName);
                var tipoArquivo = Path.GetExtension(arquivo.FileName).Trim('.').ToLowerInvariant();
                var tamanhoArquivo = arquivo.Length;

                string texto = await ExtrairTextoAsync(arquivo, tipoArquivo);

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

        private static async Task<string> ExtrairTextoAsync(IFormFile arquivo, string tipoArquivo)
        {
            if (tipoArquivo == "pdf")
                return ExtrairTextoPdf(arquivo);

            if (tipoArquivo == "docx")
                return ExtrairTextoDocx(arquivo);

            using var reader = new StreamReader(arquivo.OpenReadStream(), Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            return await reader.ReadToEndAsync();
        }

        private static string ExtrairTextoPdf(IFormFile arquivo)
        {
            using var stream = arquivo.OpenReadStream();
            using var pdf = PdfDocument.Open(stream);

            var builder = new StringBuilder();
            foreach (var pagina in pdf.GetPages())
            {
                builder.AppendLine(pagina.Text);
            }

            return builder.ToString();
        }

        private static string ExtrairTextoDocx(IFormFile arquivo)
        {
            using var stream = arquivo.OpenReadStream();
            using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

            var entry = zip.GetEntry("word/document.xml");
            if (entry == null)
                return string.Empty;

            using var reader = new StreamReader(entry.Open());
            var xml = reader.ReadToEnd();

            var document = XDocument.Parse(xml);
            XNamespace ns = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            var textos = document.Descendants(ns + "t")
                .Select(t => t.Value)
                .Where(t => !string.IsNullOrWhiteSpace(t));

            return string.Join(" ", textos);
        }
    }
}
