using Microsoft.AspNetCore.Mvc;
using RAG.Aplicacao.Rotas.RagRota;
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


            builder.Services.AddScoped<DocumentoCommandRepositorio>();
            builder.Services.AddScoped<ObterTodosDocumentoHandler>();
            builder.Services.AddScoped<ImportarDocumentoHandler>();
            builder.Services.AddScoped<ImportarTextoHandler>();
            builder.Services.AddScoped<ObterDocumentoPorTituloHandler>();
            builder.Services.AddScoped<ObterDocumentoPorTipoArquivoHandler>();
            builder.Services.AddScoped<ObterDocumentoPorTrechoHandler>();
            builder.Services.AddScoped<ObterDocumentoPorPalavraHandler>();

            builder.Services.AddScoped<IDocumentoCommandRepositorio, DocumentoCommandRepositorio>();
            builder.Services.AddScoped<IDocumentoTrechoCommandRepositorio, DocumentoTrechoCommandRepositorio>();
            builder.Services.AddScoped<IDocumentoTrechoPalavraCommandRepositorio, DocumentoTrechoPalavraCommandRepositorio>();

 
        }
    }
}
