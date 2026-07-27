using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG.Dominio.InterfaceRepositorio.Configuracao
{
    public interface IGenericoCommandRepositorio<T> where T : class
    {
        Task<T> IncluirAsync(T entidade, CancellationToken cancellationToken);
        Task<T> EditarAsync(T entidade, CancellationToken cancellationToken);
        Task<bool> ExcluirAsync(T entidade, CancellationToken cancellationToken);


        Task<List<T>> ObterTodosAsync(CancellationToken cancellationToken);

        Task<T?> ObterPorIdAsync(int id, CancellationToken cancellationToken);

    }
}
