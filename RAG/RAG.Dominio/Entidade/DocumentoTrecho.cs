namespace RAG.Dominio.Entidade
{
    public class DocumentoTrecho
    {
        public int Id { get; set; }
        public string Frase { get; set; } = string.Empty;

        public int? IdDocumento { get; set; }
        public Documento? Documento { get; set; }


        // Relacionamento
        public ICollection<DocumentoTrechoPalavra> Palavras { get; set; } = new List<DocumentoTrechoPalavra>();
    }
}
