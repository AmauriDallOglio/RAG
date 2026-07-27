using RAG.Dominio.Entidade;
using RAG.Dominio.InterfaceRepositorio.Configuracao;

namespace RAG.Dominio.InterfaceRepositorio
{
    public interface IDocumentoCommandRepositorio : IGenericoCommandRepositorio<Documento>
    {
        Task<List<Documento>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<Documento>> ObterTodosComEstruturaAsync(CancellationToken cancellationToken);
        Task<Documento?> ObterPorIdComEstruturaAsync(int id, CancellationToken cancellationToken);

    }
}
