using RAG.Dominio.Entidade;

namespace RAG.Aplicacao.Rotas.RagRota
{
    public class ObterTodosDocumentoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public string? TipoArquivo { get; set; }
        public long? TamanhoArquivo { get; set; }
        public DateTime DataImportacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public static ObterTodosDocumentoResponse Criar(Documento entidade, int page = 1, int pageSize = 20)
        {
            return new ObterTodosDocumentoResponse
            {
                Id = entidade.Id,
                Titulo = entidade.Titulo,
                Texto = entidade.Texto,
                TipoArquivo = entidade.TipoArquivo,
                TamanhoArquivo = entidade.TamanhoArquivo,
                DataImportacao = entidade.DataImportacao,
                DataAtualizacao = entidade.DataAtualizacao,
                Page = page,
                PageSize = pageSize
            };
        }

        public static List<ObterTodosDocumentoResponse> CriarLista(IEnumerable<Documento> documentos, int page = 1, int pageSize = 20)
        {
            return documentos.Select(d => Criar(d, page, pageSize)).ToList();
        }
    }
}
