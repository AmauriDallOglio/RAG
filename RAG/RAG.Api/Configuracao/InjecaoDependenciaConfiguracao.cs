using Microsoft.AspNetCore.Mvc;
using RAG.Aplicacao.Rotas.RagRota;
using RAG.Aplicacao.Util;
using RAG.Dominio.InterfaceRepositorio;
using RAG.Infraestrutura.Repositorio;

namespace RAG.Api.Configuracao
{
    public static class InjecaoDependenciaConfiguracao
    {
        public static void RegistrarServicos(WebApplicationBuilder builder)
        {
 

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            builder.Services.AddScoped<IContratoBaseHandler<ObterTodosDocumentoRequest, ResultadoOperacao>, ObterTodosDocumentoHandler>();
            builder.Services.AddScoped<IContratoBaseHandler<ImportarDocumentoRequest, ResultadoOperacao>, ImportarDocumentoHandler>();



            builder.Services.AddScoped<IDocumentoCommandRepositorio, DocumentoCommandRepositorio>();
            builder.Services.AddScoped<IDocumentoTrechoCommandRepositorio, DocumentoTrechoCommandRepositorio>();
            builder.Services.AddScoped<IDocumentoTrechoPalavraCommandRepositorio, DocumentoTrechoPalavraCommandRepositorio>();

 
        }
    }
}
