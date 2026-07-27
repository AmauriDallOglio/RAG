using System.Text.RegularExpressions;

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

        // Construtor de domínio
        public static Documento Criar(string titulo, string texto, string? tipoArquivo, long? tamanhoArquivo)
        {
            var documento = new Documento
            {
                Titulo = titulo,
                Texto = SanitizarTexto(texto),
                TipoArquivo = tipoArquivo,
                TamanhoArquivo = tamanhoArquivo,
                DataImportacao = DateTime.Now
            };

            // Quebra o documento em frases
            string[] frases = documento.Texto.Split(new[] { '.', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string frase in frases)
            {
                // Ignorar frases muito curtas
                if (frase.Length <= 5)
                    continue;

                string fraseCorrigida = Regex.Replace(frase.ToLowerInvariant(), @"[.,;]", "").Trim().ToLowerInvariant();
                //  fraseCorrigida = Regex.Replace(fraseCorrigida, @"[^a-zA-Z0-9\s]", ""); // remove tudo que não seja letra, número ou espaço

                var trecho = new DocumentoTrecho
                {
                    Frase = frase.Trim(),
                    Documento = documento
                };

                // Quebra a frase em palavras
                string[] palavras = fraseCorrigida.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string palavra in palavras)
                {
                    string palavraCorrigida = Regex.Replace(palavra.ToLowerInvariant(), @"[.,;]", "");

                    // Ignorar stopwords, palavras vazias e palavras de 1 caractere
                    if (string.IsNullOrWhiteSpace(palavraCorrigida)
                        || palavraCorrigida.Length == 1
                        || ignorarPalavras.Contains(palavraCorrigida))
                        continue;

                    var trechoPalavra = new DocumentoTrechoPalavra
                    {
                        Palavra = palavra,
                        Quantidade = 1,
                        DocumentoTrecho = trecho
                    };

                    trecho.Palavras.Add(trechoPalavra);
                }

                documento.Trechos.Add(trecho);
            }

            return documento;

 
        }



 

        // Lista de palavras ignoradas (stopwords)
        private static readonly HashSet<string> ignorarPalavras = new HashSet<string>
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","os","as","um","uma","uns","umas",
            "de","do","da","dos","das","em","no","na","nos","nas","ele","ela","mais","menos","mas","ou","se","que","porque","como","quando","onde",
            "foi","será","está","estão","era","eram","sou","somos","são",
            "tinha","tinham","houve","houveram","estava","estavam","seria","seriam",
            "para","por","com","sem","ou","mas","se","até","ser","será",
            "sua","seu","suas","seus","ao","aos","sobre","entre",
            "ate","apos","ja","nao","sim","pelo","pela","pelos","pelas","este","esta","estes","estas","pelos","pelas",
            "então","assim","também","ainda","pois","logo","portanto","contudo","todavia",
            "sempre","nunca","agora","depois","antes","aqui","ali","lá",
            "alguém","ninguém","todos","tudo","nada","cada","qualquer",
            "exemplo","tipo","coisa","caso","vez","forma","modo"
        };

        // sanitizar texto
        private static string SanitizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // Normaliza quebras de linha para espaços simples
            return texto.Replace("\r\n", " ").Replace("\n", " ").Trim();
        }

        // atualizar texto
        public void AtualizarTexto(string novoTexto)
        {
            Texto = SanitizarTexto(novoTexto);
            DataAtualizacao = DateTime.Now;
        }




        // Método de domínio para atualizar metadados
        public void AtualizarMetadados(string? tipoArquivo, long? tamanhoArquivo)
        {
            TipoArquivo = tipoArquivo;
            TamanhoArquivo = tamanhoArquivo;
            DataAtualizacao = DateTime.Now;
        }


    }
}
