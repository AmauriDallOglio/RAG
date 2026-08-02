namespace RAG.Aplicacao.Rotas.RagRota
{
    public class DocumentoPalavraResponse : DocumentoResumoResponse
    {
        public string? Trecho { get; set; }
        public string? Palavra { get; set; }
        public int Quantidade { get; set; }
    }
}
