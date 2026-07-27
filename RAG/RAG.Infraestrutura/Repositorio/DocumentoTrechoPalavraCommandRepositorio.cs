using RAG.Dominio.Entidade;
using RAG.Dominio.InterfaceRepositorio;
using RAG.Infraestrutura.Contexto;
using RAG.Infraestrutura.Repositorio.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG.Infraestrutura.Repositorio
{
    public class DocumentoTrechoPalavraCommandRepositorio : GenericoCommandRepositorio<DocumentoTrechoPalavra>, IDocumentoTrechoPalavraCommandRepositorio
    {
        private readonly CommandContexto _CommandContexto;
        public DocumentoTrechoPalavraCommandRepositorio(CommandContexto dbContext) : base(dbContext)
        {
            _CommandContexto = dbContext;
        }
    }
}
