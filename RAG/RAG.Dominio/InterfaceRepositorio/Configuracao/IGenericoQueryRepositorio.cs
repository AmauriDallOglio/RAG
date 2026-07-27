using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG.Dominio.InterfaceRepositorio.Configuracao
{
    public interface IGenericoQueryRepositorio<T> where T : class
    {
        Task<List<T>> ObterTodosAsync(CancellationToken cancellationToken);
        Task<T?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
