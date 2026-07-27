namespace RAG.Dominio.Entidade
{
    public class DocumentoTrechoPalavra
    {
        public int Id { get; set; }
        public string Palavra { get; set; } = string.Empty;
        public int Quantidade { get; set; }

        public int IdDocumentoTrecho { get; set; }
        public DocumentoTrecho DocumentoTrecho { get; set; } = null!;
    }
}
