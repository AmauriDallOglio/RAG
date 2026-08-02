namespace RAG.Aplicacao.Rotas.RagRota
{
    public class DocumentoResumoResponse
    {
        public string Titulo { get; set; } = string.Empty;
        public string? TipoArquivo { get; set; }
        public DateTime DataImportacao { get; set; }
    }
}
