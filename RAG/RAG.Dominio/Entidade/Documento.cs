namespace RAG.Dominio.Entidade
{
    public class Documento
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;

        public string? TipoArquivo { get; set; }   // PDF, TXT, DOCX
        public long? TamanhoArquivo { get; set; }  // em bytes

        public DateTime DataImportacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }

        public ICollection<DocumentoTrecho> Trechos { get; set; } = new List<DocumentoTrecho>();

        protected Documento() { }
    }
}
