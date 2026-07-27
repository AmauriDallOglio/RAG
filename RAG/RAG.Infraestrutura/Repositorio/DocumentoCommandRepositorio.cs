using RAG.Dominio.Entidade;
using RAG.Dominio.InterfaceRepositorio;
using RAG.Infraestrutura.Contexto;
using RAG.Infraestrutura.Repositorio.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace RAG.Infraestrutura.Repositorio
{
    public class DocumentoCommandRepositorio : GenericoCommandRepositorio<Documento>, IDocumentoCommandRepositorio
    {
        private readonly CommandContexto _CommandContexto;
        public DocumentoCommandRepositorio(CommandContexto dbContext) : base(dbContext)
        {
            _CommandContexto = dbContext;
        }


        // Método para carregar toda a estrutura
        public async Task<List<Documento>> ObterTodosComEstruturaAsync(CancellationToken cancellationToken)
        {
            return await _CommandContexto.Documento.AsNoTracking()
                .Include(d => d.Trechos)
                .ThenInclude(t => t.Palavras)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Documento>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            int paginaNormalizada = page < 1 ? 1 : page;
            int tamanhoNormalizado = pageSize < 1 ? 20 : pageSize;

            return await _CommandContexto.Documento.AsNoTracking()
                .Include(d => d.Trechos)
                .ThenInclude(t => t.Palavras)
                .OrderByDescending(d => d.DataImportacao)
                .Skip((paginaNormalizada - 1) * tamanhoNormalizado)
                .Take(tamanhoNormalizado)
                .ToListAsync(cancellationToken);
        }

        // Se quiser apenas um documento específico com toda a estrutura
        public async Task<Documento?> ObterPorIdComEstruturaAsync(int id, CancellationToken cancellationToken)
        {
            return await _CommandContexto.Documento.AsNoTracking()
                .Include(d => d.Trechos)
                .ThenInclude(t => t.Palavras)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }


    }
}
