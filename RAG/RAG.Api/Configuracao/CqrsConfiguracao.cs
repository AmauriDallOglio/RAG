using RAG.Aplicacao.Rotas.RagRota;
using RAG.Aplicacao.Util;

namespace RAG.Api.Configuracao
{
    public static class CqrsConfiguracao
    {
        public static IServiceCollection RegistrarCqrs(this IServiceCollection services)
        {
            services.RegistrarHandler<ImportarDocumentoRequest, ImportarDocumentoHandler>();
            services.RegistrarHandler<ObterTodosDocumentoRequest, ObterTodosDocumentoHandler>();
 

            return services;
        }

        private static IServiceCollection RegistrarHandler<TRequest, THandler>(this IServiceCollection services)
            where TRequest : IRequest<ResultadoOperacao>
            where THandler : class, IContratoBaseHandler<TRequest, ResultadoOperacao>
        {
            services.AddScoped<THandler>();
            services.AddScoped<IContratoBaseHandler<TRequest, ResultadoOperacao>, THandler>();

            return services;
        }
    }
}
